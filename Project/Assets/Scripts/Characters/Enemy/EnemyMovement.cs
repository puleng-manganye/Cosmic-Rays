using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region Class Description:
/* 
 * This class mainly calculates a variable named '_velocity' which the 'CharacterController2D' script needs in order to work. 
 * It uses some rudimentary conditions to move the enemy around the level without the need for input. 
 * Most of the important variables are exposed in the inspector, so you shouldn't need to modify this script much!
 */
#endregion

public class EnemyMovement : MonoBehaviour
{
    #region Fields
    ///////////////////////////////////////////////////////////////

    #region Initializations:

    // A reference to the Character Controller
    private CharacterController2D _controller;

    [Header("Initializations")]

    // A reference to the Enemy's graphics
    public GameObject enemyGFX;
    #endregion

    #region Public Fields:

    #region Movement

    [Header("Movement")]

    // Gravity
    public float gravity = -25f;

    // Move speed
    public float moveSpeed = 2f;

    // Turn speed
    public float turnSpeed = 10f;
    #endregion

    #region Jumping

    [Header("Jumping")]

    // Jump On
    public bool jumpOn;

    // Jump Heaight
    public float jumpHeight = 1.5f;

    // Jump Delay
    public float jumpDelay = 2f;

    // Bouncing On
    public bool bouncingOn;

    // Bounce Height
    public float bounceHeight = 1f;
    #endregion

    #region Miscellaneous

    [Header("Miscellaneous")]

    // // Debugging Rays
    public bool debuggingRays;
    #endregion

    #endregion

    #region Public Non-Serialized Fields:

    // Movement Direction
    [System.NonSerialized]
    public int _movementDirection;

    [System.NonSerialized]
    public Vector3 _leftMid;

    [System.NonSerialized]
    public Vector3 _rightMid;

    [System.NonSerialized]
    public Vector3 _topLeft;

    [System.NonSerialized]
    public Vector3 _topMid;

    [System.NonSerialized]
    public Vector3 _topRight;

    [System.NonSerialized]
    public Vector3 _bottomLeft;

    [System.NonSerialized]
    public Vector3 _bottomMid;

    [System.NonSerialized]
    public Vector3 _bottomRight;
    #endregion

    #region Private Fields:

    // Player
    private GameObject _player;

    // Box Collider
    private Collider2D _boxCollider;

    // Velocity
    private Vector3 _velocity;

    // Direction
    private Vector2 _direction;

    // Can Flip
    private bool canFlip = true;

    // Can Jump
    private bool canJump = true;
    #endregion

    ///////////////////////////////////////////////////////////////
    #endregion

    #region References and Initializations

    private void Awake()
    {
        // Get the Player
        _player = GameObject.FindGameObjectWithTag("Player");

        // Get the Character Controller
        _controller = GetComponent<CharacterController2D>();

        // Get the Box Collider
        _boxCollider = GetComponent<BoxCollider2D>();

        // Initialize Direction
        _direction = new Vector2(1, 0);
    }
    #endregion

    #region Updates

    private void Update()
    {
        float delta = Time.deltaTime;

        RaycastingPoints();
        InitialMovementCalculations(delta);

        // Check to see if we can jump, and if we can, call the 'Jump()' function.
        if (jumpOn && _controller.isGrounded && canJump)
        {
            Jump(delta);
        }

        // Set gravity
        _velocity.y += gravity * delta;

        // Pass the previously calculated '_velocity' to the 'CharacterController2D' script to process movement.
        _controller.move(_velocity * delta);
        _velocity = _controller.velocity;
    }
    #endregion

    #region Movement Functions:

