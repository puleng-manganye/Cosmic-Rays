using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

#region Class Description:
/*
 * Animations are controlled by the 'Enemy - Animation Controller' Animatior Controller.
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

public class EnemyAnim : MonoBehaviour
{
    #region Fields

    // A reference to the Enemy
    public GameObject enemy;

    // A reference to the Character Controller
    private CharacterController2D _enemyController2D;

    // A reference to the Animator
    private Animator _animator;

    #endregion

    #region References and Initialisations

    private void Start()
    {
        // Get the Enemy
        //_enemy = GameObject.FindGameObjectWithTag("Enemy");

        // Get the Character Controller
        _enemyController2D = enemy.GetComponent<CharacterController2D>();

        // Get the Animator
        _animator = GetComponent<Animator>();

        _animator.keepAnimatorStateOnDisable = true;
    }
    #endregion

    #region Updating the Animation States

    private void Update()
    {
        if (_enemyController2D.collisionState.below)
        {
                // 1 = walk
                _animator.SetInteger("animState", 1);
        }
        else if (!_enemyController2D.collisionState.below)
        {
            if (_enemyController2D.velocity.y < 0)
            {
                // 2 (0) = jump
                _animator.SetInteger("animState", 2);
                _animator.SetFloat("jumpFallBlend", 0);
            }
            else if (_enemyController2D.velocity.y > 0)
            {
                // 2 (1) = fall
                _animator.SetInteger("animState", 2);
                _animator.SetFloat("jumpFallBlend", 1);
            }
        }
    }
    #endregion
}
