using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    private static bool _attack;
    private static bool _interact;
    private static string _lastDirection;

    [SerializeField] public Vector2 publicMovement;
    [SerializeField] public bool publicAttack;
    [SerializeField] public bool publicInteract;
    [SerializeField] public string publicLastDirection;

    public static string LastDirection
    {
        get => _lastDirection;
        set => _lastDirection = value;
    }
    public static bool Attack
    {
        get => _attack;
        set
        {
            _attack = value;
            attackAction?.Invoke(value);
        }
    }
    public static bool Interact
    {
        get => _interact;
        set
        {
            _interact = value;
            interactAction?.Invoke(value);
        }
    }

    private PlayerInput _playerInput;
    private InputAction _attackAction;
    private InputAction _interactionAction;
    private InputAction _moveAction;


    public static UnityAction<bool> attackAction;
    public static UnityAction<bool> interactAction;

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InputManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        _playerInput = GetComponent<PlayerInput>();
        _attackAction = _playerInput.actions["Attack"];
        _moveAction = _playerInput.actions["Move"];
        _interactionAction = _playerInput.actions["Interact"];

        _attackAction.started += ctx => Attack = true;
        _attackAction.canceled += ctx => Attack = false;

        _interactionAction.started += ctx => Interact = true;
        _interactionAction.canceled += ctx => Interact = false;
    }
    private void Update()
    {
        movement = _moveAction.ReadValue<Vector2>();
        publicMovement = movement;
        publicAttack = _attack;
        SetMovementKey();
    }

    private void SetMovementKey()
    {
        if (movement.x > 0)
        {
            publicLastDirection = "E";
            _lastDirection = "E";
        }
        else if (movement.x < 0)
        {
            publicLastDirection = "W";
            _lastDirection = "W";
        }
        else if (movement.y > 0)
        {
            publicLastDirection = "N";
            _lastDirection = "N";
        }
        else if (movement.y < 0)
        {
            publicLastDirection = "S";
            _lastDirection = "S";
        }
        else
        {
            return;
        }
    }

    public InputManager GetInstance()
    {
        return this;
    }
}
