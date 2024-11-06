using UnityEditor;

namespace StudioScor.GameplayTagSystem.Editor
{
    public static class GameplayTagSystemPathUtility
    {
        private static string _RootFolder;
        public static string Resources => RootFolder + "Editor/Icons/";
        public static string RootFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_RootFolder))
                {
                    _RootFolder = PathOf("studioscor_gameplaytagsystem_root");
                }

                return _RootFolder;
            }
        }

        private static string PathOf(string fileName)
        {
            var files = AssetDatabase.FindAssets(fileName);

            if (files.Length == 0)
                return string.Empty;

            var assetPath = AssetDatabase.GUIDToAssetPath(files[0]).Replace(fileName, string.Empty);

            return assetPath;
        }
    }
}
