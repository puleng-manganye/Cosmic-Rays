using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class can be attached to a trigger volume to turn it into a checkpoint.
 * It tells the Level Manager to store information about the level and the Player
 * when the checkpoint is reached so that it can be re-instantiated if the Player dies. 
 * 
 * NOTES: The checkpoint should be on the Trigger Volumes layer.
 */
#endregion

public class CheckPoint : MonoBehaviour
{
    #region Fields

    // A reference to the Player
    private GameObject _player;

    // A reference to the Level Manager
    private LevelManager _levelManager;
    #endregion

    #region References and Initialisations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Get the Level Manager
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }
    #endregion

    #region When the Player collides with the Checkpoint

    // Store information about the level and the Player when the checkpoint is reached
    // so that it can be re-instantiated if the Player dies
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            // Location of the Checkpoint
            _levelManager.transformAtLastCheckPoint = this.transform;

            // Score at the last Checkpoint
            _levelManager.scoreAtLastCheckPoint = _levelManager.score;

            // Update Respawns
            _levelManager.UpdateRespawns();
            

            // Respawn when next checkpoint is reached
            Destroy(gameObject);  
        }
    }
    #endregion
}
