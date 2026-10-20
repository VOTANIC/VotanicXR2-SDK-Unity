using System;
using UnityEngine;
using System.Reflection;
using Votanic.VXR2.Core.Input;

namespace Votanic.VXR2.Sample
{
    /// <summary>
    /// Sample for using Action Binding for VXR2.
    /// It delegate a method to an input by a action name defined in VXRInputAsset,
    /// after the action is trigged, it callthe delegated method.
    /// </summary>
    [Obfuscation(Exclude = true)]
    public class SampleInteractionBinding : MonoBehaviour
    {
        /// <summary>
        /// Action name on input action by VXR2.
        /// </summary>
        private readonly string triggerActionName = "CustomTrigger";
        /// <summary>
        /// Action binding 
        /// </summary>
        private VXRActionBinding<bool> _buttonInput;

        void Start()
        {
            _buttonInput = new VXRActionBinding<bool>(triggerActionName, ButtonAction);
        }

        /// <summary>
        /// Enable/Disable/Dispose the action binding to reduce cost from input manager.
        /// </summary>
        private void OnEnable()
        {
            _buttonInput.Bound = true;
        }
        private void OnDisable()
        {
            _buttonInput.Bound = false;
        }
        private void OnDestroy()
        {
            _buttonInput.Dispose();
        }

        /// <summary>
        /// Customize action delegate to the input.
        /// </summary>
        private void ButtonAction(bool value)
        {
            if (value)
            {
                Debug.Log($"Button pressed down");
            }
            else
            {
                Debug.Log($"Button released");
            }
        }
    }
}