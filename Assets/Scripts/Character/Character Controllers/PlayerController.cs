using Inputs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : CharacterMovement
{
    [SerializeField] private Vector2 _playerInput;

    [SerializeField] private InputReader _inputReader;

    private void OnEnable()
    {
        if (!_inputReader)
        {
            Debug.LogError($"{name}: The InputReader is null!");
            return;
        }

        _inputReader.OnMovementInput += MoveCharacter;
        _inputReader.OnJumpInput += Jump;
    }

    private void OnDisable()
    {
        if (!_inputReader)
        {
            Debug.LogError($"{name}: The InputReader is null!");
            return;
        }

        _inputReader.OnMovementInput -= MoveCharacter;
        _inputReader.OnJumpInput -= Jump;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        _characterController.Move(characterSpeed * Time.deltaTime * nextPosition);
        ApplyGravity();
        RotatePlayer();
    }

    public override void MoveCharacter(Vector2 rawDirection)
    {
        Vector3 relativeDirection = new(rawDirection.x, 0.0f, rawDirection.y);
        relativeDirection = relativeDirection.x * transform.right + relativeDirection.z * transform.forward;

        nextPosition = relativeDirection.normalized;
    }

    private void RotatePlayer()
    {
        Camera mainCamera = Camera.main;

        if (!mainCamera)
        {
            Debug.LogError($"{name}: Main Camera is null." +
                "\nDisabling to avoid errors!");
            enabled = false;
            return;
        }

        transform.rotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);
    }

}
