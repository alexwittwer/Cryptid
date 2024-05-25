using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class controls animations of a given sprite.
/// </summary>
public class AnimatorBrain : MonoBehaviour, IAnimateSprite
{
    public Animator anim;
    public int currentState;
    public int deathStateTime;

    public int IDLE, MOVE, ATTACK, HURT, DEATH, WAKE, SLEEP;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        InitializeHashes();
        deathStateTime = 0;
    }

    public void ChangeAnimationState(int newState)
    {
        if (currentState == newState) return;
        anim.CrossFade(newState, 0f, 0);
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

    public void OnDeath()
    {
        ChangeAnimationState(DEATH);
        float time = Time.deltaTime;
        OnObjectDestroyed(1f);
    }

    public void OnWake()
    {
        ChangeAnimationState(WAKE);
    }

    public void OnSleep()
    {
        ChangeAnimationState(SLEEP);
    }

    public void OnObjectDestroyed(float time)
    {
        Destroy(gameObject, time);
    }

    private void InitializeHashes()
    {
        IDLE = Animator.StringToHash("Idle");
        MOVE = Animator.StringToHash("Move");
        ATTACK = Animator.StringToHash("Attack");
        HURT = Animator.StringToHash("Hurt");
        DEATH = Animator.StringToHash("Death");
        WAKE = Animator.StringToHash("Wake");
        SLEEP = Animator.StringToHash("Sleep");
    }
}
