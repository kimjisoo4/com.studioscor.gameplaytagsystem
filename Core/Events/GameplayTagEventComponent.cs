using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Event Component", order: 10)]
    public class GameplayTagEventComponent : BaseMonoBehaviour
    {
        [Header(" [ GameplayTag Event Component ] ")]
        [SerializeField] private GameObject _Target;
        [SerializeField] private GameplayTagEvent[] _GameplayEvents;
        [SerializeField] private bool _AutoPlaying = true;

        private IGameplayTagSystemEvent _GameplayTagSystemEvent;
        private bool _IsPlaying = false;
        public bool IsPlaying => _IsPlaying;

#if UNITY_EDITOR
        private void Reset()
        {
            Setup();
        }

        private void OnValidate()
        {
            if (_Target)
            {
                if(_Target.TryGetComponentInParentOrChildren(out _GameplayTagSystemEvent))
                {
                    if (_Target != _GameplayTagSystemEvent.gameObject)
                    {
                        _Target = gameObject;
                    }
                }
                else
                {
                    Log($"{_Target.name} Is Not Has {_GameplayTagSystemEvent.GetType()}");
                }
            }
            else
            {
                _GameplayTagSystemEvent = null;
            }
        }
#endif

        private void Awake()
        {
            Setup();
        }

        private void OnEnable()
        {
            if (_AutoPlaying)
                OnListenGameplayTagEvent();

        }

        private void OnDisable()
        {
            EndListenGameplayEvent();
        }
        
        protected void Setup()
        {
            if (_GameplayTagSystemEvent is not null)
                return;

            if(_Target)
                _Target.TryGetComponentInParentOrChildren(out _GameplayTagSystemEvent);
        }

        public void SetTarget(GameObject gameObject)
        {
            var gameplayTagSystemEvent = gameObject.GetComponent<IGameplayTagSystemEvent>();

            SetGameplayTagSystemEvent(gameplayTagSystemEvent);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystemEvent = component.GetComponent<IGameplayTagSystemEvent>();

            SetGameplayTagSystemEvent(gameplayTagSystemEvent);
        }
        public void SetGameplayTagSystemEvent(IGameplayTagSystemEvent gameplayTagSystemEvent)
        {
            if (_GameplayTagSystemEvent is not null)
            {
                EndListenGameplayEvent();

                _Target = null;
            }

            _GameplayTagSystemEvent = gameplayTagSystemEvent;
            
            if (_GameplayTagSystemEvent is null)
                return;

            _Target = gameplayTagSystemEvent.gameObject;

            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.SetGameplayTagSystemEvent(_GameplayTagSystemEvent);
            }

            if (_AutoPlaying)
                OnListenGameplayTagEvent();
        }

        public void OnListenGameplayTagEvent()
        {
            if (_IsPlaying)
                return;

            _IsPlaying = true;

            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.OnGameplayTagEvent();
            }
        }
        public void EndListenGameplayEvent()
        {
            if (!_IsPlaying)
                return;

            _IsPlaying = false;

            foreach (GameplayTagEvent gameplayTagEvent in _GameplayEvents)
            {
                gameplayTagEvent.EndGameplayTagEvent();
            }
        }
    }
}
