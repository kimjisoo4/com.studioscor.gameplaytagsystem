using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEvent : BaseScriptableObject
    {
        [Header(" [ Scriptable GameplayTag Event ] ")]
        [SerializeField] protected EGameplayTagEventType _GameplayTagEventType;
        [SerializeField] private GameplayTag _GameplayTag;


        public EGameplayTagEventType GameplayTagEventType => _GameplayTagEventType;
        public GameplayTag GameplayTag => _GameplayTag;
        public abstract ScriptableGameplayTagEventSpec CreateSpec(GameplayTagSystemComponent gameplayTagSystemComponent);
    }
}
