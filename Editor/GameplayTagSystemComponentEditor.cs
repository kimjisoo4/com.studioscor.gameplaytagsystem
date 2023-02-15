using UnityEditor;
using UnityEngine;
using StudioScor.Utilities.Editor;

namespace StudioScor.GameplayTagSystem.Editor
{
    [CustomEditor(typeof(GameplayTagSystemComponent))]
    [CanEditMultipleObjects]
    public sealed class GameplayTagSystemComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
            {
                GUILayout.Space(5f);
                SEditorUtility.GUI.DrawLine(4f);
                GUILayout.Space(5f);

                var gameplayTagSystem = (GameplayTagSystemComponent)target;

                var ownedTags = gameplayTagSystem.OwnedTags;
                var blockTags = gameplayTagSystem.BlockTags;

                GUIStyle title = new();
                GUIStyle has = new();
                GUIStyle notHas = new();

                title.normal.textColor = Color.white;
                title.alignment = TextAnchor.MiddleCenter;
                title.fontStyle = FontStyle.Bold;

                has.normal.textColor = Color.white;
                notHas.normal.textColor = Color.gray;

                GUILayout.Label("[ Owned Tags ]", title);

                if (ownedTags is not null)
                {
                    foreach (var ownedtag in ownedTags)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(ownedtag.Key.name, ownedtag.Value > 0 ? has : notHas);
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(ownedtag.Value.ToString(), ownedtag.Value > 0 ? has : notHas);
                        GUILayout.Space(10f);
                        GUILayout.EndHorizontal();

                        SEditorUtility.GUI.DrawLine(1f);
                    }
                }

                GUILayout.Space(5f);


                GUILayout.Label("[ Block Tags ]", title);

                if (blockTags is not null)
                {
                    foreach (var blockTag in blockTags)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(blockTag.Key.name, blockTag.Value > 0 ? has : notHas);
                        GUILayout.FlexibleSpace();
                        GUILayout.Label(blockTag.Value.ToString(), blockTag.Value > 0 ? has : notHas);
                        GUILayout.Space(10f);
                        GUILayout.EndHorizontal();

                        SEditorUtility.GUI.DrawLine(1f);
                    }
                }
            }
        }
    }
}
