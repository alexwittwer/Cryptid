using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimator : MonoBehaviour, IAnimateSprite
{
    [SerializeField] private Animator anim;
    private string currentState;

    public string HURT => "Slime_red_Flash";
    public string IDLE => "Slime_red_Hop";
    public string MOVE => "Slime_red_Hop";
    public string ATTACK => "Slime_red_Hop";

    void FixedUpdate()
    {
        currentState ??= IDLE;

    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    public void OnHit()
    {
        ChangeAnimationState(HURT);
    }

    public string GetCurrentState()
    {
        return currentState;
    }
}
