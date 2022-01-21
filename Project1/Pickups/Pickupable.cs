using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Pickups
    {
        [RequireComponent(typeof(Collider), typeof(MeshRenderer))]
        public abstract class Pickupable : MonoBehaviour
        {
            public int amount = 1;
            public ParticleSystem pickupParticle;
            protected MeshRenderer mesh;
            

            protected virtual void Awake()
            {
                mesh = GetComponent<MeshRenderer>();
            }

            protected virtual void OnTriggerEnter(Collider other)
            {
                Instantiate(pickupParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }

        }
    }
}