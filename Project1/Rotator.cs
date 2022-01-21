using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WhereIsMyFood
{
    public class Rotator : MonoBehaviour
    {
        public bool isRotate;
        public Vector3 rotationAxis;
        public float speed;
        public bool isFloating;
        public float floatingSpeed;
        public float floatingAmplitude;

        private Vector3 _startPosition;

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (isRotate)
                transform.Rotate(rotationAxis * speed * Time.deltaTime);
            if (isFloating)
                transform.position = _startPosition + new Vector3(0f,
                                                //transform.position.y + 
                                                Mathf.Sin(Time.fixedTime * Mathf.PI * floatingSpeed) * floatingAmplitude,
                                                0f);
        }
    }
}