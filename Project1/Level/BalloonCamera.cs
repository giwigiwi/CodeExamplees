using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Patterns;


namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(CinemachineVirtualCamera))]
        public class BalloonCamera : Singleton<BalloonCamera>
        {
            private CinemachineVirtualCamera thisCamera;

            private void Start()
            {
                thisCamera = GetComponent<CinemachineVirtualCamera>();
            }

            public void SetBalloon(GameObject balloon)
            {
                thisCamera.Follow = balloon.transform;
                thisCamera.LookAt = balloon.transform;
            }

            private void StartLevelHandler(int level)
            {
                thisCamera.Follow = null;
                thisCamera.LookAt = null;
            }

        }
    }
}