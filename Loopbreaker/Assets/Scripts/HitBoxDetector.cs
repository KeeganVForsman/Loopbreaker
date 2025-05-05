using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxDetector : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{gameObject.name} collided with {collision.gameObject.name} (normal collision)");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} triggered with {other.gameObject.name} (normal collision)");
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"{gameObject.name} is still colliding with {collision.gameObject.name}");
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log($"{gameObject.name} is still triggering with {other.gameObject.name}");
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log($"{gameObject.name} stopped colliding with {collision.gameObject.name}");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{gameObject.name} stopped triggering with {other.gameObject.name}");
    }
}

