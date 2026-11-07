using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class can be used to create a generic coin type pickup that
 * adds a set amount of points to the player's score when they collide with it.
 */
#endregion

public class PointPickup : MonoBehaviour
{
    #region Fields

    [Header("Score")]

    // Score to add
    public int pointsToAdd = 0;
    public int candy = 1;

    [Header("Effects")]

    // VFX
    public GameObject vfx;

    // SFX
    public GameObject sfx;

    // A reference to the Player
    private GameObject _player;

    // A refernce to the Level Manager
    private LevelManager _levelManager;

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

    #region When the Player collides with the PickUp

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if it is the Player, if it is the Player,
        // add to the score and instatiate the effects for this PickUp
        if (col.gameObject == _player)
        {
            _levelManager.AddCandyToBag(candy);
            _levelManager.AddPointsToScore(pointsToAdd);

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