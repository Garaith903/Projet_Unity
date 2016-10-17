using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    private float value = 0.0f;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, value, moveVertical);
        Rigidbody rigidBody;

        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = movement * speed;

        rigidBody.position = new Vector3
        (   
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            value,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidBody.rotation = Quaternion.Euler(value, value, rigidBody.velocity.x * (-1 * tilt));
    }
}
