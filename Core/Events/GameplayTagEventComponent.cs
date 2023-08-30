using UnityEngine;

using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemUtility.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/GameplayTagSystem Event Component", order: 10)]
    public class GameplayTagEventComponent : BaseMonoBehaviour
    {
        [Header(" [ GameplayTag Event Component ] ")]
        [SerializeField] private GameObject target;
        [SerializeField] private GameplayTagEvent[] gameplayEvents;
        [SerializeField] private bool autoPlaying = true;

        private IGameplayTagSystem gameplayTagSystemEvent;
        public bool IsPlaying { get; protected set; }

        private void Reset()
        {
#if UNITY_EDITOR
            Setup();
#endif
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (target)
            {
                if(target.TryGetComponentInParentOrChildren(out gameplayTagSystemEvent))
                {
                    if (target != gameplayTagSystemEvent.gameObject)
                    {
                        target = gameObject;
                    }
                }
                else
                {
                    Log($"{target.name} Is Not Has {gameplayTagSystemEvent.GetType()}");
                }
            }
            else
            {
                gameplayTagSystemEvent = null;
            }
#endif

        }
        private void Awake()
        {
            Setup();
        }

        private void OnEnable()
        {
            if (autoPlaying)
                OnListenGameplayTagEvent();
        }

        private void OnDisable()
        {
            EndListenGameplayEvent();
        }
        
        protected void Setup()
        {
            if (gameplayTagSystemEvent is not null)
                return;

            if(target)
                target.TryGetComponentInParentOrChildren(out gameplayTagSystemEvent);
        }

        public void SetTarget(GameObject gameObject)
        {
            var gameplayTagSystemEvent = gameObject.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystemEvent(gameplayTagSystemEvent);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystemEvent = component.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystemEvent(gameplayTagSystemEvent);
        }
        public void SetGameplayTagSystemEvent(IGameplayTagSystem gameplayTagSystemEvent)
        {
            if (this.gameplayTagSystemEvent is not null)
            {
                EndListenGameplayEvent();

                target = null;
            }

            this.gameplayTagSystemEvent = gameplayTagSystemEvent;
            
            if (this.gameplayTagSystemEvent is null)
                return;

            target = gameplayTagSystemEvent.gameObject;

            foreach (GameplayTagEvent gameplayTagEvent in gameplayEvents)
            {
                gameplayTagEvent.SetGameplayTagSystemEvent(this.gameplayTagSystemEvent);
            }

            if (autoPlaying)
                OnListenGameplayTagEvent();
        }

        public void OnListenGameplayTagEvent()
        {
            if (IsPlaying)
                return;

            IsPlaying = true;

            foreach (GameplayTagEvent gameplayTagEvent in gameplayEvents)
            {
                gameplayTagEvent.OnGameplayTagEvent();
            }
        }
        public void EndListenGameplayEvent()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;

            foreach (GameplayTagEvent gameplayTagEvent in gameplayEvents)
            {
                gameplayTagEvent.EndGameplayTagEvent();
            }
        }
    }
}
