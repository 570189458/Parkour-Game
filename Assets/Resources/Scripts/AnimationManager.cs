using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager _instance;

    public Animation anim;

    public AnimationClip Dead;
    public AnimationClip JumpDown;
    public AnimationClip JumpLoop;
    public AnimationClip JumpUp;
    public AnimationClip Roll;
    public AnimationClip Run;
    public AnimationClip TurnLeft;
    public AnimationClip TurnRight;

    public delegate void AnimationHandler();

    public AnimationHandler animationHandler;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        anim = GetComponent<Animation>();
        animationHandler = PlayRun;
    }

    // Update is called once per frame
    void Update()
    {
        if(animationHandler!=null)
        {
            animationHandler();
        }
    }

    public void PlayDead()
    {
        anim.Play(Dead.name);
        if (anim[Dead.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }

    public void PlayJumpDown()
    {
        anim.Play(JumpDown.name);
        if (anim[JumpDown.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }

    public void PlayJumpLoop()
    {
        anim.Play(JumpLoop.name);
        if (anim[JumpLoop.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }

    public void PlayJumpUp()
    {
        anim.Play(JumpUp.name);
        if (anim[JumpUp.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }

    public void PlayDoubleJump()
    {
        anim.Play(Roll.name);
        if (anim[JumpUp.name].normalizedTime >= 0.95f)
            animationHandler = PlayJumpLoop;
    }

    public void PlayRoll()
    {
        anim.Play(Roll.name);
        if (anim[Roll.name].normalizedTime >= 0.95f)
        {
            animationHandler = PlayRun;
            PlayerControl._instance.isRoll = false;
        }
        else
        {
            PlayerControl._instance.isRoll = true;
        }
    }

    public void PlayRun()
    {
        anim.Play(Run.name);
    }

    public void PlayTurnLeft()
    {
        anim.Play(TurnLeft.name);
        if (anim[TurnLeft.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }

    public void PlayTurnRight()
    {
        anim.Play(TurnRight.name);
        if (anim[TurnRight.name].normalizedTime >= 0.95f)
            animationHandler = PlayRun;
    }
}
