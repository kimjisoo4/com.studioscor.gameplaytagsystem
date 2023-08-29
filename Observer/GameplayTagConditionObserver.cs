using UnityEngine;

using StudioScor.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace StudioScor.GameplayTagSystem
{
    public delegate void ObserverToggleHandler(GameplayTagObserver gameplayTagConditionObserver, bool isOn);    

    public class GameplayTagObserver : BaseClass
    {
        [field: Header(" [ GameplayTag Observer ] ")]
        [field: SerializeField] public GameObject Owner { get; private set; }

        [field: SerializeField][field: SReadOnly] public bool IsOn { get; private set; } = false;
        [field: SerializeField][field: SReadOnly] public bool IsPlaying { get; private set; } = false;

        public IGameplayTagSystem GameplayTagSystem { get; private set; }
        public event ObserverToggleHandler OnChangedState;

        public GameplayTagObserver()
        {

        }
        public GameplayTagObserver(GameObject owner)
        {
            GameplayTagSystem = owner.GetGameplayTagSystem();
            Owner = GameplayTagSystem.gameObject;
        }
        public GameplayTagObserver(IGameplayTagSystem gameplayTagSystem)
        {
            GameplayTagSystem = gameplayTagSystem;
            Owner = GameplayTagSystem.gameObject;
        }

        public void SetOwner(GameObject owner)
        {
            GameplayTagSystem = owner.GetGameplayTagSystem();
            Owner = GameplayTagSystem.gameObject;
        }
        public void SetOwner(IGameplayTagSystem targetGameplayTagSystem)
        {
            GameplayTagSystem = targetGameplayTagSystem;
            Owner = GameplayTagSystem.gameObject;
        }
        public void OnObserver()
        {
            if (IsPlaying)
                return;

            IsPlaying = true;
            IsOn = false;

            ChangedToggleState(CanActivate());

            EnterObserver();
        }

        public void EndObserver()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;

            if (GameplayTagSystem is null)
                return;

            ExitObserver();
        }

        protected virtual void EnterObserver()
        {

        }
        protected virtual void ExitObserver()
        {

        }


        protected virtual bool CanActivate()
        {
            return true;
        }

        protected void ChangedToggleState(bool newState)
        {
            if (IsOn == newState)
                return;

            IsOn = newState;

            Callback_OnActivated();
        }

        protected virtual void Callback_OnActivated()
        {
            OnChangedState?.Invoke(this, IsOn);
        }
    }
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

            }

            if (containeTags.Blocks.Count > 0)
            {

            }

        }
        protected override void ExitObserver()
        {
            base.ExitObserver();
        }
        protected override bool CanActivate()
        {
            return base.CanActivate();
        }

    }
    [System.Serializable]
    public class GameplayTagConditionObserver : GameplayTagObserver
    {
        
        [Header(" [ Gameplay Tag Condition Observer ] ")]
        [SerializeField] private GameplayTag[] attributeTags;
        [SerializeField] private FConditionTags conditionTags;

        public GameplayTagConditionObserver()
        {

        }
        
        public GameplayTagConditionObserver(GameObject owner, IEnumerable<GameplayTag> attributeTags, FConditionTags conditionTags) : base(owner)
        {
            this.attributeTags = attributeTags.ToArray();
            this.conditionTags = conditionTags;
        }
        public GameplayTagConditionObserver(IGameplayTagSystem gameplayTagSystem, IEnumerable<GameplayTag> attributeTags, FConditionTags conditionTags) : base(gameplayTagSystem)
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


        private void GameplayTagSystem_OnGrantedOwnedTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if(IsOn)
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

        private void GameplayTagSystem_OnRemovedOwnedTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if (IsOn)
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

        private void GameplayTagSystem_OnGrantedBlockTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if (IsOn)
            {
                if (attributeTags.Contains(gameplayTag))
                    ChangedToggleState(false);

            }
        }
        private void GameplayTagSystem_OnRemovedBlockTag(IGameplayTagSystem gameplayTagSystem, GameplayTag gameplayTag)
        {
            if (!IsOn)
            {
                if (attributeTags.Contains(gameplayTag))
                    ChangedToggleState(CanActivate());
            }
        }
    }
}
