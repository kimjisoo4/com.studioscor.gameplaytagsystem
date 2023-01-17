using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using System.Diagnostics;

namespace StudioScor.GameplayTagSystem
{

    [System.Serializable]
    public class GameplayTagEvent
    {

#if UNITY_EDITOR
        public string DecsriptionName = "GameplayTag Event";
#endif

        [SerializeField] private EGameplayTagEventType _EventType;
        [SerializeField] private GameplayTag[] _GameplayTags;

        public UnityEvent TriggerTag;
        public UnityEvent ReleaseTag;

        private bool IsToggleEvent => _EventType.Equals(EGameplayTagEventType.ToggleOwned)
                                   || _EventType.Equals(EGameplayTagEventType.ToggleBlock);

        private bool _IsOnToggle = false;
        private GameplayTagSystem _GameplayTagSystemComponent;

        #region EDITOR ONLY
#if UNITY_EDITOR
        [SerializeField] private bool _UseDebug;
        protected bool UseDebug => _UseDebug;
#endif
        [Conditional("UNITY_EDITOR")]
        protected void Log(object content, bool isError = false)
        {
#if UNITY_EDITOR
            if (isError)
            {
                UnityEngine.Debug.LogError("Grant GameplayTag Componenet [ " + _GameplayTagSystemComponent.name + " ] : " + content, _GameplayTagSystemComponent);

                return;
            }

            if (UseDebug)
                UnityEngine.Debug.Log("Grant GameplayTag Componenet [ " + _GameplayTagSystemComponent.name + " ] : " + content, _GameplayTagSystemComponent);
#endif
        }
        #endregion

        public void SetGameplayEvent(GameplayTagSystem gameplayTagSystem)
        {
            _GameplayTagSystemComponent = gameplayTagSystem;

            if(!_GameplayTagSystemComponent)
            {
                Log("GameplayTag System Is Null!", true);
                
                return;
            }

            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag += TryTriggerTagEvent;
                    _GameplayTagSystemComponent.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag += TryTriggerTagEvent;
                    _GameplayTagSystemComponent.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystemComponent.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystemComponent.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystemComponent.OnTriggeredTag += TryTriggerTagEvent;
                    break;
                default:
                    break;
            }
        }
        public void ResetGameplayEvent()
        {
            if (!_GameplayTagSystemComponent)
                return;
            
            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    _GameplayTagSystemComponent.OnRemovedOwnedTag -= TryTriggerTagEvent;

                    _IsOnToggle = false;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag -= TryTriggerTagEvent;
                    _GameplayTagSystemComponent.OnRemovedBlockTag -= TryTriggerTagEvent;

                    _IsOnToggle = false;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystemComponent.OnRemovedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystemComponent.OnRemovedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystemComponent.OnTriggeredTag -= TryTriggerTagEvent;
                    break;
                default:
                    break;
            }
        }

        private void TryTriggerTagEvent(GameplayTagSystem gameplayTagSystemComponent, GameplayTag changedTag)
        {
            foreach (GameplayTag tag in _GameplayTags)
            {
                if (tag == changedTag)
                {
                    if (IsToggleEvent)
                    {
                        if (!_IsOnToggle)
                        {
                            _IsOnToggle = true;

                            TriggerTag?.Invoke();

                            Log(" GameplayTag On Trigger Event ( Tag : " + tag.name + " )");
                        }
                        else
                        {
                            _IsOnToggle = false;

                            ReleaseTag?.Invoke();

                            Log(" GameplayTag On Release Event ( Tag : " + tag.name + " )");
                        }
                    }
                    else
                    {
                        TriggerTag?.Invoke();

                        Log(" GameplayTag On Trigger Event ( Tag : " + tag.name + " )");

                    }

                    break;
                }
            }
        }
    }
}
