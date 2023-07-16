

using UnityEngine;
using UnityPhysics;

public class pushable : MonoBehaviour


{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _rigidbody.AddForce(collision.contactNormal * 100);
        }
    }
}