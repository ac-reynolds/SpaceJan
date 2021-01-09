using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Manages player actions.  Uses character controller to move the player.  
 * 
 * Place an instance of this script on a gameobject with a charactercontroller that should be
 * directly controlled by player input.  
 */
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MeleeAttack))]
public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed;
	
	enum animation {
			
	}
	
	private Animator _animator;
    private CharacterController _controller;
    private MeleeAttack _meleeAttack;
    private Vector2 _currentVelocity;

    void Start()
    {
        //set player as current active
        EventHandler<SetActiveControllerEvent>.GetInstance().Invoke(new SetActiveControllerEvent(this));
        _controller = GetComponent<CharacterController>();
        _meleeAttack = GetComponent<MeleeAttack>();
        _animator = GetComponent<Animator>();
    }

    /*
     * Moves the character according to the calculated velocity.  
     */
    private void FixedUpdate()
    {
        _controller.Move(_currentVelocity * PlayerSpeed * Time.deltaTime);
    }

    public Vector2 GetForwardFacing() {
        return Vector2.right * transform.localScale.x;
    }

    public void RequestMeleeAttack() {
        _meleeAttack.Attack(GetForwardFacing());
    }
    
    /*
     * Can be called at any time, but does not modify character position immediately.  Instead, it stores
     * input commands in _currentVelocity, to be enacted on the next FixedUpdate().  
     */
    public void RequestMovement(float horizontalMovement, float verticalMovement) {
        _currentVelocity.Set(horizontalMovement, verticalMovement);
        if (_currentVelocity.x > 0) {
            _animator.SetInteger("direction", 2);
        } else if (_currentVelocity.x < 0) {
            _animator.SetInteger("direction", 3);
        } else if (_currentVelocity.y > 0) {
            _animator.SetInteger("direction", 1);
        } else if (_currentVelocity.y < 0) {
            _animator.SetInteger("direction", 0);
        }
    }
}
