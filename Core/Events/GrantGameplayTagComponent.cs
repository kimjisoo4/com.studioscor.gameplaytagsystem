using UnityEngine;
using StudioScor.Utilities;

namespace StudioScor.GameplayTagSystem
{

    [DefaultExecutionOrder(GameplayTagSystemExecutionOrder.SUB_ORDER)]
    [AddComponentMenu("StudioScor/GameplayTagSystem/Grant GameplayTagComponent", order:10)]
    public class GrantGameplayTagComponent : BaseMonoBehaviour
    {
        [Header(" [ Grant GameplayTag Component ] ")]
        [SerializeField] private GameObject target;
        [Space(5f)]
        [SerializeField] private FGameplayTags toggleTags;

        [Header(" [ Auto Playing ] ")]
        [SerializeField] private bool autoPlaying;

        public bool IsPlaying { get; protected set; }

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
                if(target.TryGetComponentInParentOrChildren(out gameplayTagSystem))
                {
                    if(target != gameplayTagSystem.gameObject)
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
                OnToggleGameplayTag();
        }
        private void OnDisable()
        {
            EndToggleGameplayTag();
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
            if (this.gameplayTagSystem is not null && IsPlaying)
            {
                EndToggleGameplayTag();

                target = null;
            }

            this.gameplayTagSystem = gameplayTagSystem;

            if(this.gameplayTagSystem is not null)
            {
                target = this.gameplayTagSystem.gameObject;

                if (IsPlaying)
                    OnToggleGameplayTag();
            }
        }

        public void OnToggleGameplayTag()
        {
            if (IsPlaying)
                return;

            IsPlaying = true;

            Log("On Toggle GameplayTags");

            if (gameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            gameplayTagSystem.AddOwnedTags(toggleTags.Owneds);
            gameplayTagSystem.AddBlockTags(toggleTags.Blocks);
        }

        public void EndToggleGameplayTag()
        {
            if (!IsPlaying)
                return;

            IsPlaying = false;

            Log("End Toggle GameplayTags");

            if (gameplayTagSystem is null)
            {
                Log("GamepalyTag System Is Null", true);

                return;
            }

            gameplayTagSystem.RemoveOwnedTags(toggleTags.Owneds);
            gameplayTagSystem.RemoveBlockTags(toggleTags.Blocks);
        }
    }
}
