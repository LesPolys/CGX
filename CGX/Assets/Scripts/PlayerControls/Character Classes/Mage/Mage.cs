using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Mage : Player
{
    float abilityRange;

    [SerializeField]
    protected LayerMask partyMask = 0;

    Mage()
    {
        actionKey = KeyCode.W;
    }

    public override void Animation(int anim)
    {
        switch (anim)
        {

            case 0: // idle
                _animator.Play(Animator.StringToHash("MageIdle"));
                AnimationEnd();
                break;
            case 1: //run
                _animator.Play(Animator.StringToHash("MageRun"));
                AkSoundEngine.PostEvent("Mage_Footsteps", gameObject);
                AnimationEnd();
                break;
            case 2: //jump up
                _animator.Play(Animator.StringToHash("MageJump"));
                AkSoundEngine.PostEvent("Mage_Jump", gameObject);
                break;
            case 3: //fall down
                _animator.Play(Animator.StringToHash("MageFall"));
                break;
            case 4: //ability
                _animator.Play(Animator.StringToHash("MagePower"));
                AkSoundEngine.PostEvent("Mage_Attack", gameObject);
                break;
            case 5: //fainting
                break;
            case 6: //just in case
                break;



        }
    }

    public override void Ability()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, abilityRange, partyMask);
        if (colliders.Length > 0)
        {

        }
    }
}