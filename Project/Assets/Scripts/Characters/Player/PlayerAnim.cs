using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region Class Description:
/*
 * Animations are controlled by the 'Moving Entity - Animation Controller' Animatior Controller.
 * The controller has a parameter named 'animState' which controls which animation plays.
 * 'animState' is an integer, here are the animations that it's value represents:
 * 
 * 0 = idle
 * 1 = walk
 * 2 = jumping (blend tree containing 2 clips, controlled by 'jumpFallBlend' float parameter, remeber to set it!)
 * 3 = duck
 * 4 = shoot
 * 
 * You can add more animations if you need to, just keep counting up from 5...
 */
#endregion

public class PlayerAnim : MonoBehaviour
{
    #region Fields

    //private PlayerMovement _playerMovement;
    private GameObject _player;
    private CharacterController2D _playerController2D;
    private Animator _animator;

    // SFX
    public AudioSource sfx;

    #endregion

    #region References and Initialisations

    private void Start()
    {   
        //_playerMovement = _player.GetComponent<PlayerMovement>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerController2D = _player.GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
        _animator.keepAnimatorStateOnDisable = true;
    }
    #endregion

    #region Updating the Animation States

    private void Update()
    {
        if (_playerController2D.collisionState.below)
        {
           // Debug.Log("on ground");

            if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0)
            {
                // 1 = walk
                _animator.SetInteger("animState", 1);
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                // 0 = idle
                _animator.SetInteger("animState", 0);
            }
        }
        else if (!_playerController2D.collisionState.below)
        {
            if (_playerController2D.velocity.y < 0)
            {
                // 2 (0) = jump
                _animator.SetInteger("animState", 2); 
                _animator.SetFloat("jumpFallBlend", 0);
            } 
            else if (_playerController2D.velocity.y > 0)
            {
                // 2 (1) = fall
                _animator.SetInteger("animState", 2);
                _animator.SetFloat("jumpFallBlend", 1);

                
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sfx.Play();
        }
    }
    #endregion
}
