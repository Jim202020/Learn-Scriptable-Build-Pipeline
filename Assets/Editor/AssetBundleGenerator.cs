using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class AssetBundleGenerator
{
    public static AssetBundleBuild[] GetPrefabsAssetBundle(string path)
    {
        var list = new List<AssetBundleBuild>();
        var prefabList = Directory.GetFiles("*.prefab", path);
        foreach (var prefabPath in prefabList)
        {
            var assetBundleBuild = new AssetBundleBuild();
            assetBundleBuild.assetBundleName = prefabPath;
            assetBundleBuild.assetNames = new[] { prefabPath };
            list.Add(assetBundleBuild);
        }

        return list.ToArray();
    }
}