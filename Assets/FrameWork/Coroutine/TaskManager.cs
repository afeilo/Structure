/// TaskManager.cs
/// Copyright (c) 2011, Ken Rockot  <k-e-n-@-REMOVE-CAPS-AND-HYPHENS-oz.gs>.  All rights reserved.
/// Everyone is granted non-exclusive license to do anything at all with this code.
///
/// This is a new coroutine interface for Unity.
///
/// The motivation for this is twofold:
///
/// 1. The existing coroutine API provides no means of stopping specific
///    coroutines; StopCoroutine only takes a string argument, and it stops
///    all coroutines started with that same string; there is no way to stop
///    coroutines which were started directly from an enumerator.  This is
///    not robust enough and is also probably pretty inefficient.
///
/// 2. StartCoroutine and friends are MonoBehaviour methods.  This means
///    that in order to start a coroutine, a user typically must have some
///    component reference handy.  There are legitimate cases where such a
///    constraint is inconvenient.  This implementation hides that
///    constraint from the user.
///
/// Example usage:
///
/// ----------------------------------------------------------------------------
/// IEnumerator MyAwesomeTask()
/// {
///     while(true) {
///         Debug.Log("Logcat iz in ur consolez, spammin u wif messagez.");
///         yield return null;
////    }
/// }
///
/// IEnumerator TaskKiller(float delay, Task t)
/// {
///     yield return new WaitForSeconds(delay);
///     t.Stop();
/// }
///
/// void SomeCodeThatCouldBeAnywhereInTheUniverse()
/// {
///     Task spam = new Task(MyAwesomeTask());
///     new Task(TaskKiller(5, spam));
/// }
/// ----------------------------------------------------------------------------
///
/// When SomeCodeThatCouldBeAnywhereInTheUniverse is called, the debug console
/// will be spammed with annoying messages for 5 seconds.
///
/// Simple, really.  There is no need to initialize or even refer to TaskManager.
/// When the first Task is created in an application, a "TaskManager" GameObject
/// will automatically be added to the scene root with the TaskManager component
/// attached.  This component will be responsible for dispatching all coroutines
/// behind the scenes.
///
/// Task also provides an event that is triggered when the coroutine exits.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// A Task object represents a coroutine.  Tasks can be started, paused, and stopped.
/// It is an error to attempt to start a task that has been stopped or which has
/// naturally terminated.
//public class Task
//{
//    /// Returns true if and only if the coroutine is running.  Paused tasks
//    /// are considered to be running.
//    public bool Running
//    {
//        get
//        {
//            return task.Running;
//        }
//    }

//    /// Returns true if and only if the coroutine is currently paused.
//    public bool Paused
//    {
//        get
//        {
//            return task.Paused;
//        }
//    }

//    /// Delegate for termination subscribers.  manual is true if and only if
//    /// the coroutine was stopped with an explicit call to Stop().
//    public delegate void FinishedHandler(bool manual,string name);

//    /// Termination event.  Triggered when the coroutine completes execution.
//    public event FinishedHandler Finished;

//    /// Creates a new Task object for the given coroutine.
//    ///
//    /// If autoStart is true (default) the task is automatically started
//    /// upon construction.
//    public Task(IEnumerator c,string name,bool autoStart = true)
//    {
//        task = TaskManager.CreateTask(c,name);
//        task.Finished += TaskFinished;
//        if (autoStart)
//            Start();
//    }

//    /// Begins execution of the coroutine
//    public void Start()
//    {
//            task.Start();
        
//    }

//    /// Discontinues execution of the coroutine at its next yield.
//    public void Stop()
//    {
//        task.Stop();
//    }

//    public void Pause()
//    {
//        task.Pause();
//    }

//    public void Unpause()
//    {
//        task.Unpause();
//    }

//    void TaskFinished(bool manual,string name)
//    {
//        FinishedHandler handler = Finished;
//        if (handler != null)
//            handler(manual,name);
//    }

//    TaskManager.TaskState task;
//}

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

        //public delegate void FinishedHandler(bool manual, string name);
        public Action<bool,string> FinishedHandler;
        //public event FinishedHandler Finished;
        public IEnumerator coroutine;
        public string name;
        private bool running;
        private bool paused;
        private bool stopped;

        public TaskState(IEnumerator c,string n)
        {
            coroutine = c;
            name = n;
        }

        private void Pause()
        {
            paused = true;
        }

        private void Unpause()
        {
            paused = false;
        }

        private void Start()
        {
            //loadingTasks.Add(this);
            //Debug.Log(loadingTasks.Count);
            if (loadingTasks.Count == 1) {
                StartConroutine();
            }
            
        }
        private void StartConroutine() {
            Debug.Log("StartConroutine  " + Time.frameCount);
            //singleton.StartCoroutine(CallWrapper());
            running = true;
        }
        private void Stop()
        {
            stopped = true;
            running = false;
        }

    }
    //public event FinishedHandler Finished;

    //public delegate void FinishedHandler(bool manual, string name);


    public bool running;
    public bool paused;
    public bool stopped;

    private IEnumerator CallWrapper()
    {
        //yield return true;
        //MLog.D("CallWrapper");
        running = true;
        IEnumerator e = null;
        TaskManager.TaskState task = null;
        while (running)
        {    
            if (e==null)
            {
                if (loadingTasks.Count == 0) {
                    running = false;
                    break;
                }
                task = loadingTasks.Dequeue();
                e = task.coroutine;
            }
            if (paused)
                yield return null;
            else
            {
                if (e != null && e.MoveNext())
                {
                    //todo 
                    //这里会在下一帧执行，需替换方案
                    yield return e.Current;
                }
                else
                {
                    //task.FinishedHandler handler = task.Finished;
                    if (task.FinishedHandler != null)
                        task.FinishedHandler(stopped, task.name);
                    e = null;
                 }
            }
        }
    }

    private void startTasks() {
        StartCoroutine(CallWrapper());
    }

    public static TaskManager singleton;

    static protected Queue<TaskManager.TaskState> loadingTasks = new Queue<TaskManager.TaskState>();
    public static TaskManager.TaskState StartTask(IEnumerator coroutine, string name)
    {
        Debug.Log("CreateTask = "+name+coroutine.ToString());
        if (singleton == null)
        {
            GameObject go = new GameObject("TaskManager");
            singleton = go.AddComponent<TaskManager>();
        }
        TaskState task = new TaskState(coroutine, name);
        loadingTasks.Enqueue(task);
        if (singleton.running == false)
        {
            singleton.startTasks();
        }
        return task;
    }
}