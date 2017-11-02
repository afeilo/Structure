using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.ComponentModel;
using System;

public class Downloader  {
	private static Downloader instance;
	/// <summary>
	/// The task queue.
	/// 下载任务列表
	/// </summary>
	public Queue<DownloadRequest> taskQueue = new Queue<DownloadRequest> ();
  	/// <summary>
  	/// The task count.
	/// 同时下载任务数量
  	/// </summary>
	private int taskCount = SystemInfo.processorCount > 1 ? SystemInfo.processorCount - 1 : 1;
	/// <summary>
	/// The current task count.
	/// 当前任务数量
	/// </summary>
	private int currentTaskCount = 0;
	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <returns>The instance.</returns>
	public static Downloader getInstance(){
		if (instance == null)
			instance = new Downloader ();
		return instance;
	}

	public DownloadRequest AddDownload(string url,string path){
		DownloadRequest request = new DownloadRequest ();
		request.Url = url;
		request.Path = path;
		taskQueue.Enqueue (request);
		realDownload ();
		return request;
	}

	/// <summary>
	/// Reals the download.
	/// 真正执行下载的类
	/// </summary>
	public void realDownload(){
		if (currentTaskCount >= taskCount)
			return;
		DownloadRequest request = taskQueue.Dequeue ();
		WebClient client = new WebClient ();
		client.DownloadFileCompleted += downloadFileCompleted;
		client.DownloadProgressChanged += downloadProgressChanged;
		client.DownloadFileAsync (new System.Uri(request.Url),request.Path,request);
		currentTaskCount++;

		//WebClient client2 = new WebClient ();
		//client2.DownloadFileCompleted += downloadFileCompleted2;
		//client2.DownloadProgressChanged += downloadProgressChanged2;
		//client2.DownloadFileAsync (new System.Uri("http://down.360safe.com/instmobilemgr.exe"),Application.streamingAssetsPath+"/2.exe");
		//WebClient client3 = new WebClient ();
		//client3.DownloadFileCompleted += downloadFileCompleted3;
		//client3.DownloadProgressChanged += downloadProgressChanged3;
		//client3.DownloadFileAsync (new System.Uri("http://mirror.cogentco.com/pub/apache/tomcat/tomcat-7/v7.0.82/bin/apache-tomcat-7.0.82.exe"),Application.streamingAssetsPath+"/3.exe");
		//WebClient client4 = new WebClient ();
		//client4.DownloadFileCompleted += downloadFileCompleted4;
		//client4.DownloadProgressChanged += downloadProgressChanged4;
		//client4.DownloadFileAsync (new System.Uri("https://codeload.github.com/AI-HELP/iOS-SDK-stable/zip/master"),Application.streamingAssetsPath+"/4.zip");
		//WebClient client5 = new WebClient ();
		//client5.DownloadFileCompleted += downloadFileCompleted5;
		//client5.DownloadProgressChanged += downloadProgressChanged5;
		//client5.DownloadFileAsync (new System.Uri ("http://182.135.76.3/bigota.d.miui.com/tools/MiPhone20141107.exe"), Application.streamingAssetsPath + "/5.exe");
	}


	private void downloadProgressChanged(object obj,DownloadProgressChangedEventArgs e){
		DownloadRequest request = e.UserState as DownloadRequest;
		request.Progress = e.ProgressPercentage;
	}
	private void downloadFileCompleted(object obj,AsyncCompletedEventArgs e){
		DownloadRequest request = e.UserState as DownloadRequest;
		request.Error = e.Error;
		request.IsDownload = true;
		currentTaskCount--;
		//realDownload ();
	}

	public class DownloadRequest : IEnumerator{
		public string Url;
		public string Path;
		public bool IsDownload = false;
		public int Progress;
		public Exception Error;

		public object Current {
			get{ 
				return Progress;
			}
		}

		//
		// Methods
		//
		public bool MoveNext (){
			return !IsDownload;
		}

		public void Reset (){
			
		}
	}
}
