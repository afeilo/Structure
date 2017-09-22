using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ReferContainer))]
public class ReferContainerEditor : Editor {
    public List<string> name = new List<string>();
    public List<Object> monos = new List<Object>();
    public override void OnInspectorGUI()
    {
        var container = (ReferContainer)(serializedObject.targetObject);
        if (container.name != null)
        {
            name = new List<string>(container.name);
            monos = new List<Object>(container.monos);
            for (int i = 0, len = name.Count; i < len; i++)
            {
                
                GUILayout.BeginHorizontal();
                container.name[i] = GUILayout.TextField(name[i], GUILayout.Width(100));
                if (monos!=null&&i < monos.Count)
                {
                    UnityEngine.Object obj = monos[i];
                }
                EditorGUILayout.ObjectField(monos[i], typeof(UnityEngine.Object),true);
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
}