using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEvent : BaseScriptableObject
    {
        [Header(" [ Scriptable GameplayTag Event ] ")]
        [SerializeField] protected EGameplayTagEventType gameplayTagEventType;
        [SerializeField] private GameplayTagSO gameplayTag;


        public EGameplayTagEventType GameplayTagEventType => gameplayTagEventType;
        public GameplayTagSO GameplayTag => gameplayTag;
        public abstract ScriptableGameplayTagEventSpec CreateSpec(GameplayTagSystemComponent gameplayTagSystemComponent);
    }
}
