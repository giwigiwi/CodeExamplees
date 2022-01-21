using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(Collider))]
        public class Obstacle : MonoBehaviour
        {

            private void OnCollisionEnter(Collision other)
            {
                GameStateHandler.Instance.onPlayerFail?.Invoke(FailReason.obstacle);
            }

        }
    }
}
