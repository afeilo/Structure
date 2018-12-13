using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class QuickAcessUtils
{
    public static int IndexOf<T>(List<T> ls, T o) where T : ICouldEqual
    {
        for (int i = 0; i < ls.Count; i++)
        {
            if (ls[i].MEqual(o))
                return i;
        }
        return -1;
    }

    public static void UnRepeatListAdd<T>(List<T> ls, T o)
    {
        if (ls.IndexOf(o) == -1)
        {
            ls.Add(o);
        }
    }

    public static T InstantiateMono<T>(T s) where T : MonoBehaviour
    {
        GameObject o = GameObject.Instantiate(s.gameObject);
        return o.GetComponent<T>();
    }

    public static T InstantiateMono<T>(T s, Transform parent) where T : MonoBehaviour
    {
        GameObject o = GameObject.Instantiate(s.gameObject);
        o.transform.SetParent(parent, false);
        return o.GetComponent<T>();
    }

    public static Component CheckComponent<T>(GameObject o) where T : Component
    {
        Component temp = o.GetComponent<T>();
        if (temp == null)
            temp = o.AddComponent<T>();
        return temp;
    }

    /// <summary>
    /// 检查文件夹
    /// </summary>
    /// <param name="url"></param>
    /// <param name="delete"></param>
    public static bool CheckDirectory(string url, bool delete = true)
    {
        try
        {
            if (Directory.Exists(url))
            {
                if (delete)
                {
                    string[] files = Directory.GetFiles(url, "*.*", SearchOption.AllDirectories);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Delete(files[i]);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(url);
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }

    }

    public static bool CopyDirectory(string sourcePath, string destinationPath)
    {
        try
        {
            if (!Directory.Exists(sourcePath)) return false;
            Debug.LogFormat("copy direcotry {0} to {1} ,time{2}", sourcePath, destinationPath, System.DateTime.Now);
            DirectoryInfo info = new DirectoryInfo(sourcePath);
            if (!Directory.Exists(destinationPath))
                Directory.CreateDirectory(destinationPath);
            foreach (FileSystemInfo fsi in info.GetFileSystemInfos())
            {
                string destName = Path.Combine(destinationPath, fsi.Name);
                if (fsi is System.IO.FileInfo)
                {
                    if (!fsi.Extension.Equals(".meta"))
                        File.Copy(fsi.FullName, destName, true);
                }
                else
                {
                    if (!Directory.Exists(destName))
                        Directory.CreateDirectory(destName);
                    CopyDirectory(fsi.FullName, destName);
                }
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return false;
        }
    }


    public static void ForEachAction<T>(this T[] array, Func<int, T, bool> action)
    {
        if (null == action) return;
        for (int i = 0, max = array.Length; i < max; ++i)
        {
            if (action(i, array[i]))
                break;
        }
    }

    public static void CombineList<T>(this List<T> srcList, List<T> targetList)
    {
        if (null == targetList)
            return;
        foreach (T elem in targetList)
        {
            if (!srcList.Contains(elem))
                srcList.Add(elem);
        }
    }
}

public interface ICouldEqual
{
    bool MEqual(object o);
}