using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;
using Cinemachine;




namespace RunawayBride
{
    public class CameraSwitcher : Singleton<CameraSwitcher>
    {
        public CinemachineVirtualCamera followCamera;
        public CinemachineVirtualCamera balloonCamera;


        public override void Awake()
        {
            base.Awake();

            GameStateHandler.Instance.onPlayerFinish += FinishHandler;
            GameStateHandler.Instance.onLevelStart += StartLevelHandler;
            GameStateHandler.Instance.onLevelRestart += StartLevelHandler;
        }

        private void FinishHandler()
        {
            balloonCamera.gameObject.SetActive(false);
            balloonCamera.gameObject.SetActive(true);
        }

        private void StartLevelHandler(int level)
        {
            followCamera.gameObject.SetActive(false);
            followCamera.gameObject.SetActive(true);

        }


    }
}