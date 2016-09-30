using UnityEngine;
using System.Collections;

// 'Serialize this class in order for it to show up in Unity
[System.Serializable] 
public class Boundary
{
    // limit bounds for Ship on background
    public float xMin, xMax, zMin, zMax; 
}

public class PlayerController : MonoBehaviour {

    public float additionalSpeed;
    public float tilt;
    public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Update()
    {
        // Get mouse input
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            // get audio component attached to this object
            AudioSource fireSound = GetComponent<AudioSource>();
            // Play audio on each shot
            fireSound.Play(); 
        }
    }
	
	void FixedUpdate () {
  
        // Get Keyboard input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Rigidbody rigidBodyOfSpaceShip = GetComponent<Rigidbody>();

        // Set movement parameters from Keyboard Input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Determine speed of movement based on Keyboard Input and Speed multiplier
        rigidBodyOfSpaceShip.velocity = movement * additionalSpeed;

        // Set bondaries for the Ship to not go out of frame
        rigidBodyOfSpaceShip.position = new Vector3
             (
               Mathf.Clamp(rigidBodyOfSpaceShip.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rigidBodyOfSpaceShip.position.z, boundary.zMin, boundary.zMax)
             );

        // Quaternion used to measure rotation much like Vector is used for position
        // Set rotation of rigidbody of Ship to a value along its z axis to make it tilt on move
        rigidBodyOfSpaceShip.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBodyOfSpaceShip.velocity.x * -tilt);


    }
}
