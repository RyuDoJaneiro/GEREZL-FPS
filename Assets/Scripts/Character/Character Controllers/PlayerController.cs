using Inputs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GunLogic))]
public class PlayerController : CharacterMovement
{
    [SerializeField] private Vector2 _playerInput;

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GunLogic _gunLogic;
    private Camera mainCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (!_inputReader)
        {
            Debug.LogError($"{name}: The InputReader is null!");
            return;
        }

        _inputReader.OnMovementInput += MoveCharacter;
        _inputReader.OnJumpInput += Jump;
        _inputReader.OnShootInput += _gunLogic.Shoot;
        _inputReader.OnReloadInput += _gunLogic.Reload;
    }

    private void OnDisable()
    {
        _inputReader.OnMovementInput -= MoveCharacter;
        _inputReader.OnJumpInput -= Jump;
        _inputReader.OnShootInput -= _gunLogic.Shoot;
        _inputReader.OnReloadInput -= _gunLogic.Reload;
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
