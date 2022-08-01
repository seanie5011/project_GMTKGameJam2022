using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // children
    EnemyMovement[] enemyScripts;

    // Start is called before the first frame update
    void Start()
    {
        // get EnemyMovement scripts for each child enemy
        enemyScripts = gameObject.GetComponentsInChildren<EnemyMovement>();

        // iterate through each and disable
        foreach (EnemyMovement currentScript in enemyScripts)
        {
            currentScript.enabled = false;
        }
    }

    // trigger enter
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if trigger with player
        if (collision.CompareTag("Player"))
        {
            // re enable each script
            foreach (EnemyMovement currentScript in enemyScripts)
            {
                currentScript.enabled = true;
            }

            // disable this collider, cannot be reused
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
