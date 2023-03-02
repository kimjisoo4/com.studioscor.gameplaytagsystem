using System.Collections.Generic;
using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    public interface IGameplayTagSystem
    {
        public Transform transform { get; }

        public void AddOwnedTag(GameplayTag addTag);
        public void RemoveOwnedTag(GameplayTag removeTag);
        public void AddBlockTag(GameplayTag addTag);
        public void RemoveBlockTag(GameplayTag removeTag);

        public void AddOwnedTags(IReadOnlyCollection<GameplayTag> addTags);
        public void AddBlockTags(IReadOnlyCollection<GameplayTag> addTags);
        public void RemoveOwnedTags(IReadOnlyCollection<GameplayTag> removeTags);
        public void RemoveBlockTags(IReadOnlyCollection<GameplayTag> removeTags);

        public bool ContainBlockTag(GameplayTag containTag);
        public bool ContainOwnedTag(GameplayTag containTag);
        public bool ContainAllTagsInOwned(IReadOnlyCollection<GameplayTag> containTags);
        public bool ContainAllTagsInBlock(IReadOnlyCollection<GameplayTag> containTags);
        public bool ContainAnyTagsInOwned(IReadOnlyCollection<GameplayTag> containTags);
        public bool ContainAnyTagsInBlock(IReadOnlyCollection<GameplayTag> containTags);
    }

    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order:0)]
    public class GameplayTagSystemComponent : BaseMonoBehaviour, IGameplayTagSystem
    {
        #region Events
        public delegate void GameplayTagEventHandler(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag);
        #endregion

        [Header(" [ Setup ] ")]
        [SerializeField] private FGameplayTags _InitializationTags;

        protected Dictionary<GameplayTag, int> _OwnedTags;
        protected Dictionary<GameplayTag, int> _BlockTags;

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags => _OwnedTags;
        public IReadOnlyDictionary<GameplayTag, int> BlockTags => _BlockTags;


        public event GameplayTagEventHandler OnGrantedOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;

        public event GameplayTagEventHandler OnGrantedBlockTag;
        public event GameplayTagEventHandler OnAddedBlockTag;
        public event GameplayTagEventHandler OnSubtractedBlockTag;
        public event GameplayTagEventHandler OnRemovedBlockTag;

        public event GameplayTagEventHandler OnTriggeredTag;

        private void Awake()
        {
            SetupGameplayTagSystem();
        }

        private void Start()
        {
            AddOwnedTags(_InitializationTags.Owneds);
            AddBlockTags(_InitializationTags.Blocks);
        }

        protected void SetupGameplayTagSystem()
        {
            _OwnedTags = new();
            _BlockTags = new();

            OnSetup();
        }

        public void ResetGameplayTagSystem()
        {
            _OwnedTags.Clear();
            _BlockTags.Clear();

            AddOwnedTags(_InitializationTags.Owneds);
            AddBlockTags(_InitializationTags.Blocks);

            OnReset();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }

        #region Trigger Tag
        public void TriggerTag(GameplayTag triggerTag)
        {
            if (!triggerTag)
                return;

            Callback_OnTriggerTag(triggerTag);
        }
        public void TriggerTags(GameplayTag[] triggerTags)
        {
            if (triggerTags is null)
                return;

            foreach (GameplayTag tag in triggerTags)
            {
                TriggerTag(tag);
            }
        }
        #endregion

        #region Add, Remove Tags
        public void AddOwnedTag(GameplayTag addTag)
        {
            if (addTag == null)
                return;

            if (_OwnedTags.ContainsKey(addTag))
            {
                _OwnedTags[addTag] += 1;

                if (_OwnedTags[addTag] == 1)
                {
                    Callback_OnGrantedOwnedTag(addTag);
                }
            }
            else
            {
                _OwnedTags.TryAdd(addTag, 1);

                Callback_OnGrantedOwnedTag(addTag);
            }

            Callback_OnAddedOwnedTag(addTag);
        }
        public void AddOwnedTags(IReadOnlyCollection<GameplayTag> addTags)
        {
            if (addTags is null)
                return;

            foreach (var tag in addTags)
            {
                AddOwnedTag(tag);
            }
        }
        public void AddOwnedTags(GameplayTag[] addTags)
        {
            if (addTags == null)
                return;

            foreach (GameplayTag tag in addTags)
            {
                AddOwnedTag(tag);
            }
        }

        public void RemoveOwnedTag(GameplayTag removeTag)
        {
            if (removeTag == null)
                return;

            if (_OwnedTags.ContainsKey(removeTag))
            {
                _OwnedTags[removeTag] -= 1;

                if (_OwnedTags[removeTag] == 0)
                {
                    Callback_OnRemovedOwnedTag(removeTag);
                }
            }
            else
            {
                _OwnedTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeTag);
        }
        public void RemoveOwnedTags(GameplayTag[] removeTas)
        {
            if (removeTas is null)
                return;

            foreach (GameplayTag tag in removeTas)
            {
                RemoveOwnedTag(tag);
            }
        }
        public void RemoveOwnedTags(IReadOnlyCollection<GameplayTag> removeTags)
        {
            if (removeTags is null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveOwnedTag(tag);
            }
        }

        public void AddBlockTag(GameplayTag addTag)
        {
            if (addTag == null)
                return;

            if (_BlockTags.ContainsKey(addTag))
            {
                _BlockTags[addTag] += 1;

                if (_BlockTags[addTag] == 1)
                {
                    Callback_OnGrantedBlockTag(addTag);
                }
            }
            else
            {
                _BlockTags.TryAdd(addTag, 1);

                Callback_OnGrantedBlockTag(addTag);
            }

            Callback_OnAddedBlockTag(addTag);
        }
        public void AddBlockTags(GameplayTag[] addTags)
        {
            if (addTags is null)
                return;

            foreach (GameplayTag tag in addTags)
            {
                AddBlockTag(tag);
            }
        }
        public void AddBlockTags(IReadOnlyCollection<GameplayTag> addTags)
        {
            if (addTags is null)
                return;

            foreach (GameplayTag tag in addTags)
            {
                AddBlockTag(tag);
            }
        }

        public void RemoveBlockTag(GameplayTag removeTag)
        {
            if (removeTag == null)
                return;

            if (_BlockTags.ContainsKey(removeTag))
            {
                _BlockTags[removeTag] -= 1;

                if (_BlockTags[removeTag] == 0)
                {
                    Callback_OnRemovedBlockTag(removeTag);
                }
            }
            else
            {
                _BlockTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeTag);
        }
        public void RemoveBlockTags(GameplayTag[] removeTags)
        {
            if (removeTags is null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveBlockTag(tag);
            }
        }
        public void RemoveBlockTags(IReadOnlyCollection<GameplayTag> removeTags)
        {
            if (removeTags is null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveBlockTag(tag);
            }
        }
        #endregion

        #region Check Has Tag
        protected bool ContainTag(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag tag)
        {
            if (tag is null)
                return false;

            if (!container.TryGetValue(tag, out int value))
                return false;

            return value > 0;
        }
        public bool ContainOwnedTag(GameplayTag tag)
        {
            return ContainTag(_OwnedTags, tag);
        }
        public bool ContainBlockTag(GameplayTag tag)
        {
            return ContainTag(_BlockTags, tag);
        }

        protected bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
        {
            if (tags is null)
                return true;

            foreach (GameplayTag tag in tags)
            {
                if (!ContainTag(container, tag))
                {
                    return false;
                }
            }
            return true;
        }
        protected bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, IReadOnlyCollection<GameplayTag> tags)
        {
            if (tags is null)
                return true;
            foreach (GameplayTag tag in tags)
            {
                if (!ContainTag(container, tag))
                {
                    return false;
                }
            }
            return true;
        }

        protected bool ContainAnyTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
        {
            if (tags is null)
                return false;

            foreach (GameplayTag tag in tags)
            {
                if (ContainTag(container, tag))
                {
                    return true;
                }
            }
            return false;
        }
        protected bool ContainAnyTags(IReadOnlyDictionary<GameplayTag, int> container, IReadOnlyCollection<GameplayTag> tags)
        {
            if (tags is null)
                return false;

            foreach (GameplayTag tag in tags)
            {
                if (ContainTag(container, tag))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainAllTagsInOwned(GameplayTag[] tags)
        {
            return ContainAllTags(_OwnedTags, tags);
        }
        public bool ContainAllTagsInOwned(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAllTags(_OwnedTags, tags);
        }

        public bool ContainAllTagsInBlock(GameplayTag[] tags)
        {
            return ContainAllTags(_BlockTags, tags);
        }
        public bool ContainAllTagsInBlock(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAllTags(_BlockTags, tags);
        }

        public bool ContainAnyTagsInOwned(GameplayTag[] tags)
        {
            return ContainAnyTags(_OwnedTags, tags);
        }
        public bool ContainAnyTagsInOwned(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAnyTags(_OwnedTags, tags);
        }

        public bool ContainAnyTagsInBlock(GameplayTag[] tags)
        {
            return ContainAnyTags(_BlockTags, tags);
        }
        public bool ContainAnyTagsInBlock(IReadOnlyCollection<GameplayTag> tags)
        {
            return ContainAnyTags(_BlockTags, tags);
        }

        #endregion

        #region CallBack
        protected virtual void Callback_OnTriggerTag(GameplayTag  triggerTag)
        {
            Log("On Trigger Tag - " + triggerTag);

            OnTriggeredTag?.Invoke(this, triggerTag);
        }
        protected virtual void Callback_OnAddedOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Add Owned Tag - " + addOwnedTag);

            OnAddedOwnedTag?.Invoke(this, addOwnedTag);
        }
        protected virtual void Callback_OnSubtractedOwnedTag(GameplayTag addOwnedTag)
        {
            Log("Subtract Owned Tag - " + addOwnedTag);

            OnSubtractedOwnedTag?.Invoke(this, addOwnedTag);
        }
        protected virtual void Callback_OnGrantedOwnedTag(GameplayTag addNewOwnedTag)
        {
            Log("Add New Owned Tag - " + addNewOwnedTag);

            OnGrantedOwnedTag?.Invoke(this, addNewOwnedTag);
        }
        protected virtual void Callback_OnRemovedOwnedTag(GameplayTag removeOwnedTag)
        {
            Log("Remove Owned Tag - " + removeOwnedTag);

            OnRemovedOwnedTag?.Invoke(this, removeOwnedTag);
        }
        protected virtual void Callback_OnAddedBlockTag(GameplayTag addBlockTag)
        {
            Log("Add Block Tag - " + addBlockTag);

            OnAddedBlockTag?.Invoke(this, addBlockTag);
        }
        protected virtual void Callback_OnSubtractedBlcokTag(GameplayTag addBlockTag)
        {
            Log("Subtract Block Tag - " + addBlockTag);

            OnSubtractedBlockTag?.Invoke(this, addBlockTag);
        }
        protected virtual void Callback_OnGrantedBlockTag(GameplayTag addNewBlockTag)
        {
            Log("Add New Block Tag - " + addNewBlockTag);

            OnGrantedBlockTag?.Invoke(this, addNewBlockTag);
        }
        protected virtual void Callback_OnRemovedBlockTag(GameplayTag removeBlockTag)
        {
            Log("Remove Block Tag - " + removeBlockTag);

            OnRemovedBlockTag?.Invoke(this, removeBlockTag);
        }
        #endregion
    }
}
