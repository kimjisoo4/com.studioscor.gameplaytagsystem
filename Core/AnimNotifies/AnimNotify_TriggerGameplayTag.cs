using StudioScor.Utilities;
using UnityEngine;

namespace StudioScor.GameplayTagSystem
{
    public class AnimNotify_TriggerGameplayTag : AnimNotifyBehaviour
    {
        [Header(" [ Trigger Gameplay Tag ] ")]
        [SerializeField] private GameplayTag[] _triggerTags;

        protected override void OnNotify(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var gameplayTagSystem = animator.transform.parent.GetGameplayTagSystem();

            gameplayTagSystem.TriggerTags(_triggerTags);
        }
    }
}
