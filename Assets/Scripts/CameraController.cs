using Inputs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _mouseX;
    private float _mouseY;

    [Header("Script References")]
    [SerializeField] private Transform _targetObject;
    [SerializeField] private InputReader _inputReader;

    [Header("Mouse Settings")]
    [SerializeField] private float _mouseXSensitivity = 5f;
    [SerializeField] private float _mouseYSensitivity = 5f;

    [Header("Camera Settings")]
    [SerializeField] private float _maxEnemyRayDetection = 100f;
    [SerializeField] private LayerMask _enemyLayerMask;

    public event Action<bool> OnEnemyDetection = delegate { };

    private void Start()
    {
        // Prevents camera from looking down at the start of the scene
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        if (!_inputReader) return;

        _inputReader.OnMouseXInput += GetMouseX;
        _inputReader.OnMouseYInput += GetMouseY;
    }

    private void OnDisable()
    {
        _inputReader.OnMouseXInput -= GetMouseX;
        _inputReader.OnMouseYInput -= GetMouseY;
    }

    private void Awake()
    {
        _inputReader = FindAnyObjectByType<InputReader>();
        if (!_targetObject)
        {
            Debug.LogError($"{name}: No target assigned!\n" +
                $"Disabling to avoid errors!");
            enabled = false;
            return;
        }
    }
    private void GetMouseX(float desiredX) => _mouseX = desiredX;
    private void GetMouseY(float desiredY) => _mouseY = desiredY;

    private void LateUpdate()
    {
        if (!_targetObject)
            return;

        _mouseX *= _mouseXSensitivity * Time.deltaTime;
        _mouseY *= _mouseYSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, _mouseX, Space.World);
        transform.Rotate(Vector3.right, -_mouseY, Space.Self);

        transform.position = _targetObject.position;
        
        Ray pointToObj = new(transform.position, transform.forward);
        if (Physics.Raycast(pointToObj, _maxEnemyRayDetection, _enemyLayerMask))
        {
            OnEnemyDetection?.Invoke(true);
        }
        else
        {
            OnEnemyDetection?.Invoke(false);
        }

        Debug.DrawRay(pointToObj.origin, pointToObj.direction * _maxEnemyRayDetection, Color.yellow);
    }

}
