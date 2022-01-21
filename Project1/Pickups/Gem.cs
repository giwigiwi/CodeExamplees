using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace RunawayBride
{
    namespace Pickups
    {
        public class Gem : Pickupable
        {
            private GemController _controller;

            protected override void Awake()
            {
                base.Awake();
                _controller = GemController.Instance;
            }

            protected override void OnTriggerEnter(Collider other)
            {
                _controller.onGemPickup?.Invoke(amount);
                base.OnTriggerEnter(other);
            }

        }
    }
}