using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    // GameObjects and Children
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject gun;
    Transform gunTransform;
    SpriteRenderer gunSprite;

    BulletUI bulletUI;

    AudioManager audioManager;

    // variables
    [HideInInspector] public bool canShoot = true;

    [SerializeField] float gunDistanceFromPlayer = 1.4f;

    [SerializeField] int bulletsTotal = 7;
    int bulletsLeft;

    // Start is called before the first frame update
    void Start()
    {
        // gun child
        gunTransform = gun.transform;
        gunSprite = gun.GetComponent<SpriteRenderer>();

        // bullets
        bulletsLeft = bulletsTotal;

        // bullet ui
        bulletUI = FindObjectOfType<BulletUI>().GetComponent<BulletUI>();

        // audio manager
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // get guns new position relative to the player; in direction of mouse, a certain distance from player
        Vector3 gunNewRelativePosition = GetDirectionToShoot() * gunDistanceFromPlayer;

        // update gun position; adding to player position
        gunTransform.position = gameObject.transform.position + gunNewRelativePosition;

        // if cant shoot, disable gunsprite
        if (!canShoot)
        {
            gunSprite.enabled = false;
        }
        else if (gunSprite.enabled == false) // otherwise if sprite disabled, re-enable
        {
            gunSprite.enabled = true;
        }
    }

    // LMB pressed
    private void OnFire(InputValue value)
    {
        // check if player can shoot and has bullets left
        if (canShoot && bulletsLeft > 0)
        {
            // less bullets left
            bulletsLeft -= 1;

            // create bullet
            Instantiate(bullet, gunTransform.position, transform.rotation);

            // update bulletUI bool to say we have shot
            bulletUI.bulletCountHasChanged = true;

            // play sound
            audioManager.Play("Throw");
        }
    }

    public Vector2 GetDirectionToShoot()
    {
        // get mouse position
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        // convert from pixel units to world units
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // get vector from player to mouse position
        Vector2 directionToShoot = mouseWorldPosition - gameObject.transform.position;

        // normalize them to get direction
        directionToShoot.Normalize();

        return directionToShoot;
    }

    // function to get how many bullets are left
    public int GetBulletsLeft()
    {
        return bulletsLeft;
    }
}
