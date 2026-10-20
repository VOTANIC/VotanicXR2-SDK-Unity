using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Votanic.VXR2.Core.Input;
using Votanic.VXR2.Core.Player;
using System.Reflection;

namespace Votanic.VXR2.Sample
{
    /// <summary>
    /// This is a sample 
    /// </summary>
    [Obfuscation(Exclude = true)]
    public class InteractorControl : MonoBehaviour
    {
        private readonly string triggerActionName = "NextInteractor";

        private VXRActionBinding<bool> _buttonInput;
        private List<XRBaseInteractor> interactors;
        private int index = -1;
        private VXRHandControllerHandler controllerHandler;
        IEnumerator Start()
        {
            yield return new WaitUntil(() => VXRPlayer.Players.Count > 0);
            _buttonInput = new VXRActionBinding<bool>(triggerActionName, ButtonAction);
            controllerHandler = VXRPlayer.Players.First().RightController.ControllerHandler;
            interactors = controllerHandler.GetInteractors().ToList();
            controllerHandler.ResetInteractors();
            MoveToNextInteractor();
        }

        private void OnEnable()
        {
            if (_buttonInput != null) _buttonInput.Bound = true;
        }
        private void OnDisable()
        {
            if (_buttonInput != null) _buttonInput.Bound = false;
        }
        private void OnDestroy()
        {
            if (_buttonInput != null) _buttonInput.Dispose();
        }

        private void ButtonAction(bool value)
        {
            if (value) MoveToNextInteractor();
        }

        private void MoveToNextInteractor()
        {
            index++;
            index %= interactors.Count;
            controllerHandler.ResetInteractors();
            controllerHandler.SetInteractorActive(interactors[index], true);
        }
    }
}