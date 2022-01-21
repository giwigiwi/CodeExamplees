using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunawayBride.Input;
using Patterns;



namespace RunawayBride
{
    namespace Character
    {
        [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
        public class CharacterMover : Singleton<CharacterMover>
        {
            public float defaultSpeed;
            public float intensity;
            public float sideRange;
            public float maxVelocity;
            public float fallSpeed;

            private Rigidbody _rigidBody;
            private Animator _animator;
            private Collider _coll;
            private float _speed;
            private bool _isGrounded;
            private bool _isRun = false;
            private Vector3 _velocity;
            private float _groundDistance = 0.1f;

            public override void Awake()
            {
                base.Awake();
                _rigidBody = GetComponent<Rigidbody>();
                _animator = GetComponent<Animator>();
                _coll = GetComponent<Collider>();
                _animator.SetFloat("Speed", 0);
            }

            private void Start()
            {
                PlayerInput.Instance.OnInputProcess += HandlePlayerInput;
                PlayerInput.Instance.OnFinishInput += HandleFinishInput;
                GameStateHandler.Instance.onLevelStart += StartLevelHandler;
                GameStateHandler.Instance.onLevelRestart += StartLevelHandler;
                GameStateHandler.Instance.onPlayerFinish += StopCharacter;
                GameStateHandler.Instance.onPlayerFail += FailHandler;
            }

            private void StartLevelHandler(int levelnumber)
            {
                StopCharacter();
                _rigidBody.velocity = _velocity;
            }

            private void StopCharacter()
            {
                _velocity = Vector3.zero;
                _animator.SetFloat("Speed", 0);
                _isRun = false;
                _speed = 0;
                _rigidBody.Sleep();
            }

            private void FailHandler(FailReason reason)
            {
                StopCharacter();
            }

            private void Update()
            {
                if (InputHandler.Instance.InputMode == InputMode.Enabled && _isRun)
                {
                    CheckGround();
                    Move();
                    if (_speed < defaultSpeed)
                        _speed += 6f * Time.deltaTime;
                    _animator.SetFloat("Speed", _speed);
                    if (Mathf.Abs(transform.position.x + (_velocity.x * Time.deltaTime)) > sideRange)
                    {
                        transform.position = new Vector3(Mathf.Sign(transform.position.x) * sideRange, transform.position.y, transform.position.z);
                        _velocity.x = 0;
                    }
                }
            }

            private void CheckGround()
            {
                if (Physics.Raycast(transform.position + new Vector3(0f, 0.05f, 0f), new Vector3(0f, -_groundDistance, 0f), _groundDistance))
                {
                    _isGrounded = true;
                    _coll.material.dynamicFriction = 1;
                    _coll.material.staticFriction = 1;
                    _animator.SetBool("isGrounded", _isGrounded);
                }
                else
                {
                    _isGrounded = false;
                    _coll.material.dynamicFriction = 0;
                    _coll.material.staticFriction = 0;
                    _animator.SetBool("isGrounded", _isGrounded);
                }
            }

            private void Move()
            {
                _velocity.z = 1 * _animator.GetFloat("Speed");
                if (_isGrounded)
                    _rigidBody.velocity = _velocity;
                else
                    _rigidBody.velocity = Vector3.down * fallSpeed;
            }

            private void HandlePlayerInput(Vector2 screenDelta)
            {
                _velocity.x = Mathf.Clamp(screenDelta.x * intensity, -maxVelocity, maxVelocity);
                _isRun = true;
            }

            private void HandleFinishInput()
            {
                _velocity.x = 0;
            }
        }
    }
}