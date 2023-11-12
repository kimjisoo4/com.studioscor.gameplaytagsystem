using UnityEngine;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    

    public static class GameplayTagSystemUtility
    {
        public const int MAIN_ORDER = 1;
        public const int SUB_ORDER = MAIN_ORDER + 1;

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
        public static void TriggerTags(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> triggerTags)
        {
            foreach (var triggerTag in triggerTags)
            {
                gameplayTagSystem.TriggerTag(triggerTag);
            }
        }
        public static void AddOwnedTags(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> addOwnedTags)
        {
            foreach (var ownedTag in addOwnedTags)
            {
                gameplayTagSystem.AddOwnedTag(ownedTag);
            }
        }
        public static void RemoveOwnedTags(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> removeOwnedTags)
        {
            foreach (var ownedTag in removeOwnedTags)
            {
                gameplayTagSystem.RemoveOwnedTag(ownedTag);
            }
        }
        public static void AddBlockTags(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> addBlockTags)
        {
            foreach (var blockTag in addBlockTags)
            {
                gameplayTagSystem.AddBlockTag(blockTag);
            }
        }
        public static void RemoveBlockTags(this IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> removeBlockTags)
        {
            foreach (var blockTag in removeBlockTags)
            {
                gameplayTagSystem.RemoveBlockTag(blockTag);
            }
        }
        public static void GrantGameplayTags(this IGameplayTagSystem gameplayTagSystem, FGameplayTags gameplayTags)
        {
            foreach (var ownedTag in gameplayTags.Owneds)
            {
                gameplayTagSystem.AddOwnedTag(ownedTag);
            }
            foreach (var blockTag in gameplayTags.Blocks)
            {
                gameplayTagSystem.AddBlockTag(blockTag);
            }
        }
        public static void RemoveGameplayTags(this IGameplayTagSystem gameplayTagSystem, FGameplayTags gameplayTags)
        {
            foreach (var ownedTag in gameplayTags.Owneds)
            {
                gameplayTagSystem.RemoveOwnedTag(ownedTag);
            }
            foreach (var blockTag in gameplayTags.Blocks)
            {
                gameplayTagSystem.RemoveBlockTag(blockTag);
            }
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
