using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float moveSpeed;
    void Start()
    {
        Rigidbody rigidBodyOfBolt = GetComponent<Rigidbody>();
        rigidBodyOfBolt.velocity = transform.forward * moveSpeed;
    }
}
