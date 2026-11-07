using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/*
 * 
 */
#endregion

public class EnemyProjectile : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Header("Projectile")]
    public int damage = 1;
    public float projectileSpeed = 2f;
    public bool flipSprite = true;
    public float cleanupDelay = 5f;
    public LayerMask _playerCollisionLayer, _levelGeoCollisionLayer;

    [Header("Effects")]
    public GameObject vfx;
    public AudioSource sfx;
    #endregion

    #region Private Fields:
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private CircleCollider2D circCol;

    private GameObject _player;
    private PlayerHealth _playerHealth;
    private PlayerMovement _playerMovement;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get references
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        circCol = GetComponent<CircleCollider2D>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();
        _playerMovement = _player.GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        // Makes sure that the sprite is facing the correct way,
        // by checking the velocity of its RigidBody2D component
        if (_rigidbody.velocity.x < 0f && flipSprite)
        {
            _sprite.flipX = true;
        }
    }
    #endregion

    #region When the Projectile collides with the Player or other object

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to make sure that the projectile isn't hitting the player, 
        // and if not, call the DestroyProjectile function below

        if (col.CompareTag("Player") && circCol.IsTouchingLayers(_playerCollisionLayer))
        {
            _playerMovement.KnockBack(transform);
            _playerHealth.LooseHealth(damage);
            DestroyProjectile();
        }
        else if (circCol.IsTouchingLayers(_levelGeoCollisionLayer))
        {
            DestroyProjectile();
        }

    }

    public void DestroyProjectile()
    {
        if (vfx != null)
        {
            Instantiate(vfx, transform.position, Quaternion.identity);
        }
        if (sfx != null)
        {
            Instantiate(sfx, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
    #endregion

    #region Clean up
    IEnumerator DestroyDelay()
    {
        // This co-routine waits for a set amount of time before destroying the projectile
        // so that we don't end up with millions of projectiles in the scene

        yield return new WaitForSeconds(cleanupDelay);
        Destroy(gameObject);
    }
    #endregion
}
