using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RunawayBride
{
    namespace Bridge
    {
        [RequireComponent(typeof(Collider), typeof(MeshRenderer))]
        public class BridgePart : MonoBehaviour
        {
            public Collider coll;
            public ParticleSystem waterSplash;

            [SerializeField] private GameObject _flowerCentre;
            [SerializeField] private Material _flowerCentreMaterial;

            private MeshRenderer _mesh;
            private Collider _trigger;
            private BridgeSpawner _spawner;
            private BridgeConstructor _constructor;
            private GameObject _character;
            private BridgePart _lastPart;


            private void Awake()
            {
                _mesh = GetComponent<MeshRenderer>();
                _trigger = GetComponent<Collider>();
                _spawner = BridgeSpawner.Instance;
                _constructor = BridgeConstructor.Instance;
                _constructor.onPartConstruct += ConstructPartHandler;

            }

            private void OnTriggerEnter(Collider other)
            {
                _character = other.gameObject;
                _constructor.onPartStep(1, this);
            }

            private void ConstructPartHandler(bool isPossible, BridgePart origin)
            {
                if (origin.GetHashCode() == this.GetHashCode())
                {
                    if (isPossible)
                    {
                        _lastPart = origin;
                        transform.position = new Vector3(_character.transform.position.x, transform.position.y, transform.position.z);
                        _mesh.sharedMaterial = _constructor.ActualColor.cornerMaterial;
                        coll.isTrigger = false;
                        ShowChadow(true);
                        _flowerCentre.GetComponent<MeshRenderer>().sharedMaterial = _flowerCentreMaterial;
                        Instantiate(waterSplash, this.transform.position, waterSplash.transform.rotation, this.transform);
                    }
                    else
                    {
                        if(_lastPart != null)
                            _lastPart.coll.isTrigger = true;
                    }
                }
            }

            public void ChangeMaterial(Material newMat)
            {
                _mesh.sharedMaterial = newMat;
            }

            public void ShowChadow(bool state)
            {
                if (state)
                    _mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                else
                    _mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
        }
    }
}