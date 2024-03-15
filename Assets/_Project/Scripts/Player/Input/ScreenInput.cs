using System;
using UnityEngine;
using Zenject;

namespace Player.Input
{
    public class ScreenInput : MonoBehaviour
    {
        [SerializeField] private Canvas screenInputCanvas;

        [Inject]
        private void Construct(IPlayerInput input)
        {
            input.Active.ValueChanged += InputActiveStateChanged;
        }

        private void InputActiveStateChanged(bool value)
        {
// #if !UNITY_EDITOR
            screenInputCanvas.enabled = value;
// #endif
        }

        private void Awake()
        {
            
            
// #if UNITY_EDITOR
//             screenInputCanvas.enabled = false;
// #endif
        }
    }
}