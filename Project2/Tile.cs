using System.Collections.Generic;
using Lean.Transition;
using UnityEngine;
using Zenject;

namespace WayOfLove
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private bool _rotatable;
        [SerializeField] private Vector3 _rotationDegree = new Vector3(0, 90f, 0);
        [SerializeField] private TileType _type;
        private readonly List<Vector3> _correctRotations = new List<Vector3>();
        private PatternInspector _inspector;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(PatternInspector inspector, SignalBus signalBus)
        {
            _inspector = inspector;
            _signalBus = signalBus;
        }

        private void Start()
        {
            if (_rotatable)
            {
                _correctRotations.Add(transform.forward);
                if (_type == TileType.DoubleRightSide)
                    _correctRotations.Add(-transform.forward);
            }
        }

        public void Rotate()
        {
            if (!_rotatable) return;
            
            _rotatable = false;
            var newRotation = _rotationDegree + transform.rotation.eulerAngles;
            var position = transform.position;
            transform.localScaleTransition(new Vector3(1.2f, 1, 1.2f), 0.1f).JoinTransition()
                .positionTransition_y(position.y + 0.2f, 0.1f).JoinTransition()
                .rotationTransition(Quaternion.Euler(newRotation), 0.2f, LeanEase.QuintInOut).JoinTransition()
                .localScaleTransition(new Vector3(1f, 1, 1f), 0.1f).JoinTransition()
                .positionTransition_y(position.y, 0.1f)
                .JoinTransition().EventTransition(() =>
                {
                    _rotatable = true;
                    _signalBus.TryFire<TileRotatedSignal>();
                });
        }

        public void Shambles(int cycles)
        {
            if (_rotatable) FastRotate(_rotationDegree * cycles);
        }

        public bool CheckRotation()
        {
            if (!_rotatable) return true;
            if (_type == TileType.AnySide) return true;
            foreach (var rotation in _correctRotations)
                if (Vector3.Angle(transform.forward, rotation) >= 1)
                    continue;
                else
                    return true;
            return false;
        }

        private void FastRotate(Vector3 rotation)
        {
            var newRotation = rotation + transform.rotation.eulerAngles;
            transform.rotationTransition(Quaternion.Euler(newRotation), 0.2f, LeanEase.QuintInOut);
        }
    }
}