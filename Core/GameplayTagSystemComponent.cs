﻿using System.Collections.Generic;
using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order:0)]
    public class GameplayTagSystemComponent : BaseMonoBehaviour, IGameplayTagSystem
    {
        [Header(" [ Setup ] ")]
        [SerializeField] private FGameplayTags initializationTags;

        protected readonly Dictionary<GameplayTag, int> ownedTags = new();
        protected readonly Dictionary<GameplayTag, int> blockTags = new();

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags => ownedTags;
        public IReadOnlyDictionary<GameplayTag, int> BlockTags => blockTags;


        public event GameplayTagEventHandler OnGrantedOwnedTag;
        public event GameplayTagEventHandler OnRemovedOwnedTag;
        public event GameplayTagEventHandler OnAddedOwnedTag;
        public event GameplayTagEventHandler OnSubtractedOwnedTag;

        public event GameplayTagEventHandler OnGrantedBlockTag;
        public event GameplayTagEventHandler OnRemovedBlockTag;
        public event GameplayTagEventHandler OnAddedBlockTag;
        public event GameplayTagEventHandler OnSubtractedBlockTag;

        public event GameplayTagEventHandler OnTriggeredTag;

        private void Awake()
        {
            SetupGameplayTagSystem();
        }

        protected void SetupGameplayTagSystem()
        {
            AddInitializationTags();

            OnSetup();
        }

        public void ResetGameplayTagSystem()
        {
            ownedTags.Clear();
            blockTags.Clear();

            AddInitializationTags();

            OnReset();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }

        private void AddInitializationTags()
        {
            foreach (var ownedTag in initializationTags.Owneds)
            {
                if (!ownedTags.TryAdd(ownedTag, 1))
                {
                    ownedTags[ownedTag] += 1;
                }
            }

            foreach (var blockTag in initializationTags.Blocks)
            {
                if (!blockTags.TryAdd(blockTag, 1))
                {
                    blockTags[blockTag] += 1;
                }
            }
        }

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

            if (ownedTags.ContainsKey(addTag))
            {
                ownedTags[addTag] += 1;

                if (ownedTags[addTag] == 1)
                {
                    Callback_OnGrantedOwnedTag(addTag);
                }
            }
            else
            {
                ownedTags.TryAdd(addTag, 1);

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

            if (ownedTags.ContainsKey(removeTag))
            {
                ownedTags[removeTag] -= 1;
            }
            else
            {
                ownedTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeTag);

            if (ownedTags[removeTag] == 0)
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

            if (blockTags.ContainsKey(addTag))
            {
                blockTags[addTag] += 1;

                if (blockTags[addTag] == 1)
                {
                    Callback_OnGrantedBlockTag(addTag);
                }
            }
            else
            {
                blockTags.TryAdd(addTag, 1);

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

            if (blockTags.ContainsKey(removeTag))
            {
                blockTags[removeTag] -= 1;
            }
            else
            {
                blockTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeTag);

            if (blockTags[removeTag] == 0)
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
