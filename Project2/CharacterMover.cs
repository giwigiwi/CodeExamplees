using System;
using Dreamteck.Splines;
using UnityEngine;
using Zenject;

namespace WayOfLove
{
    [RequireComponent(typeof(SplineFollower))]
    public class CharacterMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 1.5f;
        [SerializeField] private ParticleSystem _heartStream;
        private SplineFollower _follower;
        private PatternInspector _inspector;

        [Inject]
        private void Construct(PatternInspector inspector)
        {
            _inspector = inspector;
        }
        
        private void Start()
        {
            _follower = GetComponent<SplineFollower>();
            _inspector.OnLevelComplete += OnLevelComplete;
        }

        private void OnLevelComplete()
        {
            _follower.followSpeed = _speed;
            _heartStream.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            _inspector.OnLevelComplete -= OnLevelComplete;
        }
    }
}