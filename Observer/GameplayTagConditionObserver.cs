using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    
    [System.Serializable]
    public class GameplayTagConditionObserver : GameplayTagObserver
    {
        [Header(" [ Gameplay Tag Condition Observer ] ")]
        [SerializeField] private GameplayTagSO[] attributeTags;
        [SerializeField] private FConditionTags conditionTags;
        
        public GameplayTagConditionObserver(GameObject owner, IEnumerable<GameplayTagSO> attributeTags, FConditionTags conditionTags) : base(owner)
        {
            this.attributeTags = attributeTags.ToArray();
            this.conditionTags = conditionTags;
        }
        public GameplayTagConditionObserver(IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTagSO> attributeTags, FConditionTags conditionTags) : base(gameplayTagSystem)
        {
            this.attributeTags = attributeTags.ToArray();
            this.conditionTags = conditionTags;
        }

        protected override void EnterObserver()
        {
            base.EnterObserver();


            if (conditionTags.Obstacleds.Count > 0 || conditionTags.Requireds.Count > 0)
            {
                GameplayTagSystem.OnGrantedOwnedTag += GameplayTagSystem_OnGrantedOwnedTag;
                GameplayTagSystem.OnRemovedOwnedTag += GameplayTagSystem_OnRemovedOwnedTag;
            }

            if (attributeTags.Length > 0)
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

            if (conditionTags.Obstacleds.Count > 0 || conditionTags.Requireds.Count > 0)
            {
                GameplayTagSystem.OnGrantedOwnedTag -= GameplayTagSystem_OnGrantedOwnedTag;
                GameplayTagSystem.OnRemovedOwnedTag -= GameplayTagSystem_OnRemovedOwnedTag;
            }

            if (attributeTags.Length > 0)
            {
                GameplayTagSystem.OnGrantedBlockTag -= GameplayTagSystem_OnGrantedBlockTag;
                GameplayTagSystem.OnRemovedBlockTag -= GameplayTagSystem_OnRemovedBlockTag;
            }
        }

        protected override bool CanActivate()
        {
            if (!base.CanActivate())
                return false;

            if (GameplayTagSystem.ContainAnyTagsInBlock(attributeTags))
                return false;

            if (!GameplayTagSystem.ContainConditionTags(conditionTags))
                return false;

            return true;
        }


        private void GameplayTagSystem_OnGrantedOwnedTag(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag)
        {
            if(IsToggleOn)
            {
                if (conditionTags.Obstacleds.Contains(gameplayTag))
                    ChangedToggleState(false);

            }
            else
            {
                if(conditionTags.Requireds.Contains(gameplayTag))
                {
                    ChangedToggleState(CanActivate());
                }

            }
        }

        private void GameplayTagSystem_OnRemovedOwnedTag(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag)
        {
            if (IsToggleOn)
            {
                if (conditionTags.Requireds.Contains(gameplayTag))
                    ChangedToggleState(false);

            }
            else
            {
                if (conditionTags.Obstacleds.Contains(gameplayTag))
                {
                    ChangedToggleState(CanActivate());
                }

            }
        }

        private void GameplayTagSystem_OnGrantedBlockTag(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag)
        {
            if (IsToggleOn)
            {
                if (attributeTags.Contains(gameplayTag))
                    ChangedToggleState(false);

            }
        }
        private void GameplayTagSystem_OnRemovedBlockTag(IGameplayTagSystem gameplayTagSystem, IGameplayTag gameplayTag)
        {
            if (!IsToggleOn)
            {
                if (attributeTags.Contains(gameplayTag))
                    ChangedToggleState(CanActivate());
            }
        }

        protected override void Activate(bool inStart)
        {
        }

        protected override void Deactivate(bool inStart)
        {
        }
    }
}
