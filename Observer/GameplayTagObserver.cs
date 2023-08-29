using UnityEngine;

using StudioScor.Utilities;

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
}
