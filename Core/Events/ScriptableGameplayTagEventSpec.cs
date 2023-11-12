using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEventSpec : BaseClass
    {
        #region Event
        public delegate void TagEventHandler(ScriptableGameplayTagEventSpec spec);
        #endregion

        private readonly ScriptableGameplayTagEvent tagEvent;
        private readonly GameplayTagSystemComponent gameplayTagSystemComponent;

        private bool toggle = false;

        public event TagEventHandler OnTriggerTagEvent;
        public event TagEventHandler OnReleaseTagEvent;

#if UNITY_EDITOR
        public new bool UseDebug => tagEvent.UseDebug;
        public new Object Context => tagEvent;
#endif

        public ScriptableGameplayTagEventSpec(ScriptableGameplayTagEvent gameplayTagEvent, GameplayTagSystemComponent gameplayTagSystemComponent)
        {
            tagEvent = gameplayTagEvent;
            this.gameplayTagSystemComponent = gameplayTagSystemComponent;

            switch (tagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    this.gameplayTagSystemComponent.OnGrantedOwnedTag += GameplayTagSystem_OnToggleTag;
                    this.gameplayTagSystemComponent.OnRemovedOwnedTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    this.gameplayTagSystemComponent.OnGrantedBlockTag += GameplayTagSystem_OnToggleTag;
                    this.gameplayTagSystemComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    this.gameplayTagSystemComponent.OnGrantedOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    this.gameplayTagSystemComponent.OnRemovedOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    this.gameplayTagSystemComponent.OnGrantedBlockTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    this.gameplayTagSystemComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    this.gameplayTagSystemComponent.OnTriggeredTag += GameplayTagSystemComponent_OnTriggeredTag;
                    break;
                default:
                    break;
            }
        }


        public void Remove()
        {
            switch (tagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    gameplayTagSystemComponent.OnGrantedOwnedTag -= GameplayTagSystem_OnToggleTag;
                    gameplayTagSystemComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    gameplayTagSystemComponent.OnGrantedBlockTag -= GameplayTagSystem_OnToggleTag;
                    gameplayTagSystemComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    gameplayTagSystemComponent.OnGrantedOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    gameplayTagSystemComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    gameplayTagSystemComponent.OnGrantedBlockTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    gameplayTagSystemComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    gameplayTagSystemComponent.OnTriggeredTag -= GameplayTagSystemComponent_OnTriggeredTag;
                    break;
                default:
                    break;
            }

            OnRemove();
        }

        protected virtual void OnRemove()
        {

        }

        private void GameplayTagSystem_OnToggleTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag changedTag)
        {
            if (tagEvent.GameplayTag == changedTag)
            {
                if (!toggle)
                {
                    toggle = true;

                    TriggerEnter();

                    OnTriggerTagEvent?.Invoke(this);
                }
                else
                {
                    toggle = false;

                    TriggerExit();

                    OnReleaseTagEvent?.Invoke(this);
                }
            } 
        }


        private void GameplayTagSystemComponent_OnTriggeredTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag, object data = null)
        {
            GameplayTagSystem_OnTriggerTag(gameplayTagSystem, gameplayTag);
        }

        private void GameplayTagSystem_OnTriggerTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag changedTag)
        {
            if (tagEvent.GameplayTag == changedTag)
            {
                TriggerEnter();

                OnTriggerTagEvent?.Invoke(this);
            }
        }

        public void OnTriggerStay(float deltaTime)
        {
            if (!toggle)
                return;

            TriggerStay(deltaTime);
        }


        protected abstract void TriggerEnter();
        protected abstract void TriggerExit();
        protected abstract void TriggerStay(float deltaTime);
    }
}
