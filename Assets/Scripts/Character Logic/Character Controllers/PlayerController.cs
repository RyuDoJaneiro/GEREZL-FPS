using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : CharacterMovement
{
    [SerializeField] private Vector2 _playerInput;

    [Header("Mouse Settings")]
    [SerializeField] private float _mouseXSensitivity = 5f;
    [SerializeField] private float _mouseYSensitivity = 5f;

    [Header("Gun")]
    [SerializeField] private float _maxGunDistance = 10f;

    private float _mouseX;
    private float _mouseY;
    private Camera _playerCamera;

    private void Start()
    {
        _playerCamera ??= GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        MoveCharacter();
        ApplyGravity();
    }

    private void Update()
    {
        RotateCamera();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _playerInput = context.ReadValue<Vector2>();

        Vector3 relativeDirection = new Vector3(_playerInput.x, 0.0f, _playerInput.y);
        relativeDirection = relativeDirection.x * transform.right + relativeDirection.z * transform.forward;

        nextPosition = relativeDirection.normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Jump();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        Ray ray = new Ray(transform.position, _playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxGunDistance))
        {
            Debug.Log($"{name}: {hitInfo.transform.name}");
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.yellow);
    }

    public void OnMouseX(InputAction.CallbackContext context)
    {
        _mouseX = context.ReadValue<float>();
    }

    public void OnMouseY(InputAction.CallbackContext context)
    {
        _mouseY = context.ReadValue<float>();
    }

    private void RotateCamera()
    {
        _mouseX *= _mouseXSensitivity * Time.deltaTime;
        _mouseY *= _mouseYSensitivity * Time.deltaTime;

        _playerCamera.transform.Rotate(Vector3.up, _mouseX, Space.World);
        _playerCamera.transform.Rotate(Vector3.right, -_mouseY, Space.Self);

        transform.Rotate(Vector3.up, _mouseX, Space.Self);
    }

}
