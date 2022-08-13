using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace KimScor.GameplayTagSystem
{ 
    public class GameplayTagSystem : MonoBehaviour
    {
#region Events
        public delegate void TagChangeHandler(GameplayTagSystem gameplayTagSystem, GameplayTag changedTag);
#endregion

        private Dictionary<GameplayTag, int> _OwnedTags;
        private Dictionary<GameplayTag, int> _BlockTags;

        public IReadOnlyDictionary<GameplayTag, int> OwnedTags
        {
            get
            {
                if(_OwnedTags == null)
                {
                    _OwnedTags = new Dictionary<GameplayTag, int>();
                }

                return _OwnedTags;
            }
        }
        public IReadOnlyDictionary<GameplayTag, int> BlockTags
        {
            get
            {
                if (_BlockTags == null)
                {
                    _BlockTags = new Dictionary<GameplayTag, int>();
                }

                return _BlockTags;
            }
        }


        public event TagChangeHandler OnAddOwnedTag;
        public event TagChangeHandler OnNewAddOwnedTag;
        public event TagChangeHandler OnRemoveOwnedTag;

        public event TagChangeHandler OnAddBlockTag;
        public event TagChangeHandler OnNewAddBlockTag;
        public event TagChangeHandler OnRemoveBlockTag;

        public event TagChangeHandler OnTriggerTag;

#region Trigger Tag
        public void TriggerTag(GameplayTag triggerTag)
        {
            if (triggerTag is null)
                return;

            OnTriggerTag?.Invoke(this, triggerTag);
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
        public void TriggerTags(IReadOnlyCollection<GameplayTag> triggerTags)
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

            if (OwnedTags.ContainsKey(addTag))
            {
                _OwnedTags[addTag] += 1;

                if (_OwnedTags[addTag] == 1)
                {
                    OnNewAddOwnedTag?.Invoke(this, addTag);
                }
            }
            else
            {
                _OwnedTags.TryAdd(addTag, 1);

                OnNewAddOwnedTag?.Invoke(this, addTag);
            }

            OnAddOwnedTag?.Invoke(this, addTag);
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

            if (OwnedTags.TryGetValue(removeTag, out int value))
            {
                if (value > 1)
                {
                    _OwnedTags[removeTag] -= 1;
                }
                else if (value == 1)
                {
                    _OwnedTags[removeTag] = 0;

                    OnRemoveOwnedTag?.Invoke(this, removeTag);
                }
            }
        }

        public void RemoveOwnedTags(GameplayTag[] removeTags)
        {
            if (removeTags == null)
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

            if (BlockTags.ContainsKey(addTag))
            {
                _BlockTags[addTag] += 1;

                if (_BlockTags[addTag] == 1)
                {
                    OnNewAddBlockTag?.Invoke(this, addTag);
                }
            }
            else
            {
                _BlockTags.TryAdd(addTag, 1);

                OnNewAddBlockTag?.Invoke(this, addTag);
            }

            OnAddBlockTag?.Invoke(this, addTag);
        }

        public void AddBlockTags(GameplayTag[] addTags)
        {
            if (addTags == null)
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

            if (BlockTags.TryGetValue(removeTag, out int value))
            {
                if (value > 1)
                {
                    _BlockTags[removeTag] -= 1;
                }
                else if (value == 1)
                {
                    _BlockTags[removeTag] = 0;

                    OnRemoveBlockTag?.Invoke(this, removeTag);
                }
            }
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

        public bool ContainTag(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag tag)
        {
            if (tag is null)
                return false;

            if (!container.TryGetValue(tag, out int value))
                return false;

            return value > 0;
        }
        public bool ContainOwnedTag(GameplayTag tag)
        {
            return ContainTag(OwnedTags, tag);
        }
        public bool ContainBlockTag(GameplayTag tag)
        {
            return ContainTag(BlockTags, tag);
        }

        /// <summary>
        /// 모든 태그를 가지고 있는가
        /// </summary>
        private bool ContainAllTags(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
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
        /// <summary>
        /// 태그를 하나라도 가지고 있는가
        /// </summary>
        private bool ContainOnceTag(IReadOnlyDictionary<GameplayTag, int> container, GameplayTag[] tags)
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
            return ContainAllTags(OwnedTags,tags);
        }
        public bool ContainAllTagsInBlock(GameplayTag[] tags)
        {
            return ContainAllTags(BlockTags, tags);
        }
        public bool ContainOnceTagsInOwned(GameplayTag[] tags)
        {
            return ContainOnceTag(OwnedTags, tags);
        }
        public bool ContainOnceTagsInBlock(GameplayTag[] tags)
        {
            return ContainOnceTag(BlockTags, tags);
        }
#endregion
    }
}
