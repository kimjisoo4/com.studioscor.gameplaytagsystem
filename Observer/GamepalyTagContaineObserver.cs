using UnityEngine;
using System.Linq;

namespace StudioScor.GameplayTagSystem
{
    public class GamepalyTagContaineObserver : GameplayTagObserver
    {
        [Header(" [ Gameplay Tag Containe Observer ] ")]
        [SerializeField] private FGameplayTags containeTags;

        public GamepalyTagContaineObserver()
        {

        }
        public GamepalyTagContaineObserver(GameObject owner, FGameplayTags containTags) : base(owner)
        {
            this.containeTags = containTags;
        }
        public GamepalyTagContaineObserver(IGameplayTagSystem gameplayTagSystem, FGameplayTags containTags) : base(gameplayTagSystem)
        {
            this.containeTags = containTags;
        }

        protected override void EnterObserver()
        {
            base.EnterObserver();


            if (containeTags.Owneds.Count > 0)
            {
                GameplayTagSystem.OnGrantedOwnedTag += GameplayTagSystem_OnGrantedOwnedTag;
                GameplayTagSystem.OnRemovedOwnedTag += GameplayTagSystem_OnRemovedOwnedTag;
            }

            if (containeTags.Blocks.Count > 0)
            {
                GameplayTagSystem.OnGrantedBlockTag += GameplayTagSystem_OnGrantedBlockTag;
                GameplayTagSystem.OnRemovedBlockTag += GameplayTagSystem_OnRemovedBlockTag;

            }

        }
        protected override void ExitObserver()
        {
            base.ExitObserver();

            if (GameplayTagSystem is null)
                return;

            if (containeTags.Owneds.Count > 0)
            {
                GameplayTagSystem.OnGrantedOwnedTag -= GameplayTagSystem_OnGrantedOwnedTag;
                GameplayTagSystem.OnRemovedOwnedTag -= GameplayTagSystem_OnRemovedOwnedTag;
            }

            if (containeTags.Blocks.Count > 0)
            {
                GameplayTagSystem.OnGrantedBlockTag -= GameplayTagSystem_OnGrantedBlockTag;
                GameplayTagSystem.OnRemovedBlockTag -= GameplayTagSystem_OnRemovedBlockTag;

            }
        }

        private void GameplayTagSystem_OnGrantedOwnedTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if(!IsOn)
            {
                if(containeTags.Owneds.Contains(gameplayTag))
                {
                    ChangedToggleState(CanActivate());
                }
            }
        }


        private void GameplayTagSystem_OnRemovedOwnedTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if(IsOn)
            {
                if (containeTags.Owneds.Contains(gameplayTag))
                    ChangedToggleState(false);
            }
        }


        private void GameplayTagSystem_OnGrantedBlockTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if (!IsOn)
            {
                if (containeTags.Blocks.Contains(gameplayTag))
                {
                    ChangedToggleState(CanActivate());
                }
            }
        }
        private void GameplayTagSystem_OnRemovedBlockTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if (IsOn)
            {
                if (containeTags.Blocks.Contains(gameplayTag))
                    ChangedToggleState(false);
            }
        }


        protected override bool CanActivate()
        {
            if (!base.CanActivate())
                return false;


            if (!GameplayTagSystem.ContainAllTagsInOwned(containeTags.Owneds))
                return false;

            if (!GameplayTagSystem.ContainAllTagsInBlock(containeTags.Blocks))
                return false;

            return false;
        }

    }
}
