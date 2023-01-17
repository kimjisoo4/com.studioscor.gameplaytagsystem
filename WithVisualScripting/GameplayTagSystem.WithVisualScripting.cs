using UnityEngine;

#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
#endif

namespace StudioScor.GameplayTagSystem
{
    public partial class GameplayTagSystem : MonoBehaviour
    {
        partial void Callback_OnTriggerTag_VisualScripting(GameplayTag triggerTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.TRIGGER_TAG, gameObject, triggerTag);
#endif
        }

        partial void Callback_OnAddedOwnedTag_VisualScripting(GameplayTag addOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_OWNED_TAG, gameObject, addOwnedTag);
#endif
        }

        partial void Callback_OnSubtractedOwnedTag_VisualScripting(GameplayTag removeOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.SUBTRACT_OWNED_TAG, gameObject, removeOwnedTag);
#endif
        }

        partial void Callback_OnGrantedOwnedTag_VisualScripting(GameplayTag addNewOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.GRANT_OWNED_TAG, gameObject, addNewOwnedTag);
#endif
        }

        partial void Callback_OnRemovedOwnedTag_VisualScripting(GameplayTag removeOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.REMOVE_OWNED_TAG, gameObject, removeOwnedTag);
#endif
        }


        partial void Callback_OnAddedBlockTag_VisualScripting(GameplayTag addBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_BLOCK_TAG, gameObject, addBlockTag);
#endif
        }
        partial void Callback_OnSubtractedBlockTag_VisualScripting(GameplayTag removeBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.SUBTRACT_BLOCK_TAG, gameObject, removeBlockTag);
#endif
        }
        partial void Callback_OnGrantedBlockTag_VisualScripting(GameplayTag addNewBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.GRANT_BLOCK_TAG, gameObject, addNewBlockTag);
#endif
        }
        partial void Callback_OnRemovedBlockTag_VisualScripting(GameplayTag removeBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.REMOVE_BLOCK_TAG, gameObject, removeBlockTag);
#endif
        }
    }
}