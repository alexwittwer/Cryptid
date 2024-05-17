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

    private void Awake()
    {
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
        SetMovementKey();
    }

    private void SetMovementKey()
    {
        if (movement.x > 0)
        {
            _lastDirection = "E";
        }
        else if (movement.x < 0)
        {
            _lastDirection = "W";
        }
        else if (movement.y > 0)
        {
            _lastDirection = "N";
        }
        else if (movement.y < 0)
        {
            _lastDirection = "S";
        }
        else
        {
            return;
        }
    }

}
