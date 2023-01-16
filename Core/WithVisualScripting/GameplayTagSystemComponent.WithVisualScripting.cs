using UnityEngine;
using System.Diagnostics;

#if SCOR_ENABLE_VISUALSCRIPTING
using Unity.VisualScripting;
#endif

namespace StudioScor.GameplayTagSystem
{
    public partial class GameplayTagSystemComponent : MonoBehaviour
    {
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void TriggerTagWithVisualScripting(GameplayTag triggerTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.TRIGGER_TAG, gameObject, triggerTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void AddOwnedTagWithVisualScripting(GameplayTag addOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_OWNED_TAG, gameObject, addOwnedTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void SubtractOwnedTagWithVisualScripting(GameplayTag removeOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.SUBTRACT_OWNED_TAG, gameObject, removeOwnedTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void AddNewOwnedTagWithVisualScripting(GameplayTag addNewOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_NEW_OWNED_TAG, gameObject, addNewOwnedTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void RemoveOwnedTagWithVisualScripting(GameplayTag removeOwnedTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.REMOVE_OWNED_TAG, gameObject, removeOwnedTag);
#endif
        }


        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void AddBlockTagWithVisualScripting(GameplayTag addBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_BLOCK_TAG, gameObject, addBlockTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void SubtractBlockTagWithVisualScripting(GameplayTag removeBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.SUBTRACT_BLOCK_TAG, gameObject, removeBlockTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void AddNewBlockTagWithVisualScripting(GameplayTag addNewBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.ADD_NEW_BLOCK_TAG, gameObject, addNewBlockTag);
#endif
        }
        [Conditional("SCOR_ENABLE_VISUALSCRIPTING")]
        protected virtual void RemoveBlockTagWithVisualScripting(GameplayTag removeBlockTag)
        {
#if SCOR_ENABLE_VISUALSCRIPTING
            EventBus.Trigger(GameplayTagSystemVisualScriptingEvent.REMOVE_BLOCK_TAG, gameObject, removeBlockTag);
#endif
        }
    }
}
