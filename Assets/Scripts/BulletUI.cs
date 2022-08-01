using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    // Components
    Image image; // the image of this object

    // variables
    int bulletsLeft;

    [SerializeField] Sprite[] allSprites; // sprites used for this object
    Sprite currentSprite; // current sprite used
    int totalSprites;

    public bool bulletCountHasChanged = false; // if the player has shot a bullet and we have not accounted for it yet

    // game objects
    PlayerShoot playerShoot;

    // Start is called before the first frame update
    void Start()
    {
        // Components
        image = GetComponent<Image>();

        // game objects
        playerShoot = FindObjectOfType<PlayerShoot>();

        // variables
        bulletsLeft = playerShoot.GetBulletsLeft();

        currentSprite = allSprites[0];
        totalSprites = allSprites.Length - 1;

        // update image sprite
        image.sprite = currentSprite;
    }

    // Update is called once per frame
    void Update()
    {
        // check if bullet count has changed
        if (bulletCountHasChanged)
        {
            //change the sprite
            ChangeSprite();

            // reset bool
            bulletCountHasChanged = false;
        }
    }

    // change bullet sprite
    void ChangeSprite()
    {
        // update bullet count
        bulletsLeft = playerShoot.GetBulletsLeft();

        // check bullets are in range
        if (bulletsLeft >= 0)
        {
            // update current sprite to next in array
            currentSprite = allSprites[totalSprites - bulletsLeft];

            // update image sprite
            image.sprite = currentSprite;
        }
    }
}
