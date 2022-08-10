using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace KimScor.GameplayTagSystem
{

    [System.Serializable]
    public class GameplayTagEvent
    {
        public enum ETagEventType
        {
            AddOwned,
            RemoveOwned,
            AddBlock,
            RemoveBlock,
            Trigger,
        }
#if UNITY_EDITOR
        public string Name = "Tag Event";
#endif

        [SerializeField] private ETagEventType _TagEventType;
        [SerializeField] private GameplayTag[] _GameplayTags;
        [SerializeField] private UnityEvent _TagEvent;

        public void SetGameplayEvent(GameplayTagSystem gameplayTagSystem)
        {
            switch (_TagEventType)
            {
                case ETagEventType.AddOwned:
                    gameplayTagSystem.OnNewAddOwnedTag += GameplayTagSystem_OnTagEvent;
                    break;
                case ETagEventType.RemoveOwned:
                    gameplayTagSystem.OnRemoveOwnedTag += GameplayTagSystem_OnTagEvent;
                    break;
                case ETagEventType.AddBlock:
                    gameplayTagSystem.OnNewAddBlockTag += GameplayTagSystem_OnTagEvent;
                    break;
                case ETagEventType.RemoveBlock:
                    gameplayTagSystem.OnRemoveBlockTag += GameplayTagSystem_OnTagEvent;
                    break;
                case ETagEventType.Trigger:
                    gameplayTagSystem.OnTriggerTag += GameplayTagSystem_OnTagEvent;
                    break;
                default:
                    break;
            }
        }

        private void GameplayTagSystem_OnTagEvent(GameplayTagSystem gameplayTagSystem, GameplayTag changedTag)
        {
            foreach (GameplayTag tag in _GameplayTags)
            {
                if (tag == changedTag)
                {
                    _TagEvent?.Invoke();
                }
            }
        }
    }

    public class GameplayTagReciver : MonoBehaviour
    {
        [SerializeField] private GameplayTagSystem _GameplayTagSystem;
        public GameplayTagSystem GameplayTagSystem
        {
            get
            {
                if (_GameplayTagSystem == null)
                {
                    _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();
                }
                return _GameplayTagSystem;
            }
        }
        [SerializeField] private GameplayTagEvent[] _GameplayEvents;

#if UNITY_EDITOR
        private void Reset()
        {
            _GameplayTagSystem = GetComponentInParent<GameplayTagSystem>();
        }
#endif

        private void Awake()
        {
            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.SetGameplayEvent(GameplayTagSystem);
            }
        }
    }
}
