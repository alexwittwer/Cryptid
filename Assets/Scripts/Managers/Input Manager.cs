using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 movement;
    public static bool attack;
    public KeyCode lastKey;
    private InputAction _attackAction;
    private InputAction _moveAction;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _attackAction = _playerInput.actions["Attack"];
    }

    private void Update()
    {
        movement = _moveAction.ReadValue<Vector2>();
        attack = _attackAction.triggered;
    }
}
