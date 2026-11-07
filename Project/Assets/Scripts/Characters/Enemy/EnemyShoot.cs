using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region Class Description
/*
 * 
 */
#endregion

public class EnemyShoot : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Header("Initializations")]
    public GameObject enemy;
    public Transform projectileSpawnPoint;
    public GameObject projectilePrefab;

    [Header("Projectile")]
    public float range = 5f;
    public LayerMask playerMask;
    public float projectileFireRate = 0.5f;
    public float shootDelay = 0.5f;

    [Header("Debugging")]
    public SpriteRenderer _spriteRenderer;
    #endregion

    #region Private Fields:
    private GameObject _player;
    private float _projectileSpeed = 1;
    private bool _canShoot = true;
    private bool _playerInRange;
    private Animator _animator;
    private CharacterController2D _enemyController2D;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");
        
        // Get the Sprite Renderer
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Player isn't in range
        _playerInRange = false;

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
            _projectileSpeed = projectilePrefab.GetComponent<EnemyProjectile>().projectileSpeed;
        }

        _enemyController2D = enemy.GetComponent<CharacterController2D>();
        _animator = enemy.GetComponentInChildren<Animator>();
        _animator.keepAnimatorStateOnDisable = true;
    }
    #endregion

    #region Player in range

    private void Start()
    {
            StartCoroutine(ProjectileDelay());
    }

    private void Update()
    {
        if (_enemyController2D.collisionState.below)
        {
            // Play walk animation
            _animator.SetInteger("animState", 1);
        }

        if (_spriteRenderer.isVisible)
        {
            _playerInRange = Physics2D.Raycast(transform.position,
                                                _player.transform.position - transform.position,
                                                range,
                                                playerMask);
            
            
            if (_playerInRange && _canShoot)
            {
                Debug.DrawRay(transform.position, _player.transform.position - transform.position, Color.red);
                StartCoroutine(Shoot());
                StartCoroutine(ProjectileDelay());
            } 
        }
    }
    #endregion

    #region Shoot

    IEnumerator Shoot()
    {
        // Play shoot animation
        _animator.SetInteger("animState", 4);

        yield return new WaitForSeconds(shootDelay);

        // If we're facing right:
        if (transform.position.x < projectileSpawnPoint.position.x && transform.position.x < _player.transform.position.x)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rididbody = projectile.GetComponent<Rigidbody2D>();
            rididbody.linearVelocity = transform.TransformDirection(Vector3.right * _projectileSpeed);
        }
        // If we're facing left:
        else if (transform.position.x > projectileSpawnPoint.position.x && transform.position.x > _player.transform.position.x)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            Rigidbody2D rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.linearVelocity = transform.TransformDirection(Vector3.left * _projectileSpeed);
        }
    }

    // This co-routine determines how often the Enemy is able to shoot a projectile.
    IEnumerator ProjectileDelay()
    {
        _canShoot = false;
        yield return new WaitForSeconds(projectileFireRate);
        _canShoot = true;
    }
    #endregion
}
