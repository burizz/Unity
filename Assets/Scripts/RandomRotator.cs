using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {

    // multiplier for Asteroid rotation
    public float tumble; 

    void Start()
    {
        Rigidbody rigidBodyOfAsteroid = GetComponent<Rigidbody>();
        // rotation of asteroid randomly * tumble speed
        rigidBodyOfAsteroid.angularVelocity = Random.insideUnitSphere * tumble; 
    }
}
