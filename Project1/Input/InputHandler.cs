using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using Patterns;



namespace RunawayBride
{
    namespace Input
    {
        public enum InputMode { Enabled, Disabled }

        public class InputHandler : Singleton<InputHandler>
        {
            public InputMode InputMode => _inputMode;
            [SerializeField] private InputMode _inputMode;


            public override void Awake()
            {
                base.Awake();
                LeanTouch.OnFingerDown += HandleFingerDown;
                LeanTouch.OnFingerUp += HandleFingerUp;
                GameStateHandler.Instance.onPlayerFail += FailHandler;
                GameStateHandler.Instance.onPlayerFinish += DisableInput;
                GameStateHandler.Instance.onLevelRestart += EnableInput;
                GameStateHandler.Instance.onLevelStart += EnableInput;
            }

            private void HandleFingerDown(LeanFinger finger)
            {
                if (_inputMode == InputMode.Enabled)
                    PlayerInput.Instance.ToggleInput(finger);
            }

            private void HandleFingerUp(LeanFinger finger)
            {
                PlayerInput.Instance.ToggleInput(null);
            }

            public void SetInputMode(InputMode mode)
            {
                _inputMode = mode;
            }

            private void FailHandler(FailReason reason)
            {
                DisableInput();
            }

            private void DisableInput()
            {
                SetInputMode(InputMode.Disabled);
            }

            private void EnableInput(int level)
            {
                SetInputMode(InputMode.Enabled);
            }
        }

    }
}
