using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(Collider), typeof(Animator))]
        public class JumpPad : MonoBehaviour
        {
            public Vector3 direction;
            public float power;

            private Animator _animator;

            private void Awake()
            {
                _animator = GetComponent<Animator>();
            }

            private void OnTriggerEnter(Collider other)
            {
                other.GetComponent<Rigidbody>().AddForce(direction * power, ForceMode.Acceleration);
                _animator.Play("FBX_Temp");
            }

        }
    }
}
