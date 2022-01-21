using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(Rigidbody))]
        public class BaloonMover : MonoBehaviour
        {
            public float defaultSpeed = 3;
            public float speedupTime = 3;
            public Transform girlPosition;

            private Rigidbody _rb;
            private float startTime = 0;


            private void Awake()
            {
                GameStateHandler.Instance.onPlayerFinish += FinishHandler;
                _rb = GetComponent<Rigidbody>();
            }

            private void Start()
            {
                Character.Character.Instance.SetBalloon(girlPosition);
                BalloonCamera.Instance.SetBalloon(this.gameObject);
            }

            private void Update()
            {
                if (startTime != 0)
                    _rb.velocity = Vector3.Lerp(Vector3.zero, new Vector3(0, defaultSpeed, 0), (Time.time - startTime) / speedupTime);
            }

            private void FinishHandler()
            {
                startTime = Time.time;
            }
        }
    }
}