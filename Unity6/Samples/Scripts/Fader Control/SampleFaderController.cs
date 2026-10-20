using System;
using UnityEngine;
using UnityEngine.UI;
using Votanic.VXR2.Core.Fader;

namespace Votanic.VXR2.Sample
{
    [RequireComponent(typeof(Toggle))]
    public class SampleFaderController : MonoBehaviour
    {
        private Toggle toggle;

        void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggleFader);
        }

        void OnDisable()
        {
            toggle.onValueChanged.RemoveListener(OnToggleFader);
        }

        void OnToggleFader(bool isOn)
        {
            if (isOn)
            {
                WorldFader.Fade(Color.black, .75f, 1f);
            }
            else
            {
                WorldFader.Fade(Color.black, 0f, 1f);
            }
        }
    }
}
