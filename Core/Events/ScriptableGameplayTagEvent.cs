using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEvent : BaseScriptableObject
    {
        [Header(" [ Scriptable GameplayTag Event ] ")]
        [SerializeField] protected EGameplayTagEventType gameplayTagEventType;
        [SerializeField] private GameplayTag gameplayTag;


        public EGameplayTagEventType GameplayTagEventType => gameplayTagEventType;
        public GameplayTag GameplayTag => gameplayTag;
        public abstract ScriptableGameplayTagEventSpec CreateSpec(GameplayTagSystemComponent gameplayTagSystemComponent);
    }
}
