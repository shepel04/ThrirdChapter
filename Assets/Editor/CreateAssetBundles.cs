using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class CreateAssetBundles
    {
        [MenuItem("Custom/Build AssetBundles")]
        private static void BuildAllAssetBundles()
        {
            string assetBundlePath = Application.dataPath + "/AssetBundles";
            try
            {
                BuildPipeline.BuildAssetBundles(assetBundlePath, BuildAssetBundleOptions.None,
                    EditorUserBuildSettings.activeBuildTarget);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
            }
        }
    }
}