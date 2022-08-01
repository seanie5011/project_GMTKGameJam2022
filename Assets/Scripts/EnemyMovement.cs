using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Components
    Rigidbody2D rb2D;

    // variables
    [SerializeField] float moveSpeed = 1f;
    int movementSign = 1;

    [SerializeField] Sprite chaseSprite;
    [SerializeField] Sprite fleeSprite;

    bool bulletHit = false;

    // game objects and children
    [SerializeField] GameObject objectToTrack;

    GameObject spriteChild;
    SpriteRenderer spriteChildRenderer;

    AudioManager audioManager;

    // Other game objects components
    PlayerShoot playerShoot;

    // Start is called before the first frame update
    void Start()
    {
        // Components
        rb2D = GetComponent<Rigidbody2D>();

        // game objects
        spriteChild = gameObject.transform.Find("SpriteObject").gameObject; // gets the SpriteObject Child of this gameObject
        spriteChildRenderer = spriteChild.GetComponent<SpriteRenderer>();

        // Other game objects components
        playerShoot = FindObjectOfType<PlayerShoot>();

        // audio manager
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Updating");
        // point sprite child at player
        spriteChild.transform.right = (objectToTrack.transform.position - spriteChild.transform.position) * movementSign;

        // update velocity of enemy
        rb2D.velocity = GetDirectionToTrack(movementSign) * moveSpeed;
    }

    private Vector2 GetDirectionToTrack(int signOfMotion)
    {
        // get players position
        // get this objects position
        // take them from eachother
        Vector2 directionToTrack = objectToTrack.transform.position - gameObject.transform.position;

        // normalize them to get direction
        directionToTrack.Normalize();

        // multiply by sign; +1 is towards player, -1 is away
        directionToTrack *= signOfMotion;

        return directionToTrack;
    }

    // collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if collide with player
        if (collision.gameObject.CompareTag("Player"))
        {
            // if player hits enemy back while enemy is fleeing
            if (movementSign == -1)
            {
                // allow player to shoot
                playerShoot.canShoot = true;

                // if this scripts update function is enabled
                if (this.enabled == true)
                {
                    // change sprite to chase sprite
                    spriteChildRenderer.sprite = chaseSprite;

                    // call coroutine
                    StartCoroutine(DisableComponent());

                    // change direction of movement
                    movementSign *= -1;

                    // play sound
                    audioManager.Play("Steal");
                }
            }
            else // if player hits enemy while enemy is chasing
            {
                // if this scripts update function is enabled and player can shoot (so player has gun)
                if (this.enabled == true && playerShoot.canShoot == true)
                {
                    // disable enemy shooting
                    playerShoot.canShoot = false;

                    // change sprite to flee sprite
                    spriteChildRenderer.sprite = fleeSprite;

                    // change direction of movement
                    movementSign *= -1;

                    // play sound
                    audioManager.Play("Steal");
                }
            }
        }
        else if (collision.gameObject.CompareTag("Bullet")) // collides with bullet
        {
            // signal that it has been hit by a bullet
            bulletHit = true;

            // disable this components update function
            this.enabled = false;
        }
    }

    // stop enemy movement coroutine
    IEnumerator DisableComponent()
    {
        // disable this components update function
        this.enabled = false;

        // wait 2 seconds
        yield return new WaitForSecondsRealtime(2f);

        // if has not been hit by a bullet in the 2 second wait time
        if (!bulletHit)
        {
            // re-enable update function
            this.enabled = true;
        }
    }
}
