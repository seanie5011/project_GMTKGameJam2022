using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Components
    Rigidbody2D rb2D;

    // variables
    [SerializeField] float bulletSpeed = 1f;
    Vector2 bulletVelocity;

    // game objects
    Transform gunTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Components
        rb2D = GetComponent<Rigidbody2D>();

        // get Gun GameObject from player, and get its transform
        gunTransform = GameObject.Find("Gun").transform;

        // set position to player gun position
        transform.position = gunTransform.position;

        // get the direction needed to shoot from PlayerShoot script
        Vector3 playerShootDirection = FindObjectOfType<PlayerShoot>().GetDirectionToShoot();

        // point bullet in direction
        transform.right = playerShootDirection;

        // set bullet Velocity by using PlayerShoot script method and multiplying by speed
        bulletVelocity = playerShootDirection * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // give the bullet some velocity
        rb2D.velocity = bulletVelocity;
    }

    // collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // destroy bullet
        Destroy(gameObject);
    }
}