`BundleBuildContent(IEnumerable<AssetBundleBuild> bundleBuilds)`

BundleBuildContent构造函数传入的不是AssetBundleBuild[]，而是父类型IEnumerable<AssetBundleBuild>。针对接口编程，后续如有扩展的话不管传入的是Array，List，Dictionary等实现了IEnumerable接口的数据结构都能够被识别。

```csharp
public Dictionary<string, List<GUID>> BundleLayout { get; private set; }
foreach (var bundleBuild in bundleBuilds)
{
    ...
    BundleLayout.GetOrAdd(bundleBuild.assetBundleName, out guids);
    ...
}
static class ExtensionMethods
{
    ...
    public static void GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value) where TValue : new()
    {
        if (dictionary.TryGetValue(key, out value))
            return;

        value = new TValue();
        dictionary.Add(key, value);
    }
    ...
}
```
BundleLayout是个Dictionary，通过扩展添加了GetOrAdd方法，GetOrAdd方法的作用是如果字典中不存在指定的key，则添加一个key-value，如果存在则返回对应的value。

## 主要逻辑
遍历传进来的bundleBuilds，通过解析每个bundleBuild，以bundleBuild.assetBundleName作为key，通过bundleBuild.assetNames获得GUID数组作为value，添加到BundleLayout中。Assets和Scenes列表分别存对应类型（除场景类型外的都是Asset类似）的asset GUID。
