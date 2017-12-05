using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ReferContainer))]
public class ReferContainerEditor : Editor {
    public List<string> name = new List<string>();
    public List<UnityEngine.Object> monos = new List<UnityEngine.Object>();
    public override void OnInspectorGUI()
    {
        var container = (ReferContainer)(serializedObject.targetObject);
        if (container.name != null)
        {
            name = new List<string>(container.name);
            monos = new List<UnityEngine.Object>(container.monos);
            for (int i = 0, len = name.Count; i < len; i++)
            {
                
                GUILayout.BeginHorizontal();
                container.name[i] = GUILayout.TextField(name[i], GUILayout.Width(100));
                if (monos!=null&&i < monos.Count)
                {
                    UnityEngine.Object obj = monos[i];
                }
                container.monos[i] = EditorGUILayout.ObjectField(monos[i], typeof(UnityEngine.Object), true);
                if(container.monos[i]!=null){
                    if (name[i] == "")
                    {
                        container.name[i] = container.monos[i].name;
                    }

                    Type currentType = container.monos[i].GetType();
                    GameObject go = null;
                    if (currentType == typeof(GameObject))
                        go = (GameObject)container.monos[i];
                    else if (currentType.IsSubclassOf(typeof(Component)))
                        go = ((Component)container.monos[i]).gameObject;
                    Component[] cs = null;
                    if (go != null) {
                        cs = go.GetComponents<Component>();
                        List<Type> types = new List<Type>();
                        int selected = 0;
                        types.Add(go.GetType());
                        for (int j = 0; j < cs.Length; j++) {
                            types.Add(cs[j].GetType());
                            if (cs[j].GetType() == container.monos[i].GetType()) {
                                selected = j+1;
                            }
                        }
                        selected = EditorGUILayout.Popup(selected, ConvertTypeArrayToStringArray(types));
                        Debug.Log(selected);
                        if (selected == 0)
                        {
                            container.monos[i] = go;
                        }
                        else {
                            container.monos[i] = cs[selected - 1];
                        }
                    }
                    if (GUILayout.Button("auto",GUILayout.Width(40)))
                    {
                        container.name[i] = container.monos[i].name;
                    }
                }

                if (GUILayout.Button("del", GUILayout.Width(40)))
                {
                    name.RemoveAt(i);
                    monos.RemoveAt(i);
                    container.name = name.ToArray();
                    container.monos = monos.ToArray();
                }
                GUILayout.EndHorizontal();
            }
        }

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Add Item"))
            {
                name.Add("");
                monos.Add(null);
                container.name = name.ToArray();
                container.monos = monos.ToArray();
            }
        }
        GUILayout.EndHorizontal();  
    }

    public static string[] ConvertTypeArrayToStringArray(List<Type> tps)
    {
        List<string> temp = new List<string>();
        for (int i = 0; i < tps.Count; i++)
        {
            string s = tps[i].ToString();
            int index = s.LastIndexOf('.');
            if (index != -1)
            {
                index += 1;
                s = s.Substring(index);
            }

            int n = 0;
            for (int j = 0; j < temp.Count; j++)
            {
                string ts = temp[j].Split('|')[0];
                if (ts == s)
                    n += 1;
            }
            if (n > 0)
                s += "|  " + n;
            temp.Add(s);
        }
        return temp.ToArray();
    }
}