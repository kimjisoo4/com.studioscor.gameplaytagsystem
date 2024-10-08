using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public abstract class GameplayTagObserver : BaseClass
    {
        public delegate void ObserverStateEventHandler(GameplayTagObserver gameplayTagConditionObserver, bool isOn);
        public delegate void ObserverEventHandler(GameplayTagObserver gameplayTagConditionObserver);

        [Header(" [ GameplayTag Observer ] ")]
        [SerializeField] private GameObject _owner;
        private bool _isToggleOn;
        private bool _isPlaying;

        private IGameplayTagSystem _gameplayTagSystem;

        public GameObject Owner => _owner;
        public bool IsPlaying => _isPlaying;
        public bool IsToggleOn => _isToggleOn;
        public IGameplayTagSystem GameplayTagSystem => _gameplayTagSystem;

        public event ObserverEventHandler OnStartedObserving;
        public event ObserverEventHandler OnEndedObserving;
        public event ObserverStateEventHandler OnDeactivate;
        public event ObserverStateEventHandler OnActivated;

        public GameplayTagObserver(GameObject owner)
        {
            _owner = owner;
            _gameplayTagSystem = owner.GetGameplayTagSystem();
        }
        public GameplayTagObserver(IGameplayTagSystem gameplayTagSystem)
        {
            _gameplayTagSystem = gameplayTagSystem;
            _owner = _gameplayTagSystem.gameObject;
        }
        public void SetOwner(GameObject owner)
        {
            _owner = owner;
            _gameplayTagSystem = owner.GetGameplayTagSystem();
        }
        public void SetOwner(IGameplayTagSystem gameplayTagSystem)
        {
            _gameplayTagSystem = gameplayTagSystem;
            _owner = _gameplayTagSystem.gameObject;
        }

        public void OnObserver()
        {
            if (_isPlaying)
                return;

            _isPlaying = true;

            EnterObserver();

            Invoke_OnStartedObserving();

            ChangedToggleState(CanActivate(), true);
        }

        public void EndObserver()
        {
            if (!_isPlaying)
                return;

            _isPlaying = false;

            ExitObserver();

            Invoke_OnEndedObserving();

            ChangedToggleState(CanActivate());
        }


        protected virtual void EnterObserver()
        {

        }
        protected virtual void ExitObserver()
        {

        }


        protected virtual bool CanActivate()
        {
            return _gameplayTagSystem is not null;
        }

        protected abstract void Activate(bool inStart);
        protected abstract void Deactivate(bool inStart);

        protected void ChangedToggleState(bool newState, bool inStart = false)
        {
            if (_isToggleOn == newState && !inStart)
                return;

            _isToggleOn = newState;

            if (_isToggleOn)
            {
                Activate(inStart);
                Invoke_OnActivated(inStart);

            }
            else
            {
                Deactivate(inStart);
                Invoke_OnDeactivate(inStart);
            }
        }


        private void Invoke_OnStartedObserving()
        {
            OnStartedObserving?.Invoke(this);
        }
        private void Invoke_OnEndedObserving()
        {
            OnEndedObserving?.Invoke(this);
        }
        private void Invoke_OnActivated(bool inStart)
        {
            OnActivated?.Invoke(this, inStart);
        }
        private void Invoke_OnDeactivate(bool inStart)
        {
            OnDeactivate?.Invoke(this, inStart);
        }
    }
}
