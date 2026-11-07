using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class SetActive1 : MonoBehaviour
{
    #region Fields

    // A reference to the Player
    private GameObject _player;

    // A reference to the Icon
    public GameObject Icon;
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Disable Icon
        Icon.gameObject.SetActive(false);
    }
    #endregion

    #region When the Player collides with the trigger

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if trigger is colliding with the Player
        if (col.gameObject == _player)
        {
            // Enable gameObject
            Icon.gameObject.SetActive(true);
        }
    }
    #endregion    
}
