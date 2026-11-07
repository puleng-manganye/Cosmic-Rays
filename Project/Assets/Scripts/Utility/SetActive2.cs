using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class SetActive2 : MonoBehaviour
{
    #region Fields

    // A reference to the Player
    private GameObject _player;

    // A reference to the Triggers
    public GameObject gameObject1,g1, gameObject2,g2;
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region When the Player collides with the trigger

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if trigger is colliding with the Player
        if (col.gameObject == _player)
        {
            // Enable gameObject2/Disable gameObject1
            gameObject1.gameObject.SetActive(false);
            gameObject2.gameObject.SetActive(true);
            g1.gameObject.SetActive(true);
            g2.gameObject.SetActive(false);
        }
    }
    #endregion    
}
