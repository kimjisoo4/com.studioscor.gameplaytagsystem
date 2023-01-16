using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEvent : ScriptableObject
    {
        [Header(" [ Scriptable GameplayTag Event ] ")]
        [SerializeField] protected EGameplayTagEventType _GameplayTagEventType;
        [SerializeField] private GameplayTag _GameplayTag;

#if UNITY_EDITOR
        [Header(" [ Use Debug] ")]
        [SerializeField] private bool _UseDebugMode = false;
        public bool UseDebug => _UseDebugMode;
#endif

        public EGameplayTagEventType GameplayTagEventType => _GameplayTagEventType;
        public GameplayTag GameplayTag => _GameplayTag;
        public abstract ScriptableGameplayTagEventSpec CreateSpec(GameplayTagSystemComponent gameplayTagSystemComponent);
    }
}
