#if SCOR_ENABLE_VISUALSCRIPTING
using UnityEngine;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.Extend.VisualScripting
{
    [DisableAnnotation]
    [AddComponentMenu("")]
    [IncludeInSettings(false)]
    public sealed class GameplayTagSystemMessageListener : MessageListener
    {
        private void Awake()
        {
            var gameplayTagSystem = GetComponent<IGameplayTagSystem>();

            gameplayTagSystem.OnTriggeredTag += GameplayTagSystem_OnTriggeredTag;
            
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
            if (TryGetComponent(out IGameplayTagSystem gameplayTagSystem))
            {
                gameplayTagSystem.OnTriggeredTag -= GameplayTagSystem_OnTriggeredTag;

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

        private void GameplayTagSystem_OnRemovedBlockTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_BLOCK_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnRemovedOwnedTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.REMOVE_OWNED_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnSubtractedBlockTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.SUBTRACT_BLOCK_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnSubtractedOwnedTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.SUBTRACT_OWNED_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnGrantedBlockTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.GRANT_BLOCK_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnGrantedOwnedTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.GRANT_OWNED_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnAddedBlockTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.ADD_BLOCK_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnAddedOwnedTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag)
        {
            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.ADD_OWNED_TAG, gameplayTagSystemEvent), gameplayTag);
        }

        private void GameplayTagSystem_OnTriggeredTag(IGameplayTagSystem gameplayTagSystemEvent, GameplayTag gameplayTag, object data = null)
        {
            var eventData = TriggerTagData.Get(gameplayTag, data);

            EventBus.Trigger(new EventHook(GameplayTagSystemWithVisualScripting.TRIGGER_TAG, gameplayTagSystemEvent), eventData);

            eventData.Release();
        }
    }
}

#endif