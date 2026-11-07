using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#region Class Description:
/*
 * This class allows for the NPC to interact with the Player.
 */
#endregion

public class NPC : MonoBehaviour
{
    #region Fields

    // A reference to the Player
    private GameObject _player;

    // A reference to the Story Manager
    private StoryManager _storyManager;

    // A reference to the Player Movement
    private PlayerMovement _playerMovement;

    // A reference to the Sprite Renderer component of the Speech Bubbles
    public SpriteRenderer _spriteRenderer, _playerRenderer;
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Get the Story Manager
        _storyManager = GameObject.Find("InkManager").GetComponent<StoryManager>();

        // Get the Player Movement
        _playerMovement = _player.GetComponent<PlayerMovement>();

        // Disable the Speech Bubbles
        _spriteRenderer.enabled = false;
        _playerRenderer.enabled = false;
    }
    #endregion

    #region When the Player collides with a NPC

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if NPC is colliding with the Player
        if (col.gameObject == _player)
        {
            //Debug.Log("Can talk with " + gameObject.name);

            // Enable the Speech Bubbles when colliding
            _spriteRenderer.enabled = true;
            _playerRenderer.enabled = true;

            // Update the active actor in the Story Manager
            _storyManager.activeNPC = gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        // Check to see if NPC has exited collision with the Player
        if (col.gameObject == _player)
        {
            //Debug.Log("Cannot talk with " + gameObject.name);

            // Disable the Speech Bubbles when not colliding
            _spriteRenderer.enabled = false;
            _playerRenderer.enabled = false;

            // Update the active actor in the Story Manager
            _storyManager.activeNPC = "";
        }
    }
    #endregion

    #region Player horizontal movement 

    private void Update()
    {
        // Make the Player stop moving when in a conversation
        if (_storyManager.activeDialogue == true)
        {
            _playerMovement.moveSpeed = 0f;
        } 
        else
        {
            _playerMovement.moveSpeed = 3f;
        }
    }
    #endregion
}