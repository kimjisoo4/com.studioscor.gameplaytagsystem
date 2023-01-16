﻿namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEventSpec
    {
        #region Event
        public delegate void TagEventHandler(ScriptableGameplayTagEventSpec spec);
        #endregion

        private readonly ScriptableGameplayTagEvent _TagEvent;
        private readonly GameplayTagSystemComponent _GameplayTagComponent;

        private bool _Toggle = false;

        public event TagEventHandler OnTriggerTagEvent;
        public event TagEventHandler OnReleaseTagEvent;

#if UNITY_EDITOR
        public bool UseDebug => _TagEvent.UseDebug;
#endif

        public ScriptableGameplayTagEventSpec(ScriptableGameplayTagEvent gameplayTagEvent, GameplayTagSystemComponent gameplayTagSystem)
        {
            _TagEvent = gameplayTagEvent;
            _GameplayTagComponent = gameplayTagSystem;

            switch (_TagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagComponent.OnAddedNewOwnedTag += GameplayTagSystem_OnToggleTag;
                    _GameplayTagComponent.OnRemovedOwnedTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagComponent.OnAddedNewBlockTag += GameplayTagSystem_OnToggleTag;
                    _GameplayTagComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagComponent.OnAddedNewOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagComponent.OnRemovedOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagComponent.OnAddedNewBlockTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagComponent.OnTriggeredTag += GameplayTagSystem_OnTriggerTag;
                    break;
                default:
                    break;
            }
        }

        public void Remove()
        {
            switch (_TagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagComponent.OnAddedNewOwnedTag -= GameplayTagSystem_OnToggleTag;
                    _GameplayTagComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagComponent.OnAddedNewBlockTag -= GameplayTagSystem_OnToggleTag;
                    _GameplayTagComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagComponent.OnAddedNewOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagComponent.OnAddedNewBlockTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagComponent.OnTriggeredTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                default:
                    break;
            }

            OnRemove();
        }

        protected virtual void OnRemove()
        {

        }

        private void GameplayTagSystem_OnToggleTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag changedTag)
        {
            if (_TagEvent.GameplayTag == changedTag)
            {
                if (!_Toggle)
                {
                    _Toggle = true;

                    TriggerEnter();

                    OnTriggerTagEvent?.Invoke(this);
                }
                else
                {
                    _Toggle = false;

                    TriggerExit();

                    OnReleaseTagEvent?.Invoke(this);
                }
            } 
        }

        private void GameplayTagSystem_OnTriggerTag(GameplayTagSystemComponent gameplayTagComponent, GameplayTag changedTag)
        {
            if (_TagEvent.GameplayTag == changedTag)
            {
                TriggerEnter();

                OnTriggerTagEvent?.Invoke(this);
            }
        }

        public void OnTriggerStay(float deltaTime)
        {
            if (!_Toggle)
                return;

            TriggerStay(deltaTime);
        }


        protected abstract void TriggerEnter();
        protected abstract void TriggerExit();
        protected abstract void TriggerStay(float deltaTime);
    }
}