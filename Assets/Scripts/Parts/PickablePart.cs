using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickablePart : MonoBehaviour
{
    public bool isTaking = false;
    public Transform targetTransform;

    void Update()
    {
        if (isTaking)
        {
            transform.position = Vector3.Slerp(transform.position, targetTransform.position, 0.01f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickCollider"))
        {
            isTaking = true;
            targetTransform = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PickCollider"))
        {
            isTaking = false;
            targetTransform = null;
        }
    }
}
