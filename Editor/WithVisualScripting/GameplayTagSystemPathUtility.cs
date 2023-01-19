#if SCOR_ENABLE_VISUALSCRIPTING
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;


using Unity.VisualScripting;


namespace StudioScor.GameplayTagSystem.VisualScripting.Editor
{
    public static class GameplayTagSystemPathUtility
    {
        private static string _RootFolder;
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
        public static string Resources => RootFolder + "Editor/Resources/";
        public static string VisualScriptingResources => RootFolder + "Editor/Resources/";

        private readonly static Dictionary<string, EditorTexture> _EditorTextures = new Dictionary<string, EditorTexture>();

        public static EditorTexture Load(string name)
        {
            if (_EditorTextures.ContainsKey(name))
            {
                return GetStateTexture(name);
            }

            var _path = VisualScriptingResources;

            var editorTexture = EditorTexture.Single(AssetDatabase.LoadAssetAtPath<Texture2D>(_path + name + ".png"));

            _EditorTextures.Add(name, editorTexture);

            return GetStateTexture(name);
        }

        private static EditorTexture GetStateTexture(string name)
        {
            return _EditorTextures[name];
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
#endif