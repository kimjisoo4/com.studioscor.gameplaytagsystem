namespace KimScor.GameplayTagSystem
{
    public abstract class TagEventSpec
    {
        #region
        public delegate void TagEventHandler(TagEventSpec spec);
        #endregion
        private TagEvent _TagEvent;
        public TagEvent TagEvent => _TagEvent;

        private bool _Toggle = false;

        public event TagEventHandler OnTriggerTagEvent;
        public event TagEventHandler OnReTriggerTagEvent;

        private GameplayTagSystem _GameplayTagSystem;
        public GameplayTagSystem GameplayTagSystem => _GameplayTagSystem;

        public bool UseDebug => _TagEvent.UseDebugMode;


        public TagEventSpec(TagEvent tagEvent, GameplayTagSystem gameplayTagSystem)
        {
            _TagEvent = tagEvent;
            _GameplayTagSystem = gameplayTagSystem;

            switch (TagEvent.GameplayTagEventType)
            {
                case EGameplayTagEventType.ToggleTag:
                    gameplayTagSystem.OnNewAddOwnedTag += GameplayTagSystem_OnToggle;
                    gameplayTagSystem.OnRemoveOwnedTag += GameplayTagSystem_OnToggle;
                    break;
                case EGameplayTagEventType.AddTag:
                    gameplayTagSystem.OnNewAddOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.RemoveTag:
                    gameplayTagSystem.OnRemoveOwnedTag += GameplayTagSystem_OnTriggerTag;
                    break;
                case EGameplayTagEventType.TriggerTag:
                    gameplayTagSystem.OnTriggerTag += GameplayTagSystem_OnTriggerTag;
                    break;
                default:
                    break;
            }
        }
        private void GameplayTagSystem_OnToggle(GameplayTagSystem gameplayTagSystem, GameplayTag changedTag)
        {
            if (TagEvent.GameplayTag == changedTag)
            {
                if (!_Toggle)
                {
                    OnTriggerEnter();

                    OnTriggerTagEvent?.Invoke(this);
                }
                else
                {
                    OnTriggerExit();

                    OnReTriggerTagEvent?.Invoke(this);
                }
            } 
        }

        private void GameplayTagSystem_OnTriggerTag(GameplayTagSystem gameplayTagSystem, GameplayTag changedTag)
        {
            if (TagEvent.GameplayTag == changedTag)
            {
                OnTriggerEnter();

                OnTriggerTagEvent?.Invoke(this);
            }
        }

        public abstract void OnTriggerEnter();
        public abstract void OnTriggerExit();
        public abstract void OnTriggerStay(float deltaTime);

        public virtual void OnDrawGizmo()
        {

        }
    }
}
