using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * This class allows for the Player to interact with doors.
 */
#endregion


public class Door : MonoBehaviour
{
    #region Fields

    // Player
    private GameObject _player;

    // Enter house Dialogue box
    public GameObject enterHouseDialoguePanel;

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Disable Dialogue box
        enterHouseDialoguePanel.SetActive(false);

        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region When the Player collides with a NPC

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if Player is colliding with a door
        if (col.gameObject == _player)
        {
            // Enable the Dialogue box when colliding a door
            enterHouseDialoguePanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Check to see if NPC has exited collision with the door
        if (col.gameObject == _player)
        {
            // Disable the Dialogue box when not colliding with the door
            enterHouseDialoguePanel.SetActive(false);
        }
    }
    #endregion
}