    private void InitialMovementCalculations(float delta)
    {
        // Check to see if we're colliding with a wall, and if so turn around.  
        if (_controller.collisionState.left || _controller.collisionState.right)
        {
            if (_controller.collisionState.left)
            {
                _direction.x = 1f;
            }
            else if (_controller.collisionState.right)
            {
                _direction.x = -1f;
            }

            if (enemyGFX != null && canFlip == true)
            {
                enemyGFX.transform.localScale = new Vector3(-enemyGFX.transform.localScale.x,   //x
                                                            enemyGFX.transform.localScale.y,    //y
                                                            enemyGFX.transform.localScale.z);   //z
                StartCoroutine(TurnWaitTime());
            }
        }

        // Set X velocity based on some variables.
        _velocity.x = Mathf.Lerp(_velocity.x, _direction.x * moveSpeed, delta * turnSpeed);

        // Set the 'movementDirection' variable.
        // Used by other functions to tell which direction we're moving in.
        // Read from velocity rather than input.
        if (_controller.velocity.x < -0.1)
        {
            _movementDirection = -1;
        }
        else if (_controller.velocity.x > 0.1)
        {
            _movementDirection = 1;
        }
        else
        {
            _movementDirection = 0;
        }
    }
    #endregion

    #region Jumping/Bouncng Functions:

    // If we're jumping, calculate how high, and how long to wait before jumping again.
    private void Jump(float delta)
    {
        _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
        if (jumpDelay > 0)
        {
            StartCoroutine(JumpDelay());
        }
    }

    public void Bounce()
    {
        //Debug.Log("Bounce!");
        _velocity.y = Mathf.Sqrt(2f * bounceHeight * -gravity);
    }
    #endregion

    #region Raycasting:

    private void RaycastingPoints()
    {
        // Top points:
        _topLeft = new Vector3(transform.position.x - ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),      // x
                                    transform.position.y + ((_boxCollider.bounds.size.y * 0.5f) - 0.1f), // y
                                    transform.position.z);                                               // z
        _topMid = new Vector3(transform.position.x,
                                    transform.position.y + ((_boxCollider.bounds.size.y * 0.5f) - 0.1f),
                                    _boxCollider.bounds.center.z);
        _topRight = new Vector3(transform.position.x + ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),
                                    transform.position.y + ((_boxCollider.bounds.size.y * 0.5f) - 0.1f),
                                    _boxCollider.bounds.center.z);
        // Left points:
        _leftMid = new Vector3(transform.position.x - ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),
                                        transform.position.y,
                                        transform.position.z);
        // Right points:
        _rightMid = new Vector3(transform.position.x + ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),
                                    transform.position.y,
                                    transform.position.z);
        // Bottom points:
        _bottomLeft = new Vector3(transform.position.x - ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),      
                                    transform.position.y + ((_boxCollider.bounds.size.y * 0.15f) - 0.1f),
                                    transform.position.z);
        _bottomMid = new Vector3(transform.position.x,
                                   transform.position.y + ((_boxCollider.bounds.size.y * 0.15f) - 0.1f),
                                   _boxCollider.bounds.center.z);
        _bottomRight = new Vector3(transform.position.x + ((_boxCollider.bounds.size.x * 0.5f) - 0.1f),
                                    transform.position.y + ((_boxCollider.bounds.size.y * 0.15f) - 0.1f),
                                    _boxCollider.bounds.center.z);

        // Debugging rays.
        if (debuggingRays)
        {
            Debug.DrawRay(_topLeft, Vector3.up, Color.green);
            Debug.DrawRay(_topMid, Vector3.up, Color.green);
            Debug.DrawRay(_topRight, Vector3.up, Color.green);
            Debug.DrawRay(_leftMid, Vector3.left, Color.green);
            Debug.DrawRay(_rightMid, Vector3.right, Color.green);
            Debug.DrawRay(_bottomLeft, Vector3.down, Color.green);
            Debug.DrawRay(_bottomMid, Vector3.down, Color.green);
            Debug.DrawRay(_bottomRight, Vector3.down, Color.green);
        }
    }
    #endregion

    #region Co-Routines:

    // When turning, we need to disable the flip function for a moment
    // to avoid sometimes "re-flipping" before we're finished turning.
    IEnumerator TurnWaitTime()
    {
        canFlip = false;
        yield return new WaitForSeconds(0.1f);
        canFlip = true;
    }

    // This co-routine determines how long the delay will be between jumps.
    // Remember that the delay is measured in seconds from when the jump starts, not when it ends. 
    IEnumerator JumpDelay()
    {
        canJump = false;
        yield return new WaitForSeconds(jumpDelay);
        canJump = true;
    }
    #endregion
}
