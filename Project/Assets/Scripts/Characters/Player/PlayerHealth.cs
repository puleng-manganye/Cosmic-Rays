using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#region Class Description:
/* 
 * This class manages the player's health and also has a public field that allows you to control
 * how long the player will be invincible for after being hit by an enemy.
*/
#endregion

public class PlayerHealth : MonoBehaviour
{
    #region Fields

    #region Public Fields:

    [Header("Health")]

    public int lives = 3;
    public Text livesText;

    public int health = 10;
    public string currentHealth;
    public HealthBar healthBar;
    public float invincibilityTime = 0.5f;

    // hit 
    public GameObject hit_vfx;
    public GameObject die_vfx;

    // die
    public GameObject hit_sfx;
    public GameObject die_sfx;

    [Header("Death")]
    public KeyCode autoDeathButton;

    #endregion

    #region Non-Serialized Public Fields:

    [System.NonSerialized]
    public bool invincible;
    #endregion

    #region Private Fields:

    private Scene _sceneManager;
    private LevelManager _levelManager;
    private GameObject _playerSprite;
    private int _currentHealth;

    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        _sceneManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<Scene>();
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        _currentHealth = health;
        currentHealth = _currentHealth.ToString();
        _playerSprite = transform.Find("Player_GFX/Player_sprite").gameObject;

        // Lifes text
        livesText.text = lives.ToString();

        // Health bar
        healthBar.SetMaxHealth(health);
    }
    #endregion

    #region Kill Player (for debbugging purposes)

    private void Update()
    {
        // Allows you to kill the player using a key for debugging and testing purposes.
        if (Input.GetKeyDown(autoDeathButton))
        {
            Die();
        }

        livesText.text = lives.ToString();
        healthBar.SetHealth(_currentHealth);
    }
    #endregion

    #region Lose health

    // Controls how the player looses health.
    public void LooseHealth(int healthToDeduct)
    {
        if (!invincible)
        {
            // Sets health based on a value passed from another script (usually the enemy).
            _currentHealth -= healthToDeduct;
            currentHealth = _currentHealth.ToString();

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

            // Makes us invincible for a short period after being hit.
            if (_currentHealth > 0)
            {
                StartCoroutine(Invincibility());
                // Debug.Log("Invincible :)");
            }
            // Kills the player if their health reaches zero.
            else if (_currentHealth <= 0)
            {
                Die();
            }
        }        
    }
    #endregion

    #region Gain health from PickUps

    // This function allows us to gain health from pickups.
    public void GainHealth(int healthToGain)
    {
        _currentHealth += healthToGain;

        if (_currentHealth > 5)
        {
            _currentHealth = 5;
            lives += 1;

        }

        currentHealth = _currentHealth.ToString();
    }
    #endregion

    #region Kills the player

    // Kills the player and reloads the current scene.
    public void Die()
    {
        lives -= 1;
        _currentHealth = 5;
        healthBar.SetMaxHealth(health);

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

        if (lives < 0 || lives <= -1)
        {
            EndGame(8);
        }
    }
    #endregion

    #region Endgame

    public void EndGame(int sceneIndex)
    {
        // Load end scene if lifes run out
        _sceneManager.LoadScene(sceneIndex);
    }

    #endregion

    #region Invicibility

    // The timer for the invincibility period. You could play around with this and the PowerUp script
    // to create a powerup that makes you invincible...
    IEnumerator Invincibility()
    {
        invincible = true;

        Color col = _playerSprite.GetComponent<SpriteRenderer>().color;
        col.a = 0.5f;
        _playerSprite.GetComponent<SpriteRenderer>().color = col;

        yield return new WaitForSeconds(invincibilityTime);
        
        invincible = false;
        col.a = 1f;
        _playerSprite.GetComponent<SpriteRenderer>().color = col;
        // Debug.Log("Not Invincible :(");
    }
    #endregion
}
