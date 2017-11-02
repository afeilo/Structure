using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Version file.
/// 记录版本文件相关的信息
/// </summary>
[CreateAssetMenu(menuName="Assets/Create VersionFile ")]
public class VersionFile : ScriptableObject {
	public int version;
	public List<ABInfo> abInfos;
	/// <summary>
	/// AB info.
	/// 记录ab信息
	/// </summary>
	[System.Serializable]
	public class ABInfo{
		public string bundleName;
		public string crc;
		public long size;
	}
}
