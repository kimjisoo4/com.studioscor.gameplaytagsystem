using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    [CreateAssetMenu(fileName = "New GameplayTag", menuName = "StudioScor/GameplayTagSystem/New GameplayTag")]
    public class GameplayTag : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea] public string Description = "";
#endif
    }
}
