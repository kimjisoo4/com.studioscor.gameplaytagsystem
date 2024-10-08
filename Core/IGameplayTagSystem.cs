using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public interface IGameplayTag
    {
        public string name { get; }
    }
    public interface IGameplayTagSystem
    {
        public delegate void GameplayTagEventHandler(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag);
        public delegate void GameplayTagTriggerEventHandler(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag, object data = null);

        public GameObject gameObject { get; }
        public Transform transform { get; }

        public IReadOnlyDictionary<IGameplayTag, int> OwnedTags { get; }
        public IReadOnlyDictionary<IGameplayTag, int> BlockTags { get; }

        public void ClearAllGameplayTags();

        public void TriggerTag(IGameplayTag triggerTag, object data = null);

        public void AddOwnedTag(IGameplayTag addTag);
        public void RemoveOwnedTag(IGameplayTag removeTag);
        public void ClearOwnedTag(IGameplayTag clearTag);

        public void AddBlockTag(IGameplayTag addTag);
        public void RemoveBlockTag(IGameplayTag removeTag);
        public void ClearBlockTag(IGameplayTag clearTag);


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
