using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Pickups
    {
        public class Flower : Pickupable
        {
            private FlowerController controller;

            protected override void Awake()
            {
                base.Awake();
                controller = FlowerController.Instance;
            }

            protected override void OnTriggerEnter(Collider other)
            {
                controller.onFlowerPickup?.Invoke(amount, mesh.sharedMaterial);
                base.OnTriggerEnter(other);
            }

        }
    }
}
