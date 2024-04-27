using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class AssetBundleGenerator
{
    public static AssetBundleBuild[] GetPrefabsAssetBundle(string path)
    {
        Debug.Log("Generating AssetBundles path:" + path);
        var list = new List<AssetBundleBuild>();
        var prefabList = Directory.GetFiles(path,"*.prefab");
        foreach (var prefabPath in prefabList)
        {
            var assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetBundleName = prefabPath;
            assetBundleBuild.assetNames = new[] { prefabPath };
            list.Add(assetBundleBuild);
            break;
        }

        return list.ToArray();
    }
}