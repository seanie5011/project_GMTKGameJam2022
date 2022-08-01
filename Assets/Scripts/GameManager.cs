using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Awake called first
    void Awake()
    {
        // Singleton
        // find all GameManagers objects
        int numGameManagers = FindObjectsOfType<GameManager>().Length;

        // if more than 1, destroy this one
        if (numGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else // otherwise, make it persist between loads
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // next level
    public void LoadNextLevel()
    {
        // find current scene (level) and then next scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // check we have more levels, if not, return to first
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        // load next scene
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // loads the active scene using its build index, effectively replaying scene
    }
}