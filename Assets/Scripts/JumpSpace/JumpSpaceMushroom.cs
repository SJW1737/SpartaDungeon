using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSpaceMushroom : MonoBehaviour
{
    public float jumpForce = 10f;
    public bool resetYVelocity = true;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody rb = other.attachedRigidbody;
        if (rb == null) return;

        if (resetYVelocity)
        {
            Vector3 v = rb.velocity;
            v.y = 0f;
            rb.velocity = v;
        }

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
