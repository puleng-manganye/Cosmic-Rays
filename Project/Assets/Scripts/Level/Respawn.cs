using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class is meant to be attached to a big trigger volume placed underneath the level. 
 * It respawns the player.
 */
#endregion

public class Respawn : MonoBehaviour
{
    #region Fields

    #region Private Fields:
    // A reference to the Player
    private GameObject _player;

    // A reference to the Level Manager
    private PlayerHealth _playerHealth;

    // Respawn Position
    private Vector3 spawnPosition;

    // Scene Manager
    private Scene _sceneManager;
    #endregion

    #region Public Fields:
    [Header("Timer")]

    // Delay Time
    public float respawnDelay = 1f;

    [Header("Coordinate")]

    // Coordinates
    public float x, y, z;

    // Player death sfx
    public GameObject die_sfx;
    #endregion

    #endregion

    #region References and Initilisations

    private void Awake()
    {
        // Set position
        spawnPosition = new Vector3(x, y, z);

        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Get the Level Manager
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        // Get the scene
        _sceneManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Scene>();
    }
    #endregion

    #region When the Player/an object collides with the Trigger

    private void OnTriggerEnter2D(Collider2D col)
    {
        #region SFX

        #region Effects

        // Die sound effect
        if (die_sfx != null)
        {
            Instantiate(die_sfx, transform.position, Quaternion.identity);
        }
        #endregion
        #endregion

        #region Deduct Life

        _playerHealth.Die();
        #endregion

        #region Spawn Player
        // Make player spawn at this position
        _player.transform.position = spawnPosition;
        #endregion
    }
    #endregion

    #region Delay Timer

    IEnumerator RespawnDelay()
    {
        // This co-routine waits for a set amount of time before respawning the player
        yield return new WaitForSeconds(respawnDelay);
    }
    #endregion
}
