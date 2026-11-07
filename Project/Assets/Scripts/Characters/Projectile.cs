using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class defines a projectile that the player can shoot, which will do damage to enemies.
*/
#endregion

public class Projectile : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Header("Projectile")]
    public float projectileSpeed = 2f;
    public bool flipSprite = true;
    public float cleanupDelay = 8f;

    [Header("Effects:")]
    public GameObject vfx; 
    public AudioSource sfx;
    #endregion

    #region Private Fields:
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private LayerMask _collisionLayers;
    private CircleCollider2D circCol;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get references
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        circCol = GetComponent<CircleCollider2D>();
        _collisionLayers = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>().projectileCollision;
    }

    private void Start()
    {
        // Makes sure that the sprite is facing the correct way, by checking the velocity of its RigidBody2D component.
        if (_rigidbody.linearVelocity.x < 0f && flipSprite)
        {
            _sprite.flipX = true;
        }
    }
    #endregion

    #region When the Projectile collides with an Enemy or other object

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check to make sure that the projectile isn't hitting the player, 
        // and if not, call the DestroyProjectile function below.
        if (!col.CompareTag("Player") && circCol.IsTouchingLayers(_collisionLayers))
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
        // so that we don't end up with millions of projectiles in the scene.
        yield return new WaitForSeconds(cleanupDelay);
        Destroy(gameObject);
    }
    #endregion
}
