using System;
using UnityEngine;
using Zenject;

namespace WayOfLove
{
    public class CharacterAnimatorController : MonoBehaviour
    {

        [SerializeField] private Animator _animator;
        private static readonly int Start = Animator.StringToHash("Start");
        private static readonly int Finish = Animator.StringToHash("Finish");
        private PatternInspector _inspector;
        
        [Inject]
        private void Construct(PatternInspector inspector)
        {
            _inspector = inspector;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _inspector.OnLevelComplete += StartRun;
        }

        private void StartRun()
        {
            _animator.SetTrigger(Start);
        }

        public void FinishRun()
        {
            _animator.SetTrigger(Finish);
        }

        private void OnDestroy()
        {
            _inspector.OnLevelComplete -= StartRun;
        }
    }
}