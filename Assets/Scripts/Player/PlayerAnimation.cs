using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationScript : MonoBehaviour
{
    [Header("Animation Script")]
    [SerializeField] public Animator anim;
    [SerializeField] private KeyCode lastKey { get; set; }
    [SerializeField] private string currentState;

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
        GetLastKey();
        if (InputManager.attack)
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
    private void changeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    private void AttackAnimation()
    {
        if (lastKey == KeyCode.W)
        {
            changeAnimationState(StateName.ATTACKU);
        }
        else if (lastKey == KeyCode.S)
        {
            changeAnimationState(StateName.ATTACKD);
        }
        else if (lastKey == KeyCode.A || lastKey == KeyCode.D)
        {
            changeAnimationState(StateName.ATTACKS);
        }
        else
        {
            changeAnimationState(StateName.ATTACKS);
        }
    }

    private void IdleAnimation()
    {
        if (lastKey == KeyCode.W)
        {
            changeAnimationState(StateName.IDLEU);
        }
        else if (lastKey == KeyCode.S)
        {
            changeAnimationState(StateName.IDLED);
        }
        else if (lastKey == KeyCode.A || lastKey == KeyCode.D)
        {
            changeAnimationState(StateName.IDLES);
        }
    }


    private void RunAnimation()
    {
        if (InputManager.movement.y > 0)
        {
            changeAnimationState(StateName.RUNU);
        }
        else if (InputManager.movement.y < 0)
        {
            changeAnimationState(StateName.RUND);
        }
        else
        {
            changeAnimationState(StateName.RUNS);
        }
    }

    private void GetLastKey()
    {
        lastKey = Input.GetKey(KeyCode.W)
            ? KeyCode.W
            : Input.GetKey(KeyCode.S)
            ? KeyCode.S
            : Input.GetKey(KeyCode.A)
            ? KeyCode.A
            : Input.GetKey(KeyCode.D)
            ? KeyCode.D
            : lastKey;
    }
}
