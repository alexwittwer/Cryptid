using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour, IAnimateSprite
{
    [SerializeField] private Animator anim;
    private int currentState;

    public int HURT = Animator.StringToHash("Hurt");
    public int IDLE = Animator.StringToHash("Idle");
    public int MOVE = Animator.StringToHash("Move");
    public int ATTACK = Animator.StringToHash("Attack");

    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = IDLE;
    }

    public void ChangeAnimationState(int newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    public void OnHit()
    {
        ChangeAnimationState(HURT);
    }

    public void OnAttack()
    {
        ChangeAnimationState(ATTACK);
    }

    public void OnMove()
    {
        ChangeAnimationState(MOVE);
    }

    public void OnIdle()
    {
        ChangeAnimationState(IDLE);
    }
}
