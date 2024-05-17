using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [Header("Animation Script")]
    [SerializeField] public Animator anim;
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
    }

    private void Update()
    {
        Flip();
        if (InputManager.Attack)
        {
            AttackAnimation();
        }
        else if (InputManager.movement != Vector2.zero)
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
        if (InputManager.movement.y > 0)
        {
            ChangeAnimationState(StateName.ATTACKU);
        }
        else if (InputManager.movement.y < 0)
        {
            ChangeAnimationState(StateName.ATTACKD);
        }
        else
        {
            ChangeAnimationState(StateName.ATTACKS);
        }
    }

    private void IdleAnimation()
    {
        if (InputManager.movement.y > 0)
        {
            ChangeAnimationState(StateName.IDLEU);
        }
        else if (InputManager.movement.y < 0)
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (InputManager.movement.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
