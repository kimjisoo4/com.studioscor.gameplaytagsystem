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
        private GameplayTagSystem _GameplayTagSystem;

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
                UnityEngine.Debug.LogError("Grant GameplayTag Componenet [ " + _GameplayTagSystem.name + " ] : " + content, _GameplayTagSystem);

                return;
            }

            if (UseDebug)
                UnityEngine.Debug.Log("Grant GameplayTag Componenet [ " + _GameplayTagSystem.name + " ] : " + content, _GameplayTagSystem);
#endif
        }
        #endregion

        public void SetGameplayEvent(GameplayTagSystem gameplayTagSystem)
        {
            _GameplayTagSystem = gameplayTagSystem;

            if(!_GameplayTagSystem)
            {
                Log("GameplayTag System Is Null!", true);
                
                return;
            }

            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystem.OnAddedNewOwnedTag += TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystem.OnAddedNewBlockTag += TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystem.OnAddedNewOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystem.OnAddedNewBlockTag += TryTriggerTagEvent;
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
        public void ResetGameplayEvent()
        {
            if (!_GameplayTagSystem)
                return;
            
            switch (_EventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystem.OnAddedNewOwnedTag -= TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;

                    _IsOnToggle = false;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystem.OnAddedNewBlockTag -= TryTriggerTagEvent;
                    _GameplayTagSystem.OnRemovedBlockTag -= TryTriggerTagEvent;

                    _IsOnToggle = false;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystem.OnAddedNewOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystem.OnAddedNewBlockTag -= TryTriggerTagEvent;
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

        private void TryTriggerTagEvent(GameplayTagSystem gameplayTagComponent, GameplayTag changedTag)
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
