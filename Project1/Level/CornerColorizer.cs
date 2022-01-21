using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Level
    {
        [RequireComponent(typeof(MeshRenderer))]
        public class CornerColorizer : MonoBehaviour
        {
            [SerializeField]
            private Material _defMaterial;
            [SerializeField]
            private MeshRenderer _mesh;

            private void Awake()
            {
                _mesh = GetComponent<MeshRenderer>();
                GameStateHandler.Instance.OnPortalEnter += PortalEnterHandler;
                GameStateHandler.Instance.onLevelStart += CleanUpHandler;
                GameStateHandler.Instance.onLevelRestart += CleanUpHandler;
            }

            private void PortalEnterHandler(PortalData data)
            {
                _mesh.sharedMaterial = data.cornerMaterial;
            }

            private void CleanUpHandler(int level)
            {
                GameStateHandler.Instance.OnPortalEnter -= PortalEnterHandler;
            }
        }
    }
}