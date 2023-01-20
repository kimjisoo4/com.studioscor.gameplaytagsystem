using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
#if SCOR_ENABLE_VISUALSCRIPTING
    [Unity.VisualScripting.Inspectable, Unity.VisualScripting.IncludeInSettings(true)]
#endif
    [CreateAssetMenu(fileName = "New GameplayTag", menuName = "StudioScor/GameplayTagSystem/New GameplayTag")]
    public class GameplayTag : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea] public string Description = "";
#endif
    }
}
