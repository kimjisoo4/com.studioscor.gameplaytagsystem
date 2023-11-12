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
        protected override Object Context => gameplayTagSystem.gameObject;
#endif

        [Header(" [ GameplayTag Event ] ")]
        [SerializeField] private EGameplayTagEventType eventType;
        [SerializeField] private GameplayTag[] gameplayTags;
        [Space(5f)]
        [SerializeField] private UnityEvent onTriggeredTag;
        [SerializeField] private UnityEvent onReleasedTag;

        public event UnityAction OnTriggeredTag;
        public event UnityAction OnReleasedTag;


        private IGameplayTagSystem gameplayTagSystem;

        private bool isPlaying = false;
        private bool wasToggleOn = false;
        public bool IsOn => wasToggleOn;
        public bool IsToggleEvent => eventType.Equals(EGameplayTagEventType.ToggleOwned)
                           || eventType.Equals(EGameplayTagEventType.ToggleBlock);

        public void OnGameplayTagEvent()
        {
            if (isPlaying)
                return;

            isPlaying = true;

            SetupEvents();
        }
        public void EndGameplayTagEvent()
        {
            if (!isPlaying)
                return;

            isPlaying = false;

            ResetEvents();
        }

        private void SetupEvents()
        {
            if (gameplayTagSystem is null)
            {
                LogError("IGameplayTagSystemEvents Is Null !!!");

                return;
            }
            switch (eventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    gameplayTagSystem.OnGrantedOwnedTag += TryTriggerTagEvent;
                    gameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    gameplayTagSystem.OnGrantedBlockTag += TryTriggerTagEvent;
                    gameplayTagSystem.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddOwned:
                    gameplayTagSystem.OnGrantedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    gameplayTagSystem.OnRemovedOwnedTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    gameplayTagSystem.OnGrantedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    gameplayTagSystem.OnRemovedBlockTag += TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    gameplayTagSystem.OnTriggeredTag += GameplayTagSystem_OnTriggeredTag;
                    break;
                default:
                    break;
            }
        }

        

        private void ResetEvents()
        {
            if (gameplayTagSystem is null)
                return;

            switch (eventType)
            {
                case EGameplayTagEventType.ToggleOwned:
                    gameplayTagSystem.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    gameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;

                    wasToggleOn = false;
                    break;
                case EGameplayTagEventType.ToggleBlock:
                    gameplayTagSystem.OnGrantedBlockTag -= TryTriggerTagEvent;
                    gameplayTagSystem.OnRemovedBlockTag -= TryTriggerTagEvent;

                    wasToggleOn = false;
                    break;
                case EGameplayTagEventType.AddOwned:
                    gameplayTagSystem.OnGrantedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveOwned:
                    gameplayTagSystem.OnRemovedOwnedTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.AddBlock:
                    gameplayTagSystem.OnGrantedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.RemoveBlock:
                    gameplayTagSystem.OnRemovedBlockTag -= TryTriggerTagEvent;
                    break;
                case EGameplayTagEventType.Trigger:
                    gameplayTagSystem.OnTriggeredTag -= GameplayTagSystem_OnTriggeredTag;
                    break;
                default:
                    break;
            }
        }

        private void GameplayTagSystem_OnTriggeredTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag, object data = null)
        {
            TryTriggerTagEvent(gameplayTagSystem, gameplayTag);
        }

        public void SetTarget(GameObject target)
        {
            var gameplayTagSystem = target.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystemEvent(gameplayTagSystem);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystem = component.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystemEvent(gameplayTagSystem);
        }

        public void SetGameplayTagSystemEvent(IGameplayTagSystem gameplayTagSystemEvent)
        {
            gameplayTagSystem = gameplayTagSystemEvent;

            if(gameplayTagSystem is null)
            {
                LogError("IGameplayTag System Events Is NULL!!!");
                
                return;
            }
        }

        private void TryTriggerTagEvent(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag changedTag)
        {
            foreach (GameplayTag tag in gameplayTags)
            {
                if (tag == changedTag)
                {
                    if (IsToggleEvent)
                    {
                        if (!wasToggleOn)
                        {
                            wasToggleOn = true;

                            Callback_OnTriggered();
                        }
                        else
                        {
                            wasToggleOn = false;

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

            onTriggeredTag?.Invoke();
            OnTriggeredTag?.Invoke();
        }
        protected void Callback_OnReleased()
        {
            Log(" GameplayTag On Release Event");

            onReleasedTag?.Invoke();
            OnReleasedTag?.Invoke();
        }
    }
}
