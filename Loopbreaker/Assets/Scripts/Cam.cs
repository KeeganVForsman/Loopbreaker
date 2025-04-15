using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;

        void LateUpdate()
        {
            if (target != null)
                transform.position = target.position + offset;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
