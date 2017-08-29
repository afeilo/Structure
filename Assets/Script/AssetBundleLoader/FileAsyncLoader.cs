using System.Collections;
using UnityEngine;

public class FileAsyncLoader : ILoader
{
    public override IEnumerator LoadAssets(Request request)
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(request.path);
        yield return bundleLoadRequest;

        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        var assetLoadRequest = myLoadedAssetBundle.LoadAllAssetsAsync();
        yield return assetLoadRequest;
        request.obj = assetLoadRequest.asset;

        //  myLoadedAssetBundle.Unload(false);

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