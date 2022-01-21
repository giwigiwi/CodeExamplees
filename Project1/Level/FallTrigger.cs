using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(Collider))]
        public class FallTrigger : MonoBehaviour
        {
            [SerializeField] private GameObject target;

            private void Update()
            {
                transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            }

            private void OnTriggerEnter(Collider other)
            {
                GameStateHandler.Instance.onPlayerFail?.Invoke(FailReason.bridge);
            }

        }
    }
}