using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public interface IGameplayTagSystem
    {
        public delegate void GameplayTagEventHandler(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag);
        public delegate void GameplayTagTriggerEventHandler(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag, object data = null);

        public GameObject gameObject { get; }
        public Transform transform { get; }

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags { get; }
        public IReadOnlyDictionary<GameplayTag, int> BlockTags { get; }

        public void ClearAllGameplayTags();

        public void TriggerTag(GameplayTag triggerTag, object data = null);

        public void AddOwnedTag(GameplayTag addTag);
        public void RemoveOwnedTag(GameplayTag removeTag);
        public void ClearOwnedTag(GameplayTag clearTag);

        public void AddBlockTag(GameplayTag addTag);
        public void RemoveBlockTag(GameplayTag removeTag);
        public void ClearBlockTag(GameplayTag clearTag);


        public event GameplayTagEventHandler OnGrantedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;

        public event GameplayTagEventHandler OnGrantedBlockTag;
        public event GameplayTagEventHandler OnRemovedBlockTag;
        public event GameplayTagEventHandler OnAddedBlockTag;
        public event GameplayTagEventHandler OnSubtractedBlockTag;

        public event GameplayTagTriggerEventHandler OnTriggeredTag;
    }
}
