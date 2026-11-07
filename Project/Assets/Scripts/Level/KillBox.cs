using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class is meant to be attached to a big trigger volume placed underneath the level. 
 * It makes sure that anything that falls off the level and touches it, get destroyed.
 * 
 * If you place the 'Player' object in the 'playerOverride' field, it will reload the current scene 
 * when the Player touches the killbox, instead of destroying the Player.
 */
#endregion

public class KillBox : MonoBehaviour
{
    #region Fields

    // A reference to the Player
    public GameObject playerOverride;

    // A reference to the Level Manager
    private LevelManager _levelManager;
    #endregion

    #region References and Initialisations

    private void Awake()
    {
        // Get the Level Manager
        // It stores the name of the current scene
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }
    #endregion

    #region When the Player/an object collides with the Killbox

    private void OnTriggerEnter2D(Collider2D col)
    {
        // If the 'playerOverride' field has been set,
        // reset the level when the Player touches the killbox.
        if (playerOverride != null && col.tag == playerOverride.tag)
        {
            _levelManager.ReloadScene(_levelManager.currentScene);
        }
        // Otherwise, destroy the object.
        else
        {
            Destroy(col.gameObject);
        }
    }
    #endregion
}
