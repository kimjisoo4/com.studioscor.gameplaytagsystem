using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{
    [DefaultExecutionOrder(GameplayTagSystemUtility.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Trigger GameplayTagComponent", order: 10)]
    public class TriggerGameplayTagComponent : BaseMonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameObject target;
        [Space(5f)]
        [SerializeField] private GameplayTag[] triggerTags;

        [Header(" [ Auto Playing ] ")]
        [SerializeField] private bool autoPlaying;

        private IGameplayTagSystem gameplayTagSystem;

#if UNITY_EDITOR
        private void Reset()
        {
            Setup();
        }

        private void OnValidate()
        {
            if (target)
            {
                if (target.TryGetComponentInParentOrChildren(out gameplayTagSystem))
                {
                    if (target != gameplayTagSystem.gameObject)
                    {
                        target = gameplayTagSystem.gameObject;
                    }
                }
                else
                {
                    target = null;
                }
            }
        }
#endif

        private void Awake()
        {
            Setup();
        }
        private void OnEnable()
        {
            if (autoPlaying)
                OnTriggerTag();
        }

        private void Setup()
        {
            if (gameplayTagSystem is not null)
                return;

            if (gameObject.TryGetComponentInParentOrChildren(out gameplayTagSystem))
            {
                target = gameplayTagSystem.gameObject;
            }
        }

        public void SetTarget(GameObject gameObject)
        {
            var gameplayTagSystemEvent = gameObject.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystem(gameplayTagSystemEvent);
        }
        public void SetTarget(Component component)
        {
            var gameplayTagSystemEvent = component.GetComponent<IGameplayTagSystem>();

            SetGameplayTagSystem(gameplayTagSystemEvent);
        }
        public void SetGameplayTagSystem(IGameplayTagSystem gameplayTagSystem)
        {
            target = null;

            this.gameplayTagSystem = gameplayTagSystem;

            if (this.gameplayTagSystem is not null)
            {
                target = this.gameplayTagSystem.gameObject;
            }
        }

        public void OnTriggerTag(GameplayTag triggerTag)
        {
            Log("On Trigger GameplayTags");

            if (gameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            gameplayTagSystem.TriggerTag(triggerTag);
        }

        public void OnTriggerTag()
        {
            Log("On Trigger GameplayTags");

            if (gameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            gameplayTagSystem.TriggerTags(triggerTags);
        }
    }
}
