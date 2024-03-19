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

    public static bool CompatibilityBuildPipelineBuildAssetBundles(string outputPath, bool forceRebuild, bool useChunkBasedCompression, BuildTarget buildTarget)
    {
        var options = BuildAssetBundleOptions.None;
        if (useChunkBasedCompression)
            options |= BuildAssetBundleOptions.ChunkBasedCompression;

        if (forceRebuild)
            options |= BuildAssetBundleOptions.ForceRebuildAssetBundle;

        Directory.CreateDirectory(outputPath);
        // Replaced BuildPipeline.BuildAssetBundles with CompatibilityBuildPipeline.BuildAssetBundles here
        var manifest = CompatibilityBuildPipeline.BuildAssetBundles(outputPath, options, buildTarget);
        return manifest != null;
    }
}