using Assets.FrameWork.Resources.simulate;
using UnityEngine;

public class PFileAsyncLoader : PLoader
{
    public override void LoadAssets(Request request)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(request.path);

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync();
        request.obj = assetLoadRequest.asset;
        request.ab = myLoadedAssetBundle;
        //        myLoadedAssetBundle.Unload(false);

        //WWW www = new WWW (request.path);
        //yield return www;
        //request.ab = www.assetBundle;
        //Object obj = www.assetBundle.mainAsset;
        //Object[] objs = www.assetBundle.LoadAllAssets();
        //yield return objs;
        //request.obj = objs[0];
        //GameObject clone = Instantiate (request.obj) as GameObject;
        //clone.name = request.name;
        //if (parent) {
        //	clone.transform.parent = parent;
        //}
    }
}