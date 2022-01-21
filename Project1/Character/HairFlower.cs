using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Character
    {
        [RequireComponent(typeof(MeshRenderer))]
        public class HairFlower : MonoBehaviour
        {

            private MeshRenderer _meshRenderer;

            public void Die()
            {
                GameStateHandler.Instance.OnPortalEnter -= OnPortalEnter;
                Destroy(transform.parent.gameObject);
            }

            private void Start()
            {
                _meshRenderer = GetComponent<MeshRenderer>();
                GameStateHandler.Instance.OnPortalEnter += OnPortalEnter;
            }

            private void OnPortalEnter(PortalData data)
            {
                _meshRenderer.sharedMaterial = data.flowerMaterial;
            }
        }
    }
}