using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class controls the enemy's health. It also defines what type of enemy this is,
 * destroys the enemy when it hits zero health and adds points to the player's score value. 
 */
#endregion

public class EnemyHealth : MonoBehaviour
{
    #region Fields

    #region Public Fields:

    public enum enemyType
    {
        walking,
        jumping,
        shooting,
    }

    public enemyType typeOfEnemy;

    [Header("Health")]
    public bool stompable = true;
    public int health = 1;
    public string currentHealth;

    public HealthBar healthBar;
    public GameObject bar;

    [Header("Effects")]

    // hit
    public GameObject hit_vfx;
    public GameObject die_vfx;

    // die
    public GameObject hit_sfx;
    public GameObject die_sfx;

    [Header("Score")]
    public int points;
    #endregion

    #region Non-Serialized Public Fields:

    [System.NonSerialized]
    public int _currentHealth;
    #endregion

    #region Private Fields:

    private GameObject _player;
    private LevelManager _levelManager;
    private LayerMask _playerLayer;
    private int _playerProjectileDamage;
    private EnemyMovement _enemyMovement;
    
    #endregion
    #endregion

    #region References and Initialisations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerLayer = LayerMask.GetMask("Player");

        // Get the Level Manager
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();

        // Get the Player Shoot
        _playerProjectileDamage = _player.GetComponent<PlayerShoot>().projectileDamage;

        // Get the Enemy Movement
        _enemyMovement = GetComponent<EnemyMovement>();

        // Health
        _currentHealth = health;
        currentHealth = _currentHealth.ToString();
        healthBar.SetMaxHealth(health);

        // Points 
        points = health * 20;
    }
    #endregion

    #region When Player collides with the Enemy

    private void OnTriggerEnter2D(Collider2D col)
    {
        // This is branch statement controls the player's stomp behaviour.
        if (col.gameObject == _player && stompable)
        {
            // Casts some rays to check if the player is above us. We could also use the 
            // CharacterController2D's 'contact_above' flag, but this gives us a bit more
            // control and potential extensability.
            if (Physics2D.Raycast
                (_enemyMovement._topLeft, Vector3.up, 0.2f, _playerLayer) ||
                Physics2D.Raycast
                (_enemyMovement._topMid, Vector3.up, 0.2f, _playerLayer) ||
                Physics2D.Raycast
                (_enemyMovement._topMid, Vector3.up, 0.2f, _playerLayer))
            {
                // Makes the player bounce after stomping the enemy.
                _player.GetComponent<PlayerMovement>().Bounce();
                bar.SetActive(true);

                // Deducts health from the stomp based on values set in the LevelManager class.
                if (typeOfEnemy == enemyType.walking)
                {
                    LooseHealth(_levelManager.walkingEnemyDamage);

                    #region Effects

                    // Hit visual effect
                    if (hit_vfx != null)
                    {
                        Instantiate(hit_vfx, transform.position, Quaternion.identity);
                    }

                    // Hit sound effect
                    if (hit_sfx != null)
                    {
                        Instantiate(hit_sfx, transform.position, Quaternion.identity);
                    }
                    #endregion
                }
                else if (typeOfEnemy == enemyType.jumping)
                {
                    LooseHealth(_levelManager.jumpingEnemyDamage);

                    #region Effects

                    // Hit visual effect
                    if (hit_vfx != null)
                    {
                        Instantiate(hit_vfx, transform.position, Quaternion.identity);
                    }

                    // Hit sound effect
                    if (hit_sfx != null)
                    {
                        Instantiate(hit_sfx, transform.position, Quaternion.identity);
                    }
                    #endregion
                }
                else if (typeOfEnemy == enemyType.shooting)
                {
                    LooseHealth(_levelManager.shootingEnemyDamage);

                    #region Effects

                    // Hit visual effect
                    if (hit_vfx != null)
                    {
                        Instantiate(hit_vfx, transform.position, Quaternion.identity);
                    }

                    // Hit sound effect
                    if (hit_sfx != null)
                    {
                        Instantiate(hit_sfx, transform.position, Quaternion.identity);
                    }
                    #endregion
                }
                else
                {
                    LooseHealth(1);

                    #region Effects

                    // Hit visual effect
                    if (hit_vfx != null)
                    {
                        Instantiate(hit_vfx, transform.position, Quaternion.identity);
                    }

                    // Hit sound effect
                    if (hit_sfx != null)
                    {
                        Instantiate(hit_sfx, transform.position, Quaternion.identity);
                    }
                    #endregion
                }
            }
        }

        // Calls the looseHealth function if the enemy is hit by one of the player's projectiles.
        if (col.gameObject.CompareTag("Projectile_player"))
        {
            LooseHealth( _playerProjectileDamage);
            bar.SetActive(true);
        }
    }
    #endregion

    #region Kill the enemy

    // Updates
    public void Update()
    {
        healthBar.SetHealth(_currentHealth);
    }

    // This one is pretty self explanatory.
    public void LooseHealth(int damage)
    {
        _currentHealth -= damage;

        #region Health Bar
        currentHealth = _currentHealth.ToString();
        healthBar.SetHealth(_currentHealth);
        #endregion

        #region Die
        if (_currentHealth <= 0)
        {
            Die();
        }
        #endregion
    }

    // So's this one ;)
    private void Die()
    {
        _levelManager.AddPointsToScore(points);

        #region Effects

        // Die visual effect
        if (die_vfx != null)
        {
            Instantiate(die_vfx, transform.position, Quaternion.identity);
        }

        // Die sound effect
        if (die_sfx != null)
        {
            Instantiate(die_sfx, transform.position, Quaternion.identity);
        }
        #endregion

        // Clean up
        bar.SetActive(false);
        Destroy(gameObject);
    }
    #endregion
}
