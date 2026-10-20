using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Votanic.VXR2.Core.Player;
using System.Linq;
using System.Reflection;
using Votanic.VXR2.Core;
using System.Collections;

namespace Votanic.VXR2.Sample
{
    [Obfuscation(Exclude = true)]
    public class ToolsSelector : MonoBehaviour
    {
        [SerializeField] private Dropdown _selectToolDropdown;

        private List<XRBaseInteractor> interactors;
        private VXRHandControllerHandler controllerHandler;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => VXRObjectManager.Instance != null && VXRObjectManager.Instance.Initialized);
            controllerHandler = VXRPlayer.Players.First().RightController.ControllerHandler;
            interactors = controllerHandler.GetInteractors().ToList();
            controllerHandler.ResetInteractors();
            controllerHandler.SetInteractorActive(interactors[0], true);
            SetupToolsDropdown();
            SetupCallbacks();
        }

        private void SetupCallbacks()
        {
            _selectToolDropdown.onValueChanged.AddListener(OnSelectToolDropdownValueChanged);
        }

        private void SetupToolsDropdown()
        {
            _selectToolDropdown.options.Clear();
            foreach (var interactor in interactors)
            {
                _selectToolDropdown.options.Add(new Dropdown.OptionData(interactor.name));
            }
        }

        public void OnSelectToolDropdownValueChanged(int value)
        {
            controllerHandler.ResetInteractors();
            controllerHandler.SetInteractorActive(interactors[value], true);
        }
    }
}