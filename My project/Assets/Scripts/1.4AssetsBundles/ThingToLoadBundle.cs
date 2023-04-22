using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ThingToLoadBundle : MonoBehaviour
{
    private string[] assetsName = {"Cube1", "Cube2"};
    private string[] bundlesName = {"cube1bundle", "cube2bundle"};

    private void Start()
    {
        //I'm not checking second array size just because this is an example for two objects but it should be checked
        for (int i = 0; i < assetsName.Length; i++)
        {
            StartCoroutine(LoadAsset(assetsName[i], bundlesName[i], i*2));
        }
    }

    private IEnumerator LoadAsset(string assetName, string bundleName, float horizontalOffset)
    {
        AssetBundleCreateRequest asyncBundleRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, bundleName));
        yield return asyncBundleRequest;

        AssetBundle localAssetBundle = asyncBundleRequest.assetBundle;

        if (localAssetBundle == null) {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest assetRequest = localAssetBundle.LoadAssetAsync<GameObject>(assetName);
        yield return assetRequest;

        GameObject prefab = assetRequest.asset as GameObject;
        Instantiate(prefab, new Vector3(0 + horizontalOffset, 0, 0), Quaternion.identity);

        localAssetBundle.Unload(false);
    }
}
