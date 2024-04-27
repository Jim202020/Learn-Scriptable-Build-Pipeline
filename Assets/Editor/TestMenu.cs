using System.IO;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class TestMenu
{
    [MenuItem("Tools/BuildPipelineBuild", false, 1)]
    private static void BuildPipelineBuild()
    {

    }

    [MenuItem("Tools/CompatibilityBuildPipelineBuild", false, 1)]
    private static void CompatibilityBuildPipelineBuild()
    {
        var assetBundles = AssetBundleGenerator.GetPrefabsAssetBundle("Assets/Unity_UI_Samples/Prefabs");
        var outputPath = "AssetBundles/Android";
        CompatibilityBuildPipelineBuildAssetBundles(outputPath, assetBundles,true,true,BuildTarget.Android);
    }

    [MenuItem("Tools/LearnBuildContext", false, 1)]
    private static void LearnBuildContext()
    {
        Debug.Log("Start LearnBuildContext...");
        var context = new LearnBuildContext();
        context.Test();
        Debug.Log("End LearnBuildContext...");
    }

    public static bool BuildPipelineBuildAssetBundles(string outputPath, bool forceRebuild, bool useChunkBasedCompression, BuildTarget buildTarget)
    {
        var options = BuildAssetBundleOptions.None;
        if (useChunkBasedCompression)
            options |= BuildAssetBundleOptions.ChunkBasedCompression;

        if (forceRebuild)
            options |= BuildAssetBundleOptions.ForceRebuildAssetBundle;

        Directory.CreateDirectory(outputPath);
        var manifest = BuildPipeline.BuildAssetBundles(outputPath, options, buildTarget);
        return manifest != null;
    }

    public static bool CompatibilityBuildPipelineBuildAssetBundles(string outputPath, AssetBundleBuild[] builds, bool forceRebuild, bool useChunkBasedCompression, BuildTarget buildTarget)
    {
        var options = BuildAssetBundleOptions.None;
        if (useChunkBasedCompression)
            options |= BuildAssetBundleOptions.ChunkBasedCompression;

        if (forceRebuild)
            options |= BuildAssetBundleOptions.ForceRebuildAssetBundle;

        Directory.CreateDirectory(outputPath);
        // Replaced BuildPipeline.BuildAssetBundles with CompatibilityBuildPipeline.BuildAssetBundles here
        var manifest = CompatibilityBuildPipeline.BuildAssetBundles(outputPath, builds, options, buildTarget);
        return manifest != null;
    }
}