using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RunawayBride.UI;



namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(Collider))]
        public class PortalTrigger : MonoBehaviour
        {

            [SerializeField] 
            private PortalData portalInfo;

            private void OnTriggerEnter(Collider other)
            {
                GameStateHandler.Instance.OnPortalEnter?.Invoke(portalInfo);
            }
        }
    }
}

