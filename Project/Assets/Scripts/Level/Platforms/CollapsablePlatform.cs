using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Class Description:
/* 
 * This class, when placed on a GameObject with a 'Rigidbody2D' and 
 * a 'BoxCollider2D' component, makes it collapse, or fall after the player stands on it.
 */
#endregion

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class CollapsablePlatform : MonoBehaviour
{
    #region Fields

    #region Public Fields:
    [Header("Platform")]

    // The time until the platform collapses
    public float timeTillCollapse = 1.0f;

    // The mass of the platform
    public float platformMass = 1.0f;

    // 'platformGravityScale' should be set to about 10% of the 'PlayerMovement' script's 'gravity' variable
    // if you want the platforms to fall at the same speed as the player
    public float platformGravityScale = 2.5f;

    // The time until the platform is destroyed
    public float timeTillDestruction = 3.0f;

    [Header("Effects")]

    // Visual effects
    public GameObject vfx; 

    // Sound effects
    public GameObject sfx;
    #endregion

    #region Non-Serialized Public Fields:

    // To tell if player is on the platform
    [System.NonSerialized]
    public bool playerOnPlatform;
    #endregion

    #region Private Fields:

    // A reference to the Box Collider component
    private BoxCollider2D _boxCollider;

    // A refernce to the Rigidbody component
    private Rigidbody2D _rigidBody;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get Box Collider component
        _boxCollider = GetComponent<BoxCollider2D>();

        // Get Rigidbody component
        _rigidBody = GetComponent<Rigidbody2D>();

        // Set Gravity Scales
        _rigidBody.gravityScale = 0f;
    }
    #endregion

    #region Collapse Platform

    // Function to collapse the platform
    public void CollapsePlatform()
    {
        StartCoroutine(PlatformWaitTime());
    }
    #endregion

    #region Timer
  
    IEnumerator PlatformWaitTime()
    {
        // This co-routine, once started, waits a set amount of time, 
        // then activates the object's 'Rigidbody2D' component, causing it to fall.
        yield return new WaitForSeconds(timeTillCollapse);

        if (vfx != null)
        {
            Instantiate(vfx, this.transform.position, Quaternion.identity);
        }

        if (sfx != null)
        {
            Instantiate(sfx, this.transform.position, Quaternion.identity);
        }

        _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        _rigidBody.mass = platformMass;
        _rigidBody.gravityScale = platformGravityScale;
        _boxCollider.enabled = false;

        // It then waits another set amount of time before destroying the object.
        yield return new WaitForSeconds(timeTillDestruction);
        Destroy(gameObject);
    }
    #endregion
}