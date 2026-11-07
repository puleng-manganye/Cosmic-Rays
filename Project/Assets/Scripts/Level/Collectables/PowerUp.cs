using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class is attached to objects that act as PowerUps. 
 * It is a generic PowerUp which allows you to select whether to activate a power on the Player 
 * such as double-jumping or gliding,or to add health to the Player. 
 * 
 * NOTES: If you want to add your own powers, apart from having to code them in the 'PlayerMovement' class, 
 * you'll also need to add them to the PowerUpType enum below if you'd like to use the powerup to activate them.
 * 
 * Also note that powerups should have their BoxCollider2D component set to 'Is Trigger'.
 */
#endregion

public class PowerUp : MonoBehaviour
{
    #region Fields

    // How much health will be added if this PowerUp is set to be a health powerup. 
    // If it's set to something else this field does nothing!
    public int healthToGive = 1;
    
    #region Enums
    public enum PowerUpType
    { 
        DoubleJump,
        WallJump,
        Glide,
        Shoot,
        Health,
    }
    #endregion

    [Header("PowerUp")]

    // Type of PowerUp
    public PowerUpType powerUpType;

    [Header("Effects")]

    // VFX
    public GameObject vfx;

    // SFX
    public GameObject sfx;

    // A reference to the Player
    private GameObject _player;
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    #endregion

    #region When the Player collides with the PowerUp
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to see if we're colliding with the Player
        if (col.gameObject == _player)
        {
            // Get a reference to the 'PlayerMovement' script
            PlayerMovement _playerMovement = _player.GetComponent<PlayerMovement>();

            // Activate a power on the Player based on what type of PowerPp this is
            
            // DoubleJump
            if (powerUpType == PowerUpType.DoubleJump)
            {
                _playerMovement.doubleJumpOn = true;
            }

            // WallJump
            else if (powerUpType == PowerUpType.WallJump)
            {
                _playerMovement.wallJumpOn = true;
            }

            // Glide
            else if (powerUpType == PowerUpType.Glide)
            {
                _playerMovement.glideOn = true;
            }

            //  Shoot
            else if (powerUpType == PowerUpType.Shoot)
            {
                _playerMovement.shootOn = true;
            }

            // Health
            else if (powerUpType == PowerUpType.Health)
            {
                PlayerHealth _playerHealth = _player.GetComponent<PlayerHealth>();
                _playerHealth.GainHealth(healthToGive);
            }

            // Run some effects if available

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