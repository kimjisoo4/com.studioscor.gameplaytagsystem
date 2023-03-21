using UnityEngine;
using UnityEngine.Events;

using StudioScor.Utilities;


namespace StudioScor.GameplayTagSystem
{

    [System.Serializable]
    public class GameplayTagEvent : BaseClass
    {
#if UNITY_EDITOR
        public string DecsriptionName = "GameplayTag Event";
        public override Object Context => _GameplayTagSystem.gameObject;
#endif
        [Header(" [ GameplayTag Event ] ")]
        [SerializeField] private EGameplayTagEventType _EventType;
        [SerializeField] private GameplayTag[] _GameplayTags;
        [Space(5f)]
        [SerializeField] private UnityEvent _OnTriggeredTag;
        [SerializeField] private UnityEvent _OnReleasedTag;

        public event UnityAction OnTriggeredTag;
        public event UnityAction OnReleasedTag;


        private IGameplayTagSystemEvent _GameplayTagSystem;

        private bool _IsPlaying = false;
        private bool _WasToggleOn = false;
        public bool IsOn => _WasToggleOn;
        public bool IsToggleEvent => _EventType.Equals(EGameplayTagEventType.ToggleOwned)
                           || _EventType.Equals(EGameplayTagEventType.ToggleBlock);

        public void OnGameplayTagEvent()
        {
            if (_IsPlaying)
                return;

            _IsPlaying = true;

            SetupEvents();
        }
        public void EndGameplayTagEvent()
        {
            if (!_IsPlaying)
                return;

            _IsPlaying = false;

            ResetEvents();
        }

        private void SetupEvents()
        {
            if (_GameplayTagSystem is null)
            {
                Log("IGameplayTagSystemEvents Is Null !!!", false);

                return;
            }
            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystem.OnGrantedOwnedTag += TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystem.OnGrantedBlockTag += TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystem.OnGrantedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystem.OnGrantedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystem.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystem.OnTriggeredTag += TryTriggerTagEvent;
                    break;
                default:
                    break;
            }
        }
        private void ResetEvents()
        {
            if (_GameplayTagSystem is null)
                return;

            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystem.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;

                    _WasToggleOn = false;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystem.OnGrantedBlockTag -= TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedBlockTag -= TryTriggerTagEvent;

                    _WasToggleOn = false;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystem.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystem.OnGrantedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystem.OnRemovedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystem.OnTriggeredTag -= TryTriggerTagEvent;
                    break;
                default:
                    break;
            }
        }

        public void SetTarget(GameObject target)
        {
            var gameplayTagSystem = target.GetComponent<IGameplayTagSystemEvent>();

            SetGameplayTagSystemEvent(gameplayTagSystem);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystem = component.GetComponent<IGameplayTagSystemEvent>();

            SetGameplayTagSystemEvent(gameplayTagSystem);
        }

        public void SetGameplayTagSystemEvent(IGameplayTagSystemEvent gameplayTagSystemEvent)
        {
            _GameplayTagSystem = gameplayTagSystemEvent;

            if(_GameplayTagSystem is null)
            {
                Log("IGameplayTag System Events Is NULL!!!", true);
                
                return;
            }
        }

        private void TryTriggerTagEvent(IGameplayTagSystemEvent gameplayTagSystemEvent, GameplayTag changedTag)
        {
            foreach (GameplayTag tag in _GameplayTags)
            {
                if (tag == changedTag)
                {
                    if (IsToggleEvent)
                    {
                        if (!_WasToggleOn)
                        {
                            _WasToggleOn = true;

                            Callback_OnTriggered();
                        }
                        else
                        {
                            _WasToggleOn = false;

                            Callback_OnReleased();
                        }
                    }
                    else
                    {
                        Callback_OnTriggered();
                    }

                    break;
                }
            }
        }

        protected void Callback_OnTriggered()
        {
            Log(" GameplayTag On Trigger Event");

            _OnTriggeredTag?.Invoke();
            OnTriggeredTag?.Invoke();
        }
        protected void Callback_OnReleased()
        {
            Log(" GameplayTag On Release Event");

            _OnReleasedTag?.Invoke();
            OnReleasedTag?.Invoke();
        }
    }
}
