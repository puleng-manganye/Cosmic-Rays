using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class controls the player's shooting behaviour. 
 * Most of the logic is contained in the Shoot function below.
 */
#endregion

public class PlayerShoot : MonoBehaviour
{
    #region Fields:

    #region Public Fields:

    [Header("Initializations")]
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;

    [Header("Projectile")]

    public int projectileDamage = 1;
    public float projectileFireRate = 0f;
    public KeyCode playerShootButton;
    public LayerMask projectileCollision;

    [Header("Effects")]
    public GameObject sfx;
    #endregion

    #region Private Fields:

    private float _projectileSpeed = 2;
    private PlayerMovement _playerMovement;
    private bool _canShoot = true;
    private LevelManager _levelManager;
    private int candy = -1;
    private int candyCornAmount;
    #endregion

    #endregion

    #region References and Initialisations

    private void Awake()
    {
        // Get the Level Manager
        _levelManager = GameObject.FindGameObjectWithTag("Level Manager").GetComponent<LevelManager>();
        candyCornAmount = _levelManager.candyCornAmount;
        _playerMovement = GetComponent<PlayerMovement>();

        // Just some reminders for us in case we forget to assign a projectile or spawn point.
        if (projectileSpawnPoint == null)
        {
            Debug.Log("ERROR: You need to assign a transform for your projectiles to spawn at!");
        }

        if (projectilePrefab == null)
        {
            Debug.Log("ERROR: You need to assign a projectile prefab!");
        }
        else
        {
            _projectileSpeed = projectilePrefab.GetComponent<Projectile>().projectileSpeed;
        }
    }
    #endregion

    #region Updates

    private void Update()
    {
        candyCornAmount = _levelManager.candyCornAmount;

        if (Input.GetKeyDown(playerShootButton) && _playerMovement.shootOn && _canShoot && candyCornAmount >= 1)
        {

            Shoot();
            StartCoroutine(ProjectileDelay());
        }
    }
    #endregion

    #region Shoot

    private void Shoot()
    {   
        // Candy Corn
        _levelManager.AddCandyToBag(candy);
        candyCornAmount = _levelManager.candyCornAmount;

        if (candyCornAmount < 0)
        {
            _canShoot = false;
        }
        else if (candyCornAmount >= 1)
        {
            _canShoot= true;
        }

        // Instantiate a projectile prefab.
        GameObject projectile;

        // Instantiate effects if there are any.
        #region SFX
        if (sfx != null)
        {
            Instantiate(sfx, transform.position, Quaternion.identity);
        }
        #endregion

        projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // If we're facing right:
        if (transform.position.x < projectileSpawnPoint.position.x)
        {
            rb.velocity = transform.TransformDirection(Vector3.right * _projectileSpeed);
        }
        //if we're facing left:
        else if (transform.position.x > projectileSpawnPoint.position.x)
        {
            rb.velocity = transform.TransformDirection(Vector3.left * _projectileSpeed);
        }
    }
    #endregion

    #region Projectile Delay

    // This co-routine determines how often the player is able to shoot a projectile.
    IEnumerator ProjectileDelay()
    {
        _canShoot = false;
        yield return new WaitForSeconds(projectileFireRate);
        _canShoot = true;
    }
    #endregion
}
