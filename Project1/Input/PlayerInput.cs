using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using Lean.Touch;
using UnityEngine;
using UnityEngine.Events;



namespace RunawayBride
{
    namespace Input
    {
        public class PlayerInput : Singleton<PlayerInput>
        {
            public UnityAction<Vector2> OnStartInput;
            public UnityAction OnFinishInput;
            public UnityAction<Vector2> OnInputProcess;
            private Camera mainCamera => Camera.main;


            private LeanFinger _finger;

            private void Start() 
            {
                GameStateHandler.Instance.onPlayerFinish += FinishInput;
                GameStateHandler.Instance.onPlayerFail += FailHandler;
            }


            private void StartInput(LeanFinger finger)
            {
                OnStartInput?.Invoke(finger.StartScreenPosition);
            }

            private void FinishInput()
            {
                _finger = null;
                OnFinishInput?.Invoke();
            }

            private void FailHandler(FailReason resaon)
            {
                FinishInput();
            }

            private void InputProcess(LeanFinger finger)
            {
                OnInputProcess?.Invoke(finger.StartScreenPosition);
            }

            private void InputProcess(Vector2 vector)
            {
                OnInputProcess?.Invoke(vector);
            }

            public void ToggleInput(LeanFinger finger)
            {
                if (finger != null)
                {
                    _finger = finger;
                    StartInput(finger);
                }
                else
                    FinishInput();
            }

            private void Update()
            {
                if (_finger != null)
                    InputProcess(GetScreenDelta());
            }

            private Vector2 GetScreenDelta()
            {
                // // Calculate the screenDelta value based on these fingers
                // var screenDelta = LeanGesture.GetScreenDelta(LeanTouch.Fingers);
                // var screenPoint = mainCamera.WorldToScreenPoint(transform.position);
                // // Add the deltaPosition
                // screenPoint += (Vector3)screenDelta;
                // // Convert back to world space.
                // return mainCamera.ScreenToWorldPoint(screenPoint);
                return LeanTouch.Fingers[0].ScreenDelta;
            }


        }
    }
}