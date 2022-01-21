using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    
    public class LocalGameObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _local;
        private static System.Object _lock = new System.Object();

        public virtual void Awake()
        {

            if (_local == null)
            {
                _local = gameObject.GetComponent<T>();
                //DontDestroyOnLoad(_local);
            }
            else
            {
                if (_local != this)
                {
                    Destroy(gameObject);
                    Debug.LogError("DOUBLE LOCAL");
                }
            }
        }

        public static T Local
        {
            get
            {

                // if (isApplicationQuitting)
                //     return null;

                lock (_lock)
                {
                    if (_local == null)
                    {
                        _local = FindObjectOfType<T>();

                        if (_local == null)
                        {
                            //var singleton = new GameObject("[SINGLETON] " + typeof(T));
                            //_local = singleton.AddComponent<T>();
                            //Debug.LogError("NOT LOCAL");
                        }
                        //DontDestroyOnLoad(_local);
                    }

                    return _local;
                }
            }
        }

    }
}
