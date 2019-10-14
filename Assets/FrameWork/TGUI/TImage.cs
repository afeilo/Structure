using Assets.Framework;
using UnityEngine;
using UnityEngine.UI;

public class TImage : Image {

    private string _spriteName;
    public string spriteName
    {
        get {
            return _spriteName;
        }
        set {
            if (_spriteName == value) return;
            _spriteName = value;
            if (string.IsNullOrEmpty(_spriteName))
            {
                enabled = false;
                sprite = null;
                return;
            }
            enabled = true;
            Loader.LoadAsset("Test",_spriteName, (UnityEngine.Object o) =>{
                sprite = o as Sprite;
            },null,typeof(Sprite));
        }
    }

    void OnEnable()
    {
        //spriteName = "1";
    }
}
