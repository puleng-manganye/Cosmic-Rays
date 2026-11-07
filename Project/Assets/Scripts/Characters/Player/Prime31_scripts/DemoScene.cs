using UnityEngine;
using System.Collections;
using Prime31;

#region Class Description:
/*
 * 
 */
#endregion

public class DemoScene : MonoBehaviour
{
	#region Fields

	#region Public Fields:
	// movement configuration
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; 
	
	// how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;
	#endregion

	#region Private Fields:
	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
	#endregion

	#endregion

	#region References and Initializations

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}
	#endregion

	#region Event Listeners

	private void onControllerCollider(RaycastHit2D hit)
	{
		// bail out on plain old ground hits cause they arent very interesting
		if (hit.normal.y == 1f)
		{
			return;
		}

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}

	private void onTriggerEnterEvent(Collider2D col)
	{
		Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
	}

	private void onTriggerExitEvent(Collider2D col)
	{
		Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
	}
    #endregion

    #region Moving the character around and controlling the animation

    // the Update loop contains a very simple example of moving the character around and controlling the animation
    private void Update()
	{
		if (_controller.isGrounded)
		{
			_velocity.y = 0;
		}

		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			normalizedHorizontalSpeed = 1;

			if (transform.localScale.x < 0f)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}

			if (_controller.isGrounded)
			{
				_animator.Play(Animator.StringToHash("Run"));
			}
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;

			if (transform.localScale.x > 0f)
			{
				transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
			}

			if (_controller.isGrounded)
			{
				_animator.Play(Animator.StringToHash("Run"));
			}
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if (_controller.isGrounded)
			{
				_animator.Play(Animator.StringToHash("Idle"));
			}
		}

		// we can only jump whilst grounded
		if (_controller.isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
		{
			_velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
			_animator.Play(Animator.StringToHash("Jump"));
		}

		// apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp (_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		// if holding down bump up our movement amount and turn off one way platform detection for a frame.
		// this lets us jump down through one way platforms
		if (_controller.isGrounded && Input.GetKey(KeyCode.DownArrow))
		{
			_velocity.y *= 3f;
			_controller.ignoreOneWayPlatformsThisFrame = true;
		}

		_controller.move(_velocity * Time.deltaTime);

		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;
	}
	#endregion
}
