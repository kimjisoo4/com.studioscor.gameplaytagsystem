using System.Collections.Generic;
using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemUtility.MAIN_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Component", order:0)]
    public class GameplayTagSystemComponent : BaseMonoBehaviour, IGameplayTagSystem
    {
        [Header(" [ Gameplay Tag System Component ] ")]
        [SerializeField] private FGameplayTags _initializationTags;

        protected readonly Dictionary<GameplayTag, int> _ownedTags = new();
        protected readonly Dictionary<GameplayTag, int> _blockTags = new();

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags => _ownedTags;
        public IReadOnlyDictionary<GameplayTag, int> BlockTags => _blockTags;


        public event IGameplayTagSystem.GameplayTagEventHandler OnGrantedOwnedTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnRemovedOwnedTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnAddedOwnedTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnSubtractedOwnedTag;
        
        public event IGameplayTagSystem.GameplayTagEventHandler OnGrantedBlockTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnRemovedBlockTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnAddedBlockTag;
        public event IGameplayTagSystem.GameplayTagEventHandler OnSubtractedBlockTag;
        
        public event IGameplayTagSystem.GameplayTagTriggerEventHandler OnTriggeredTag;

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
            _ownedTags.Clear();
            _blockTags.Clear();

            AddInitializationTags();

            OnReset();
        }

        protected virtual void OnSetup() { }
        protected virtual void OnReset() { }

        private void AddInitializationTags()
        {
            AddOwnedTags(_initializationTags.Owneds);
            AddBlockTags(_initializationTags.Blocks);
        }

        #region Trigger Tag
        public void TriggerTag(GameplayTag triggerTag, object data = null)
        {
            if (!triggerTag)
                return;

            Callback_OnTriggerTag(triggerTag, data);
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

            if (_ownedTags.ContainsKey(addTag))
            {
                _ownedTags[addTag] += 1;

                if (_ownedTags[addTag] == 1)
                {
                    Callback_OnGrantedOwnedTag(addTag);
                }
            }
            else
            {
                _ownedTags.TryAdd(addTag, 1);

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

            if (_ownedTags.ContainsKey(removeTag))
            {
                _ownedTags[removeTag] -= 1;
            }
            else
            {
                _ownedTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedOwnedTag(removeTag);

            if (_ownedTags[removeTag] == 0)
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
        public void ClearOwnedTag(GameplayTag clearTag)
        {
            if (clearTag == null)
                return;

            if (_ownedTags.ContainsKey(clearTag))
            {
                _ownedTags[clearTag] = 0;
                Callback_OnRemovedOwnedTag(clearTag);
            }
        }
        public void AddBlockTag(GameplayTag addTag)
        {
            if (addTag == null)
                return;

            if (_blockTags.ContainsKey(addTag))
            {
                _blockTags[addTag] += 1;

                if (_blockTags[addTag] == 1)
                {
                    Callback_OnGrantedBlockTag(addTag);
                }
            }
            else
            {
                _blockTags.TryAdd(addTag, 1);

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

            if (_blockTags.ContainsKey(removeTag))
            {
                _blockTags[removeTag] -= 1;
            }
            else
            {
                _blockTags.Add(removeTag, -1);
            }

            Callback_OnSubtractedBlcokTag(removeTag);

            if (_blockTags[removeTag] == 0)
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

        public void ClearBlockTag(GameplayTag clearTag)
        {
            if (clearTag == null)
                return;

            if(BlockTags.ContainsKey(clearTag))
            {
                _blockTags[clearTag] = 0;
                Callback_OnRemovedBlockTag(clearTag);
            }
        }
        #endregion

        #region CallBack
        protected virtual void Callback_OnTriggerTag(GameplayTag  triggerTag, object data = null)
        {
            Log("On Trigger Tag - " + triggerTag);

            OnTriggeredTag?.Invoke(this, triggerTag, data);
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
