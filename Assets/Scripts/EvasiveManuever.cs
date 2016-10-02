using UnityEngine;
using System.Collections;

public class EvasiveManuever : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;

    public Vector2 startWait;
    public Vector2 manueverTime;
    public Vector2 manueverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float targetManuever;
    private Rigidbody rigidBodyOfEnemyShip;


	void Start ()
    {
        rigidBodyOfEnemyShip = GetComponent<Rigidbody>();
        currentSpeed = rigidBodyOfEnemyShip.velocity.z;
        StartCoroutine (Evade());
	}

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManuever = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(manueverTime.x, manueverTime.y));
            targetManuever = 0;
            yield return new WaitForSeconds(Random.Range(manueverWait.x, manueverWait.y));
        }
    }
	
	void FixedUpdate ()
    {
        float newManuever = Mathf.MoveTowards(rigidBodyOfEnemyShip.velocity.x, targetManuever, Time.deltaTime * smoothing);
        rigidBodyOfEnemyShip.velocity = new Vector3(newManuever, 0.0f, currentSpeed);
        rigidBodyOfEnemyShip.position = new Vector3
        (
            Mathf.Clamp(rigidBodyOfEnemyShip.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBodyOfEnemyShip.position.z, boundary.zMin, boundary.zMax)
        );

        rigidBodyOfEnemyShip.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBodyOfEnemyShip.rotation.x * -tilt);
    }
}
