using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace RunawayBride.Level
{
    public class DoubleDoorActivator : MonoBehaviour
    {

        public UnityEvent ActiveDoor;
        public PortalData portalData;

        private void OnTriggerEnter(Collider other)
        {
            ActiveDoor?.Invoke();
            GameStateHandler.Instance.OnPortalEnter?.Invoke(portalData);
        }

    }
}