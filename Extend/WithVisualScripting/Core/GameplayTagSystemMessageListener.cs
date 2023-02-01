#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public sealed class GameplayTagSystemMessageListener : MessageListener
    {
        private void Awake()
        {
            var gameplayTagSystem = GetComponent<GameplayTagSystemComponent>();

            gameplayTagSystem.OnTriggeredTag += OnTriggerTagEventListener_OnTriggeredTag;
            
            gameplayTagSystem.OnAddedOwnedTag += GameplayTagSystem_OnAddedOwnedTag;
            gameplayTagSystem.OnAddedBlockTag += GameplayTagSystem_OnAddedBlockTag;

            gameplayTagSystem.OnGrantedOwnedTag += GameplayTagSystem_OnGrantedOwnedTag;
            gameplayTagSystem.OnGrantedBlockTag += GameplayTagSystem_OnGrantedBlockTag;

            gameplayTagSystem.OnSubtractedOwnedTag += GameplayTagSystem_OnSubtractedOwnedTag;
            gameplayTagSystem.OnSubtractedBlockTag += GameplayTagSystem_OnSubtractedBlockTag;

            gameplayTagSystem.OnRemovedOwnedTag += GameplayTagSystem_OnRemovedOwnedTag;
            gameplayTagSystem.OnRemovedBlockTag += GameplayTagSystem_OnRemovedBlockTag;
        }
        private void OnDestroy()
        {
            if (TryGetComponent(out GameplayTagSystemComponent gameplayTagSystem))
            {
                gameplayTagSystem.OnTriggeredTag -= OnTriggerTagEventListener_OnTriggeredTag;

                gameplayTagSystem.OnAddedOwnedTag -= GameplayTagSystem_OnAddedOwnedTag;
                gameplayTagSystem.OnAddedBlockTag -= GameplayTagSystem_OnAddedBlockTag;

                gameplayTagSystem.OnGrantedOwnedTag -= GameplayTagSystem_OnGrantedOwnedTag;
                gameplayTagSystem.OnGrantedBlockTag -= GameplayTagSystem_OnGrantedBlockTag;

                gameplayTagSystem.OnSubtractedOwnedTag -= GameplayTagSystem_OnSubtractedOwnedTag;
                gameplayTagSystem.OnSubtractedBlockTag -= GameplayTagSystem_OnSubtractedBlockTag;

                gameplayTagSystem.OnRemovedOwnedTag -= GameplayTagSystem_OnRemovedOwnedTag;
                gameplayTagSystem.OnRemovedBlockTag -= GameplayTagSystem_OnRemovedBlockTag;
            }
        }

        private void GameplayTagSystem_OnRemovedBlockTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_BLOCK_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnRemovedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_OWNED_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnSubtractedBlockTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.SUBTRACT_BLOCK_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnSubtractedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.SUBTRACT_OWNED_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnGrantedBlockTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.GRANT_BLOCK_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnGrantedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.GRANT_OWNED_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnAddedBlockTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.ADD_BLOCK_TAG, gameplayTagSystemComponent), gameplayTag);
        }

        private void GameplayTagSystem_OnAddedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.ADD_OWNED_TAG, gameplayTagSystemComponent), gameplayTag);
        }
        private void OnTriggerTagEventListener_OnTriggeredTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.TRIGGER_TAG, gameplayTagSystemComponent), gameplayTag);
        }
    }
}

#endif