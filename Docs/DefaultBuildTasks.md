## 主要逻辑
根据选项选择不同的预设构建管线，完整的构建管线流程如下：
### 设置
- SwitchToBuildPlatform
- RebuildSpriteAtlasCache
### 脚本
- BuildPlayerScripts
- PostScriptsCallback
### 依赖
- CalculateSceneDependencyData
- CalculateCustomDependencyData
- CalculateAssetDependencyData
- StripUnusedSpriteSources
- CreateBuiltInBundle
- CreateMonoScriptBundle
- PostDependencyCallback
### 打包
- GenerateBundlePacking
- UpdateBundleObjectLayout
- GenerateBundleCommands
- GenerateSubAssetPathMaps
- GenerateBundleMaps
- PostPackingCallback
### 输出
- WriteSerializedFiles
- ArchiveAndCompressBundles
- AppendBundleHash
- GenerateLinkXml
- PostWritingCallback