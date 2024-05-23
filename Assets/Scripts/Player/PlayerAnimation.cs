using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] public Animator anim;
    [SerializeField] private SpriteRenderer sr;


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


    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
        if (InputManager.Instance.publicMovement != Vector2.zero)
        {
            OnMove();
        }
        else
        {
            OnIdle();
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= (anim.GetCurrentAnimatorStateInfo(0).length)
        && (anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack_Side")
        || anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack_Up")
        || anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack_Down")))
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
