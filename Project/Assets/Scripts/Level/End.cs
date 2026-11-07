using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#region Class Description:
/*
 * This class allows for the Player to interact with doors.
 */
#endregion


public class End : MonoBehaviour
{
    #region Fields

    // Player
    private GameObject _player;

    // Scene Manager
    private Scene _sceneManager;

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the References
        _player = GameObject.FindGameObjectWithTag("Player");
        _sceneManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Scene>();
    }
    #endregion

    #region When the Player collides with a NPC

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if Player is colliding with a door
        if (col.gameObject == _player)
        {
            // Enable the Dialogue box when colliding a door
            EndGame(7);
        }
    }

    public void EndGame(int sceneIndex)
    {
        // Load end scene if lifes run out
        _sceneManager.LoadScene(sceneIndex);
    }
    #endregion
}
