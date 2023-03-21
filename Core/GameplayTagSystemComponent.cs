using System.Collections.Generic;
using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order:0)]
    public class GameplayTagSystemComponent : BaseMonoBehaviour, IGameplayTagSystem, IGameplayTagSystemEvent
    {
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
        public void TriggerTags(IEnumerable<GameplayTag> triggerTags)
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
        public void AddOwnedTags(IEnumerable<GameplayTag> addTags)
        {
            if (addTags is null)
                return;

            foreach (var tag in addTags)
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
            }
            else
            {
                _OwnedTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeTag);

            if (_OwnedTags[removeTag] == 0)
            {
                Callback_OnRemovedOwnedTag(removeTag);
            }
        }
        public void RemoveOwnedTags(IEnumerable<GameplayTag> removeTas)
        {
            if (removeTas is null)
                return;

            foreach (GameplayTag tag in removeTas)
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
        public void AddBlockTags(IEnumerable<GameplayTag> addTags)
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
            }
            else
            {
                _BlockTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeTag);

            if (_BlockTags[removeTag] == 0)
            {
                Callback_OnRemovedBlockTag(removeTag);
            }
        }
        public void RemoveBlockTags(IEnumerable<GameplayTag> removeTags)
        {
            if (removeTags is null)
                return;

            foreach (GameplayTag tag in removeTags)
            {
                RemoveBlockTag(tag);
            }
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
