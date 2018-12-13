using Assets.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIMediator), true)]
public class UIMediatorEditor : Editor
{
    UIMediator lk = null;
    void OnEnable()
    {
        lk = target as UIMediator;
        if (lk.monos == null)
            lk.monos = new Object[0];
        if (lk.names == null)
            lk.names = new List<string>();
    }


    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        EditorGUILayout.Space();
        DrawLinkerEditor();
        DrawComment();
    }

    protected virtual void DrawComment()
    {
    }

    protected void DrawLinkerEditor()
    {
        Undo.RecordObject(target, "F");

        for (int i = 0; i < lk.monos.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            DrawItem(i);
            EditorGUILayout.EndHorizontal();
        }

        if (EditorDrawUtils.FixGreenButton("Add Item"))
        {
            AddItem(null);
        }



        EditorUtility.SetDirty(target);
    }

    string FormatName(string s)
    {
        return Regex.Replace(s, @"[^A-Za-z0-9_]", "").ToLower();
    }

    void DrawItem(int index)
    {
        string li = GUILayout.TextField((index + 1).ToString(), GUILayout.Width(20));
        int ii = index + 1;
        int.TryParse(li, out ii);
        ii = Mathf.Clamp(ii, 1, lk.names.Count);
        if (ii != index + 1)
        {
            SwitchItemIndexInsert(index, ii - 1);
        }

        EditorDrawUtils.BeginStyleBackground(Color.cyan, () =>
        {
            lk.names[index] = FormatName(lk.names[index]);
            lk.names[index] = EditorGUILayout.TextField(lk.names[index]);
        });
        Object selectType = GetMonoAtIndex(index);
        if (selectType != null)
        {
            System.Type currentType = selectType.GetType();

            GameObject go = null;
            if (currentType == typeof(GameObject))
                go = (GameObject)selectType;
            else if (currentType.IsSubclassOf(typeof(Component)))
                go = ((Component)selectType).gameObject;

            if (go != null)
            {
                List<Object> allowTypes = new List<Object>() { go };
                Component[] cs = go.GetComponents<Component>();
                allowTypes.AddRange(cs);
                int selectIndex = allowTypes.IndexOf(selectType);
                if (selectIndex == -1) selectIndex = 0;
                EditorDrawUtils.BeginStyleBackground(Color.black, () =>
                {
                    selectIndex = EditorGUILayout.Popup(selectIndex, ConvertTypeArrayToStringArray(allowTypes));
                });
                if (lk.names[index] == "")
                {
                    lk.names[index] = FormatName(allowTypes[selectIndex].name);
                }
                lk.monos[index] = allowTypes[selectIndex];
            }
        }

        lk.monos[index] = EditorGUILayout.ObjectField(lk.monos[index], typeof(UnityEngine.Object), true);

        var objfind = GetMonoAtIndex(index);
        GUILayout.BeginHorizontal();
        {
            if (objfind != null && lk.names[index] != FormatName(objfind.name))
            {
                if (EditorDrawUtils.ColorButton(Color.green, "❤", GUILayout.Width(25)))
                {
                    lk.names[index] = "";
                }
            }
            if (EditorDrawUtils.ColorButton(Color.red, "✖", GUILayout.Width(25)))
            {
                RemoveItemAtIndex(index);
            }
        }
        GUILayout.EndHorizontal();
    }

    void SwitchItemIndex(int a, int b)
    {
        Object oa = lk.monos[a];
        lk.monos[a] = lk.monos[b];
        lk.monos[b] = oa;

        string sa = lk.names[a];
        lk.names[a] = lk.names[b];
        lk.names[b] = sa;
    }

    void SwitchItemIndexInsert(int from, int to)
    {
        if (from > to)
        {
            for (int i = from; i > to; i--)
            {
                SwitchItemIndex(i, i - 1);
            }
        }
        else if (from < to)
        {
            for (int i = from; i < to; i++)
            {
                SwitchItemIndex(i, i + 1);
            }
        }
    }

    void AddItem(UnityEngine.Object obj)
    {
        List<UnityEngine.Object> monos = new List<Object>(lk.monos);
        monos.Add(obj);
        lk.names.Add("");
        lk.monos = monos.ToArray();
    }

    void RemoveItemAtIndex(int index)
    {
        List<Object> monos = new List<Object>(lk.monos);
        monos.RemoveAt(index);
        lk.monos = monos.ToArray();
        lk.names.RemoveAt(index);
    }

    Object GetMonoAtIndex(int index)
    {
        if (index >= 0 && index < lk.monos.Length)
            return lk.monos[index];
        return null;
    }

    //转换类型数组为string 数组,用来enum 下拉菜单
    static string[] ConvertTypeArrayToStringArray(List<Object> tps)
    {
        List<string> temp = new List<string>();
        for (int i = 0; i < tps.Count; i++)
        {
            string s = tps[i].GetType().ToString();
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



    //Texture tex = null;
    //public Texture GetTextPreview(UIMediator style)
    //{
    //    string id = "" + style.GetInstanceID();
    //    if (tex != null)
    //    {
    //        return tex;
    //    }
    //    GameObject o = AssetDatabase.LoadAssetAtPath<GameObject>(SlothEditorPath.FontStyleRoot + "TextSelector.prefab");
    //    if ((RectTransform)style.transform == null) return null;
    //    o = GameObject.Instantiate(o);
    //    Camera camera = o.GetComponent<Camera>();

    //    string[] ignore = new string[] { "Content/content/btn_shadow" };
    //    GameObject prefab = GameObject.Instantiate(style.gameObject);
    //    for (int i = 0; i < ignore.Length; i++)
    //    {
    //        Transform t = prefab.transform.Find(ignore[i]);
    //        if (t != null)
    //        {
    //            t.gameObject.SetActive(false);
    //        }
    //    }
    //    Canvas canvas = o.transform.Find("Canvas").GetComponent<Canvas>();
    //    prefab.transform.SetParent(canvas.transform, false);
    //    prefab.transform.localPosition = Vector3.zero;
    //    prefab.transform.localScale = Vector3.one;

    //    RectTransform rtt = (RectTransform)prefab.transform;
    //    Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, rtt);

    //    Vector3 size = bounds.extents * 2;
    //    float rx = size.x / 160;
    //    float ry = size.y / 90;
    //    Vector3 scale = Vector3.one;
    //    if (rx > 1)
    //        scale.x /= rx;
    //    if (ry > 1)
    //        scale.y /= ry;
    //    scale.x = scale.y = scale.z = Mathf.Min(scale.x, scale.y);
    //    prefab.transform.localScale = scale;
    //    bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(canvas.transform, rtt);
    //    prefab.transform.localPosition -= bounds.center;

    //    RenderTexture rt = new RenderTexture(640, 360, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
    //    //RenderTexture rt = new RenderTexture((int)rtt.sizeDelta.x * 4, 360, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
    //    camera.targetTexture = rt;

    //    camera.Render();
    //    camera.targetTexture = null;

    //    tex = rt;
    //    GameObject.DestroyImmediate(o);
    //    return rt;
    //}

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        try
        {
            UIMediator v = (UIMediator)target;
            //if (PrefabUtility.GetPrefabType(target) == PrefabType.Prefab)
            //{
            //    Texture tex = GetTextPreview(v);

            //    float rw = r.width;
            //    float rh = r.height;
            //    float tw = tex.width;
            //    float th = tex.height;


            //    float ws = tw / rw;
            //    float hs = th / rh;
            //    float s = Mathf.Max(ws, hs);

            //    float w = tw / s;
            //    float h = th / s;

            //    float gw = rw - w;
            //    float gh = rh - h;
            //    Rect rect = new Rect();
            //    rect.width = w;
            //    rect.height = h;
            //    rect.x = gw / 2 + r.x;
            //    rect.y = gh / 2 + r.y;

            //    GUI.DrawTexture(rect, tex, ScaleMode.StretchToFill);
            //}
        }
        catch (System.Exception e)
        {
        }
    }

    public override bool HasPreviewGUI()
    {
        return true;
    }
}