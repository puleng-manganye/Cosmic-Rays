using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class can be attached to an object to make it give the player points when they collide with it. 
 * You can use it to make simple Pickups like Coins and stuff. 
 */
#endregion

public class AddPoints : MonoBehaviour
{
    #region Fields

    // Destroy this Object
    public bool destroyGameObject = false;

    [Header("Score")]

    // Score to add
    public int scoreToAdd = 1;

    [Header("Effects")]

    // VFX
    public GameObject vfx;

    // SFX
    public GameObject sfx;

    // A reference to the Level Manager
    private LevelManager _levelManager;

    // A reference to the Player
    private GameObject _player;
    #endregion

    #region References and Initializations
    private void Start()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Get the Level Manager
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
    }
    #endregion

    #region When the Player collides with the Object
    // When another object collides with this one, check to see if it's the player, and if it is, 
    // add a set number of points to the total score (which is stored in the 'LevelManager' class).
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player)
        {
            _levelManager.AddPointsToScore(scoreToAdd);

            if (!destroyGameObject)
            {
                return;
            }
            
            // Instantiate effects if there are any.
            #region Effects

            #region VFX
            if (vfx != null)
            {
                Instantiate(vfx, transform.position, Quaternion.identity);
            }
            #endregion

            #region SFX
            if (sfx != null)
            {
                Instantiate(sfx, transform.position, Quaternion.identity);
            }
            #endregion

            #endregion

            // Clean up
            Destroy(gameObject);
        }
    }
    #endregion
}
