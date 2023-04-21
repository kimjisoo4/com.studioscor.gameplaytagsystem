using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    public delegate void GameplayTagEventHandler(IGameplayTagSystemEvent gameplayTagSystem, GameplayTag gameplayTag);

    public static class GameplayTagSystemUtility
    {
        public static IGameplayTagSystem GetGameplayTagSystem(this GameObject target)
        {
            return target.GetComponent<IGameplayTagSystem>();
        }
        public static IGameplayTagSystem GetGameplayTagSystem(this Component component)
        {
            return component.GetComponent<IGameplayTagSystem>();
        }
        public static bool TryGetGameplayTagSystem(this GameObject target, out IGameplayTagSystem gameplayTagSystem)
        {
            return target.TryGetComponent(out gameplayTagSystem);
        }
        public static bool TryGetGameplayTagSystem(this Component component, out IGameplayTagSystem gameplayTagSystem)
        {
            return component.TryGetComponent(out gameplayTagSystem);
        }

        public static IGameplayTagSystemEvent GetGameplayTagSystemEvent(this GameObject target)
        {
            return target.GetComponent<IGameplayTagSystemEvent>();
        }
        public static IGameplayTagSystemEvent GetGameplayTagSystemEvent(this Component component)
        {
            return component.GetComponent<IGameplayTagSystemEvent>();
        }
        public static bool TryGetGameplayTagSystemEvent(this GameObject target, out IGameplayTagSystemEvent gameplayTagSystemEvent)
        {
            return target.TryGetComponent(out gameplayTagSystemEvent);
        }
        public static bool TryGetGameplayTagSystemEvent(this Component component, out IGameplayTagSystemEvent gameplayTagSystemEvent)
        {
            return component.TryGetComponent(out gameplayTagSystemEvent);
        }


        public static bool ContainTag(this IReadOnlyDictionary<GameplayTag, int> container, GameplayTag tag)
        {
            if (tag is null)
                return false;

            if (container is null)
                return false;

            if (!container.TryGetValue(tag, out int value))
                return false;

            return value > 0;
        }
        public static bool ContainAllTags(this IReadOnlyDictionary<GameplayTag, int> container, IEnumerable<GameplayTag> tags)
        {
            if (tags is null)
                return true;

            if (container is null)
                return false;

            foreach (GameplayTag tag in tags)
            {
                if (!ContainTag(container, tag))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool ContainAnyTags(this IReadOnlyDictionary<GameplayTag, int> container, IEnumerable<GameplayTag> tags)
        {
            if (tags is null)
                return false;

            if (container is null)
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

        public static bool ContainBlockTag(this IGameplayTagSystem gameplayTagSystem, GameplayTag tag)
        {
            return gameplayTagSystem.BlockTags.ContainTag(tag);
        }
        public static bool ContainOwnedTag(this IGameplayTagSystem gameplayTagSystem, GameplayTag tag)
        {
            return gameplayTagSystem.OwnedTags.ContainTag(tag);
        }

        public static bool ContainAllTagsInOwned(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> tags)
        {
            return ContainAllTags(gameplayTagSystem.OwnedTags, tags);
        }
        public static bool ContainAllTagsInBlock(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> tags)
        {
            return ContainAllTags(gameplayTagSystem.BlockTags, tags);
        }

        public static bool ContainAnyTagsInOwned(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> tags)
        {
            return ContainAnyTags(gameplayTagSystem.OwnedTags, tags);
        }
        public static bool ContainAnyTagsInBlock(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> tags)
        {
            return ContainAnyTags(gameplayTagSystem.BlockTags, tags);
        }
    }
}
