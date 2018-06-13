using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : Agent
{

    // movement config
    [SerializeField]
    protected float gravity = -25f;
    [SerializeField]
    protected float jumpStartMultiplier = -25f;
    [SerializeField]
    protected float jumpEndMultiplier = -25f;
    [SerializeField]
    protected float groundDamping = 20f; // how fast do we change direction? higher means faster
    [SerializeField]
    protected float inAirDamping = 5f;
    [SerializeField]
    protected float jumpHeight = 3f;

    [HideInInspector]
    protected float normalizedHorizontalSpeed = 0;


    protected RaycastHit2D _lastControllerColliderHit;
   

    protected KeyCode actionKey;

    /// <summary>
    /// mask with all layers that trigger events should fire when intersected
    /// </summary>
    /// 
    [SerializeField]
    protected LayerMask enemyMask = 0;


    protected bool abilityAnimating;


    


    void Update()
    {
        if (_controller.isGrounded)
            _velocity.y = 0;


        normalizedHorizontalSpeed = 1;
        if (transform.localScale.x < 0f)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if( _controller.isGrounded)
        {
            //_animator.Play( Animator.StringToHash( "RangerRun" ) );
            Animation(1); //run anim
            // run sound

        }
 


        // we can only jump whilst grounded
        if (_controller.isGrounded && Input.GetKeyDown(actionKey))
        {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            //_animator.Play( Animator.StringToHash( "RangerJump" ) );
            Animation(2); //jump
            //jump sound
        }

        if (!_controller.isGrounded && Input.GetKeyDown(actionKey))
        {
            //_animator.Play(Animator.StringToHash("RangerPower"));
            Animation(4);
            abilityAnimating = true;
            Ability();
            //shoot sound

        }

        if (!_controller.isGrounded && _velocity.y < 0 && !abilityAnimating)
        {
            // _animator.Play(Animator.StringToHash("RangerFall"));
            Animation(3);
        }

      


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * moveSpeed, Time.deltaTime * smoothedMovementFactor);

        // apply gravity before moving

        if (_velocity.y < 0) // if falling
        {
            gravity = jumpEndMultiplier;
        }
        else if (_velocity.y > 0 && Input.GetKey(actionKey))
        {
            gravity = jumpStartMultiplier;
        }
        else
        {
            gravity = -25f;
        }


        _velocity.y += gravity * Time.deltaTime;

      

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;
    }

    public virtual void Ability()
    {
        //the players jump ability
    }

    public virtual void Animation(int anim)
    {

    }

    public void AnimationEnd()
    {
        abilityAnimating = false;
    }




}

  