using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    public static bool attack;
    public static bool interact;
    public KeyCode lastKey;
    private InputAction _attackAction;
    private InputAction _interactionAction;
    private InputAction _moveAction;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _attackAction = _playerInput.actions["Attack"];
        _moveAction = _playerInput.actions["Move"];
        _interactionAction = _playerInput.actions["Interact"];
    }

    private void Update()
    {
        movement = _moveAction.ReadValue<Vector2>();

        _attackAction.performed += ctx => attack = true;
        _attackAction.canceled += ctx => attack = false;

        _interactionAction.performed += ctx => interact = true;
        _interactionAction.canceled += ctx => interact = false;
    }


}
