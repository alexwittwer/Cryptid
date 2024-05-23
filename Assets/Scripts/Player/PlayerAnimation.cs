using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animation Script")]
    [SerializeField] public Animator anim;
    [SerializeField] private SpriteRenderer sr;
    private string currentState;

    private struct StateName
    {
        public const string IDLEU = "Player_Idle_Up";
        public const string RUNU = "Player_Run_Up";
        public const string IDLED = "Player_Idle_Down";
        public const string RUND = "Player_Run_Down";
        public const string IDLES = "Player_Idle_Side";
        public const string RUNS = "Player_Run_Side";
        public const string ATTACKS = "Player_Attack_Side";
        public const string ATTACKU = "Player_Attack_Up";
        public const string ATTACKD = "Player_Attack_Down";
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Flip();
        if (InputManager.movement != Vector2.zero)
        {
            RunAnimation();
        }
        else
        {
            IdleAnimation();
        }
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    private void AttackAnimation()
    {
        if (InputManager.LastDirection == "N")
        {
            ChangeAnimationState(StateName.ATTACKU);
        }
        else if (InputManager.LastDirection == "S")
        {
            ChangeAnimationState(StateName.ATTACKD);
        }
        else
        {
            ChangeAnimationState(StateName.ATTACKS);
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
            ChangeAnimationState(StateName.IDLEU);
        }
        else if (InputManager.LastDirection == "S")
        {
            ChangeAnimationState(StateName.IDLED);
        }
        else
        {
            ChangeAnimationState(StateName.IDLES);
        }
    }

    private void RunAnimation()
    {
        if (InputManager.movement.y > 0)
        {
            ChangeAnimationState(StateName.RUNU);
        }
        else if (InputManager.movement.y < 0)
        {
            ChangeAnimationState(StateName.RUND);
        }
        else
        {
            ChangeAnimationState(StateName.RUNS);
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
