using System.Collections.Generic;
using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order:0)]
    public class GameplayTagSystemComponent : BaseMonoBehaviour
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
        public void AddOwnedTag(GameplayTag addOwnedTag)
        {
            if (addOwnedTag == null)
                return;

            if (_OwnedTags.ContainsKey(addOwnedTag))
            {
                _OwnedTags[addOwnedTag] += 1;

                if (_OwnedTags[addOwnedTag] == 1)
                {
                    Callback_OnGrantedOwnedTag(addOwnedTag);
                }
            }
            else
            {
                _OwnedTags.TryAdd(addOwnedTag, 1);

                Callback_OnGrantedOwnedTag(addOwnedTag);
            }

            Callback_OnAddedOwnedTag(addOwnedTag);
        }

        public void AddOwnedTags(GameplayTag[] addOwnedTags)
        {
            if (addOwnedTags == null)
                return;

            foreach (GameplayTag tag in addOwnedTags)
            {
                AddOwnedTag(tag);
            }
        }

        public void RemoveOwnedTag(GameplayTag removeOwnedTag)
        {
            if (removeOwnedTag == null)
                return;

            if (_OwnedTags.ContainsKey(removeOwnedTag))
            {
                _OwnedTags[removeOwnedTag] -= 1;

                if (_OwnedTags[removeOwnedTag] == 0)
                {
                    Callback_OnRemovedOwnedTag(removeOwnedTag);
                }
            }
            else
            {
                _OwnedTags.Add(removeOwnedTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeOwnedTag);
        }

        public void RemoveOwnedTags(GameplayTag[] removeOwnedTags)
        {
            if (removeOwnedTags == null)
                return;

            foreach (GameplayTag tag in removeOwnedTags)
            {
                RemoveOwnedTag(tag);
            }
        }

        public void AddBlockTag(GameplayTag addBlockTag)
        {
            if (addBlockTag == null)
                return;

            if (_BlockTags.ContainsKey(addBlockTag))
            {
                _BlockTags[addBlockTag] += 1;

                if (_BlockTags[addBlockTag] == 1)
                {
                    Callback_OnGrantedBlockTag(addBlockTag);
                }
            }
            else
            {
                _BlockTags.TryAdd(addBlockTag, 1);

                Callback_OnGrantedBlockTag(addBlockTag);
            }

            Callback_OnAddedBlockTag(addBlockTag);
        }

        public void AddBlockTags(GameplayTag[] addBlockTags)
        {
            if (addBlockTags == null)
                return;

            foreach (GameplayTag tag in addBlockTags)
            {
                AddBlockTag(tag);
            }
        }

        public void RemoveBlockTag(GameplayTag removeBlockTag)
        {
            if (removeBlockTag == null)
                return;

            if (_BlockTags.ContainsKey(removeBlockTag))
            {
                _BlockTags[removeBlockTag] -= 1;

                if (_BlockTags[removeBlockTag] == 0)
                {
                    Callback_OnRemovedBlockTag(removeBlockTag);
                }
            }
            else
            {
                _BlockTags.Add(removeBlockTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeBlockTag);
        }

        public void RemoveBlockTags(GameplayTag[] removeTags)
        {
            if (removeTags == null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveBlockTag(tag);
            }
        }
        #endregion

        #region Check Has Tag
        
        public bool ContainOwnedTag(GameplayTag tag)
        {
            return ContainTag(_OwnedTags, tag);
        }
        public bool ContainBlockTag(GameplayTag tag)
        {
            return ContainTag(_BlockTags, tag);
        }
        protected bool ContainTag(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag tag)
        {
            if (tag is null)
                return false;

            if (!container.TryGetValue(tag, out int value))
                return false;

            return value > 0;
        }

        protected bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
        {
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
