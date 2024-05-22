using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

// <summary>
// This class is responsible for controlling animations of a given sprite. 
// </summary>
public class AnimatorBrain : MonoBehaviour, IAnimateSprite
{

    public Animator anim;
    public int currentState;
    private bool _hasBeenSet = false;
    private bool _damageAnimationFinished = false;
    public bool DeathAnimationFinished
    {
        get
        {
            return _damageAnimationFinished;
        }
        private set
        {
            _damageAnimationFinished = value;
            OnDeathEvent?.Invoke();
            Debug.Log("Death event invoked");
        }
    }
    public int IDLE, MOVE, ATTACK, HURT, DEATH, WAKE;
    public UnityAction OnDeathEvent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        InitializeHashes();
    }

    void Start()
    {
        ListAllAttachedEvents(OnDeathEvent);
    }

    private void CheckDeathAnimation(bool hasBeenSet)
    {
        // if the death animation has been set
        if (hasBeenSet)
        {
            // check if the death animation is playing
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                // check if the death animation has finished
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    // set the death animation to false
                    DeathAnimationFinished = true;
                    currentState = DEATH;
                }
            }
        }
    }


    private void ListAllAttachedEvents(UnityAction unityEvent = null)
    {
        unityEvent.GetInvocationList().ToList().ForEach(x => Debug.Log(x.Method.Name));
    }

    public void ChangeAnimationState(int newState)
    {
        CheckDeathAnimation(_hasBeenSet);
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
        _hasBeenSet = true;
        ChangeAnimationState(DEATH);
    }

    public void OnWake()
    {
        ChangeAnimationState(WAKE);
    }

    private void InitializeHashes()
    {
        IDLE = Animator.StringToHash("Idle");
        MOVE = Animator.StringToHash("Move");
        ATTACK = Animator.StringToHash("Attack");
        HURT = Animator.StringToHash("Hurt");
        DEATH = Animator.StringToHash("Death");
        WAKE = Animator.StringToHash("Wake");
    }


}