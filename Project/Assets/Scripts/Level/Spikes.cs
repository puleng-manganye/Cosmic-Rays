using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region Class Description:
/* 
 * This script allows the spikes to do damage to the player 
 * and reduce their health by a set amount.
 */
#endregion

public class Spikes : MonoBehaviour
{
    #region Fields

    #region Public Fields:

    public int damageToPlayer = 1;
    public float waitTime = 1.5f;
    #endregion

    #region Private Fields:

    private LayerMask _playerLayer;
    private GameObject _player;
    private PlayerHealth _playerHealth;
    private PlayerMovement _playerMovement;
    private bool _canDamage = true;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get references
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerLayer = LayerMask.GetMask("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }
    #endregion

    #region When the Player collides with the trigger

    // Check to see if we're colliding with the player and if so, call the 
    // 'Bounce()' and 'LooseHealth()' functions on the player object.
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == _player && _canDamage)
        {
            StartCoroutine(Damage());
        }
    }

    // Time to wait
    IEnumerator Damage()
    {
        _canDamage = false;
        _playerMovement.Bounce();
        _playerHealth.LooseHealth(damageToPlayer);

        yield return new WaitForSeconds(waitTime);

        _canDamage = true;
        
    }
    #endregion
}
