using System.Collections.Generic;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public interface IGameplayTagSystem
    {
        public GameObject gameObject { get; }
        public Transform transform { get; }
        public IReadOnlyDictionary<GameplayTag, int> OwnedTags { get; }
        public IReadOnlyDictionary<GameplayTag, int> BlockTags { get; }


        public void TriggerTag(GameplayTag triggerTag);
        public void TriggerTags(IEnumerable<GameplayTag> triggerTags);

        public void AddOwnedTag(GameplayTag addTag);
        public void RemoveOwnedTag(GameplayTag removeTag);
        public void AddBlockTag(GameplayTag addTag);
        public void RemoveBlockTag(GameplayTag removeTag);

        public void AddOwnedTags(IEnumerable<GameplayTag> addTags);
        public void AddBlockTags(IEnumerable<GameplayTag> addTags);
        public void RemoveOwnedTags(IEnumerable<GameplayTag> removeTags);
        public void RemoveBlockTags(IEnumerable<GameplayTag> removeTags);
    }
}
