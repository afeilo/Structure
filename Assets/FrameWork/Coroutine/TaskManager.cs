using UnityEngine;
using System.Collections;

/// A Task object represents a coroutine.  Tasks can be started, paused, and stopped.
/// It is an error to attempt to start a task that has been stopped or which has
/// naturally terminated.
public class Task
{
    /// Returns true if and only if the coroutine is running.  Paused tasks
    /// are considered to be running.
    public bool Running
    {
        get
        {
            return task.Running;
        }
    }

    /// Returns true if and only if the coroutine is currently paused.
    public bool Paused
    {
        get
        {
            return task.Paused;
        }
    }

    /// Delegate for termination subscribers.  manual is true if and only if
    /// the coroutine was stopped with an explicit call to Stop().
    public delegate void FinishedHandler(bool manual,string name);

    /// Termination event.  Triggered when the coroutine completes execution.
    public event FinishedHandler Finished;

    /// Creates a new Task object for the given coroutine.
    ///
    /// If autoStart is true (default) the task is automatically started
    /// upon construction.
    public Task(IEnumerator c,string name,bool autoStart = true)
    {
        task = TaskManager.CreateTask(c,name);
        task.Finished += TaskFinished;
        if (autoStart)
            Start();
    }

    /// Begins execution of the coroutine
    public void Start()
    {
        task.Start();
    }

    /// Discontinues execution of the coroutine at its next yield.
    public void Stop()
    {
        task.Stop();
    }

    public void Pause()
    {
        task.Pause();
    }

    public void Unpause()
    {
        task.Unpause();
    }

    void TaskFinished(bool manual,string name)
    {
        FinishedHandler handler = Finished;
        if (handler != null)
            handler(manual,name);
    }

    TaskManager.TaskState task;
}

class TaskManager : MonoBehaviour
{
    public class TaskState
    {
        public bool Running
        {
            get
            {
                return running;
            }
        }

        public bool Paused
        {
            get
            {
                return paused;
            }
        }

        public delegate void FinishedHandler(bool manual,string name);
        public event FinishedHandler Finished;

        readonly IEnumerator _coroutine;
        readonly string name;
        bool running;
        bool paused;
        bool stopped;

        public TaskState(IEnumerator c,string n)
        {
            _coroutine = c;
            name = n;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Start()
        {
            running = true;
            singleton.StartCoroutine(CallWrapper());
        }

        public void Stop()
        {
            stopped = true;
            running = false;
        }

        IEnumerator CallWrapper()
        {
            //yield return null;
            IEnumerator e = _coroutine;
            while (running)
            {
                if (paused)
                    yield return null;
                else
                {
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        running = false;
                    }
                }
            }

            FinishedHandler handler = Finished;
            if (handler != null)
                handler(stopped,name);
        }
    }

    static TaskManager singleton;

    public static TaskState CreateTask(IEnumerator coroutine,string name)
    {
        //Debug.Log("CreateTask = "+name+coroutine.ToString());
        if (singleton == null)
        {
            GameObject go = new GameObject("TaskManager");
            singleton = go.AddComponent<TaskManager>();
        }
        return new TaskState(coroutine,name);
    }
}