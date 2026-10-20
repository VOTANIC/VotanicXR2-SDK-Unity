using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using System.Reflection;
using Votanic.VXR2.Core.Input;

namespace Votanic.VXR2.Sample
{
    /// This sample interactor is not included in MVP.
    /// 
    /// <summary>
    /// </summary>
    [Obfuscation(Exclude = true)]
    [RequireComponent(typeof(XRRayInteractor))]
    public class SampleInteractor : MonoBehaviour
    {
        private readonly string triggerActionName = "CustomTrigger";

        private XRRayInteractor _interactor;
        private void Awake()
        {
            _interactor = GetComponent<XRRayInteractor>();
            _interactor.selectInput.inputSourceMode = XRInputButtonReader.InputSourceMode.ManualValue;

        }
        private VXRActionBinding<bool> _buttonInput;

        void Start()
        {
            _buttonInput = new VXRActionBinding<bool>(triggerActionName, ButtonAction);
        }

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
        /// Customize action delegate mappto the input.
        /// </summary>
        private void ButtonAction(bool value)
        {
            _interactor.selectInput.QueueManualState(value, 0);
        }
    }
}