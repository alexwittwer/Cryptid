using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public Animator anim;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private AudioSource audioSource;


    [Header("Animation States")]
    private int currentState;
    public int IDLEU = Animator.StringToHash("Player_Idle_Up");
    public int RUNU = Animator.StringToHash("Player_Run_Up");
    public int IDLED = Animator.StringToHash("Player_Idle_Down");
    public int RUND = Animator.StringToHash("Player_Run_Down");
    public int IDLES = Animator.StringToHash("Player_Idle_Side");
    public int RUNS = Animator.StringToHash("Player_Run_Side");
    public int ATTACKS = Animator.StringToHash("Player_Attack_Side");
    public int ATTACKU = Animator.StringToHash("Player_Attack_Up");
    public int ATTACKD = Animator.StringToHash("Player_Attack_Down");


    [Header("Audio Clip Indexes")]
    public int walkIndex;
    public int walkIndexSize;
    private float walkTimer = 0f;
    private float walkTime = 0.3f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Flip();
        if (InputManager.Instance.publicMovement != Vector2.zero)
        {
            if (walkTimer <= 0)
            {
                walkTimer = walkTime;
                AudioManager.Instance.PlaySFX(audioSource.clip, .3f);
            }
            else
            {
                walkTimer -= Time.deltaTime;
            }
            OnMove();
        }
        else
        {
            OnIdle();
        }
    }

    private void ChangeAnimationState(int newState)
    {
        if (currentState == newState) return;

        anim.CrossFade(newState, 0.1f, 0);

        currentState = newState;
    }


    private void AttackAnimation()
    {
        if (InputManager.LastDirection == "N")
        {
            ChangeAnimationState(ATTACKU);
        }
        else if (InputManager.LastDirection == "S")
        {
            ChangeAnimationState(ATTACKD);
        }
        else
        {
            ChangeAnimationState(ATTACKS);
        }
    }

    public void OnAttack()
    {
        AttackAnimation();
    }

    public void OnMove()
    {
        RunAnimation();
    }

    public void OnIdle()
    {
        IdleAnimation();
    }

    private void IdleAnimation()
    {
        if (InputManager.LastDirection == "N")
        {
            ChangeAnimationState(IDLEU);
        }
        else if (InputManager.LastDirection == "S")
        {
            ChangeAnimationState(IDLED);
        }
        else
        {
            ChangeAnimationState(IDLES);
        }
    }

    private void RunAnimation()
    {
        if (InputManager.movement.y > 0)
        {
            ChangeAnimationState(RUNU);
        }
        else if (InputManager.movement.y < 0)
        {
            ChangeAnimationState(RUND);
        }
        else
        {
            ChangeAnimationState(RUNS);
        }
    }

    private void Flip()
    {
        if (InputManager.movement.x < 0)
        {
            sr.flipX = false;
        }
        else if (InputManager.movement.x > 0)
        {
            sr.flipX = true;
        }
    }
}
