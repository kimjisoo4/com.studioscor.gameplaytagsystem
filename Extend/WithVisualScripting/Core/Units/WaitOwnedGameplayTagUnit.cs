#if SCOR_ENABLE_VISUALSCRIPTING
using System;
using System.Linq;
using Unity.VisualScripting;

namespace StudioScor.GameplayTagSystem.VisualScripting
{
    [UnitTitle("Wait Contain All OwnedTags")]
    [UnitSubtitle("GameplayTagSystem Task")]
    [UnitCategory("Time\\StudioScor\\GameplayTagSystem")]
    public class WaitOwnedGameplayTagUnit : WaitToggleGameplayTagUnit
    {
        protected override void AddEvent()
        {
            _GameplayTagSystemComponent.OnGrantedOwnedTag += WaitGameplayTagUnit_OnChangedOwnedTag;
            _GameplayTagSystemComponent.OnRemovedOwnedTag += WaitGameplayTagUnit_OnChangedOwnedTag;
        }

        protected override void TryCheckToggle()
        {
            bool containAllTags = _GameplayTagSystemComponent.ContainAllTagsInOwned(_GameplayTags);

            if (containAllTags != _IsToggleOn)
            {
                _IsTrigger = true;

                _IsToggleOn = containAllTags;
            }
        }

        protected override void RemoveEvent()
        {
            _GameplayTagSystemComponent.OnGrantedOwnedTag -= WaitGameplayTagUnit_OnChangedOwnedTag;
            _GameplayTagSystemComponent.OnRemovedOwnedTag -= WaitGameplayTagUnit_OnChangedOwnedTag;
        }

        private void WaitGameplayTagUnit_OnChangedOwnedTag(GameplayTagSystemComponent gameplayTagSystemComponent, GameplayTag gameplayTag)
        {
            if (_GameplayTags.Contains(gameplayTag))
            {
                TryCheckToggle();
            }
        }
    }
}

#endif