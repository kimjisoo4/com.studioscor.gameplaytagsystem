using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    public delegate void GameplayTagEventHandler(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag);

    public static class GameplayTagSystemUtility
    {
        #region Get GameplayTagSystem
        public static IGameplayTagSystem GetGameplayTagSystem(this GameObject target)
        {
            return target.GetComponent<IGameplayTagSystem>();
        }
        public static IGameplayTagSystem GetGameplayTagSystem(this Component component)
        {
            var gameplayTagSystem = component as IGameplayTagSystem;
            
            if (gameplayTagSystem is not null)
                return gameplayTagSystem;

            return component.GetComponent<IGameplayTagSystem>();
        }
        public static bool TryGetGameplayTagSystem(this GameObject target, out IGameplayTagSystem gameplayTagSystem)
        {
            return target.TryGetComponent(out gameplayTagSystem);
        }
        public static bool TryGetGameplayTagSystem(this Component component, out IGameplayTagSystem gameplayTagSystem)
        {
            gameplayTagSystem = component as IGameplayTagSystem;

            if (gameplayTagSystem is not null)
                return true;

            return component.TryGetComponent(out gameplayTagSystem);
        }
        #endregion

        #region Grant, Remove Tags
        public static void GrantGameplayTags(this IGameplayTagSystem gameplayTagSystem, FGameplayTags gameplayTags)
        {
            gameplayTagSystem.AddOwnedTags(gameplayTags.Owneds);
            gameplayTagSystem.AddBlockTags(gameplayTags.Blocks);
        }
        public static void RemoveGameplayTags(this IGameplayTagSystem gameplayTagSystem, FGameplayTags gameplayTags)
        {
            gameplayTagSystem.RemoveOwnedTags(gameplayTags.Owneds);
            gameplayTagSystem.RemoveBlockTags(gameplayTags.Blocks);
        }
        #endregion

        #region Contains

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

        public static bool ContainConditionTags(this IGameplayTagSystem gameplayTagSystem, FConditionTags tags)
        {
            return gameplayTagSystem.ContainAllTagsInOwned(tags.Requireds)
                && !gameplayTagSystem.ContainAnyTagsInOwned(tags.Obstacleds);
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

        #endregion
    }
}
