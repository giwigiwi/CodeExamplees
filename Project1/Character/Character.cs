using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;



namespace RunawayBride
{
    namespace Character
    {
        [System.Serializable]
        public struct ClothesPart
        {
            public GameObject cloth;
            [HideInInspector]
            public SkinnedMeshRenderer Mesh => cloth.GetComponent<SkinnedMeshRenderer>();
            public int materialOrder;
            public Material DefaulMaterial
            {
                get
                {
                    return _defaulMaterial;
                }

            }
            [SerializeField]
            private Material _defaulMaterial;
        }

        [System.Serializable]
        internal struct FlowerConfig
        {
            public int counter;
            public Vector3 position;
            public Vector3 normal;
            public Material material;
            public Transform parent;
        }

        [RequireComponent(typeof(Animator))]
        public class Character : Singleton<Character>
        {
            public Vector3 startPosition;
            public Vector3 startRotation;
            public ClothesPart shirt;
            public ClothesPart feet;
            public GameObject hair;
            public HairFlower flowerPrefab;

            private Animator _animator;
            private Mesh _hairMesh;
            private Stack<HairFlower> _flowers;
            private Transform _balloon;
            private int[] _flowersNormal = { 189, 195, 198 };
            [SerializeField] private GameObject[] _flowerStartPoints;
            [SerializeField] private Vector3 _flowersOffset;


            public override void Awake()
            {
                base.Awake();
                _animator = GetComponent<Animator>();
                _hairMesh = hair.GetComponent<SkinnedMeshRenderer>().sharedMesh;
                _flowers = new Stack<HairFlower>();
            }

            private void Start()
            {
                GameStateHandler.Instance.onLevelStart += LevelStartHandler;
                GameStateHandler.Instance.onLevelRestart += LevelStartHandler;
                GameStateHandler.Instance.OnPortalEnter += PortalEnterHandler;
                GameStateHandler.Instance.onPlayerFail += FailHandler;
                GameStateHandler.Instance.onPlayerFinish += FinishHandler;
            }

            public void AddFlowerToHair(Material material)
            {
                FlowerConfig config = new FlowerConfig();
                config.counter = _flowers.Count;
                config.material = material;
                config.parent = _flowerStartPoints[config.counter % 3].transform;
                config.position = config.parent.position;
                config.normal = _hairMesh.normals[_flowersNormal[config.counter % 3]];
                config.position += _flowersOffset * (config.counter / 3);
                _flowers.Push(SpawnFlowerOnHair(config));
            }

            public void RemoveFlower()
            {
                _flowers.Pop().Die();
            }

            private HairFlower SpawnFlowerOnHair(FlowerConfig config)
            {
                GameObject flower = new GameObject(config.counter.ToString());
                flower.transform.position = config.position;
                flower.transform.SetParent(config.parent);
                flower.transform.up = config.normal;
                HairFlower leafs = Instantiate(flowerPrefab, flower.transform.position, flower.transform.rotation, flower.transform);
                leafs.GetComponentInChildren<MeshRenderer>().sharedMaterial = config.material;
                return leafs;
            }

            private void ClearFlowers()
            {
                if (_flowers.Count > 0)
                    foreach (HairFlower flower in _flowers)
                    {
                        flower.Die();
                    }
                _flowers.Clear();
            }

            private void PlayStopAnim(int animNumber)
            {
                _animator.SetInteger("Stop", animNumber);
            }

            private void FinishHandler()
            {
                PlayStopAnim(Random.Range(1, 3));
                gameObject.transform.position = _balloon.position;
                gameObject.transform.rotation = _balloon.rotation;
                transform.SetParent(_balloon);
            }

            private void FailHandler(FailReason reason)
            {
                if (reason == FailReason.obstacle)
                    _animator.SetInteger("DeathType", 1);
                else
                    _animator.SetInteger("DeathType", 2);
            }

            private void LevelStartHandler(int levelnumber)
            {
                transform.SetParent(null);
                SetClothesMaterial(shirt, shirt.DefaulMaterial);
                SetClothesMaterial(feet, feet.DefaulMaterial);
                transform.position = startPosition;
                transform.rotation = Quaternion.Euler(startRotation);
                _animator.SetInteger("DeathType", 0);
                _animator.SetInteger("Stop", 0);
                ClearFlowers();
            }

            public void SetBalloon(Transform position)
            {
                _balloon = position;
            }

            private void PortalEnterHandler(PortalData data)
            {
                SetClothesMaterial(shirt, data.shirtMaterial);
                SetClothesMaterial(feet, data.feetMaterial);
            }

            private void SetClothesMaterial(ClothesPart part, Material newMat)
            {
                Material[] newMats = part.Mesh.sharedMaterials;
                newMats[part.materialOrder] = newMat;
                SetClothesMaterial(part, newMats);
            }

            private void SetClothesMaterial(ClothesPart part, Material[] newMats)
            {
                part.Mesh.sharedMaterials = newMats;
            }

        }
    }
}