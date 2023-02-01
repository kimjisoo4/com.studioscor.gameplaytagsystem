using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    public abstract class ScriptableGameplayTagEventSpec : BaseClass
    {
        #region Event
        public delegate void TagEventHandler(ScriptableGameplayTagEventSpec spec);
        #endregion

        private readonly ScriptableGameplayTagEvent _TagEvent;
        private readonly GameplayTagSystemComponent _GameplayTagSystemComponent;

        private bool _Toggle = false;

        public event TagEventHandler OnTriggerTagEvent;
        public event TagEventHandler OnReleaseTagEvent;

#if UNITY_EDITOR
        public new bool UseDebug => _TagEvent.UseDebug;
#endif

        public ScriptableGameplayTagEventSpec(ScriptableGameplayTagEvent gameplayTagEvent, GameplayTagSystemComponent gameplayTagSystemComponent)
        {
            _TagEvent = gameplayTagEvent;
            _GameplayTagSystemComponent = gameplayTagSystemComponent;

            switch (_TagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag += GameplayTagSystem_OnToggleTag;
                    _GameplayTagSystemComponent.OnRemovedOwnedTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag += GameplayTagSystem_OnToggleTag;
                    _GameplayTagSystemComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystemComponent.OnRemovedOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystemComponent.OnRemovedBlockTag += GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystemComponent.OnTriggeredTag += GameplayTagSystem_OnTriggerTag;
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
                    _GameplayTagSystemComponent.OnGrantedOwnedTag -= GameplayTagSystem_OnToggleTag;
                    _GameplayTagSystemComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag -= GameplayTagSystem_OnToggleTag;
                    _GameplayTagSystemComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.AddOwned:
                    _GameplayTagSystemComponent.OnGrantedOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    _GameplayTagSystemComponent.OnRemovedOwnedTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.AddBlock:
                    _GameplayTagSystemComponent.OnGrantedBlockTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    _GameplayTagSystemComponent.OnRemovedBlockTag -= GameplayTagSystem_OnToggleTag;
                    break;
                case EGameplayTagEventType.Trigger:
                    _GameplayTagSystemComponent.OnTriggeredTag -= GameplayTagSystem_OnTriggerTag;
                    break;
                default:
                    break;
            }

            OnRemove();
        }

        protected virtual void OnRemove()
        {

        }

        private void GameplayTagSystem_OnToggleTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag changedTag)
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

        private void GameplayTagSystem_OnTriggerTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag changedTag)
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
