using System;
using System.IO;
using NaughtyAttributes;
using ProjectCore.ServiceLocator;
using UnityEditor;
using UnityEngine;

namespace ProjectCore.Configs
{
    [CreateAssetMenu(fileName = nameof(ProjectConfiguration), menuName = "Config/" + nameof(ProjectConfiguration), order = 0)]
    public class ProjectConfiguration : ScriptableObject, IService
    {
        public Type ServiceType => typeof(ProjectConfiguration);
        
#if UNITY_EDITOR
        private const string CompanyName = "CoffeeVR";
        
        [BoxGroup("Build")] public string GameName = "Untitled";
        [BoxGroup("Build")] public string BuildName = "Untitled";
        [BoxGroup("Build")] public string Version = "0.1";
        [BoxGroup("Build")] public int BundleVersion = 1;
        [BoxGroup("Build"), ReorderableList] public SceneAsset[] BuildScenes;
        [BoxGroup("Build"), ReorderableList] public string[] ScriptsDefines;

        [ButtonAttribute]
        public void Build()
        {
            PlayerSettings.companyName = CompanyName;
            PlayerSettings.productName = GameName;

            PlayerSettings.bundleVersion = Version;
            PlayerSettings.Android.bundleVersionCode = BundleVersion;

            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, $"com.{CompanyName}.{GameName}"
                .ToLower()
                .Replace("-", "")
                .Replace("_", ""));
            
            var settingScenes = new EditorBuildSettingsScene[BuildScenes.Length];
            
            for (var i = 0; i < settingScenes.Length; i++)
            {
                var path = AssetDatabase.GetAssetPath(BuildScenes[i]);
                settingScenes[i] = new EditorBuildSettingsScene(path, true);
            }

            BuildPipeline.BuildPlayer(settingScenes, $"{Directory.GetCurrentDirectory()}/Builds/{GameName}/{BuildName}.apk", BuildTarget.Android, BuildOptions.None);
        }
#endif
    }
}