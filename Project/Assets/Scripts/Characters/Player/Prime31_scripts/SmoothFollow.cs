using UnityEngine;
using System.Collections;
using Prime31;

#region Class Description:
/*
 * 
 */
#endregion

public class SmoothFollow : MonoBehaviour
{
    #region Fields

    #region Public Fields:

    public Transform target;
	public float smoothDampTime = 0.2f;
	[HideInInspector]
	public new Transform transform;
	public Vector3 cameraOffset;
	public bool useFixedUpdate = false;
	#endregion

	#region Private Fields:

	private CharacterController2D _playerController;
	private Vector3 _smoothDampVelocity;
    #endregion

    #endregion

    #region References and Initializations

    private void Awake()
	{
		// Get references
		transform = gameObject.transform;
		_playerController = target.GetComponent<CharacterController2D>();
    }
    #endregion

    #region Update Camera Position 

    private void LateUpdate()
	{
		if (!useFixedUpdate)
		{
			updateCameraPosition();
		}
	}

	private void FixedUpdate()
	{
		if (useFixedUpdate)
		{
			updateCameraPosition();
		}
	}

	private void updateCameraPosition()
	{
		if( _playerController == null )
		{
			transform.position = Vector3.SmoothDamp( transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime );
			return;
		}
		
		if( _playerController.velocity.x > 0 )
		{
			transform.position = Vector3.SmoothDamp( transform.position, target.position - cameraOffset, ref _smoothDampVelocity, smoothDampTime );
		}
		else
		{
			var leftOffset = cameraOffset;
			leftOffset.x *= -1;
			transform.position = Vector3.SmoothDamp( transform.position, target.position - leftOffset, ref _smoothDampVelocity, smoothDampTime );
		}
	}
    #endregion
}
