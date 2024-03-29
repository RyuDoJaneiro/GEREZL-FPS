using Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _mouseX;
    private float _mouseY;

    [Header("Script References")]
    [SerializeField] private Transform targetObject;
    [SerializeField] private InputReader inputReader;

    [Header("Mouse Settings")]
    [SerializeField] private float _mouseXSensitivity = 5f;
    [SerializeField] private float _mouseYSensitivity = 5f;

    private void OnEnable()
    {
        if (!inputReader) return;

        inputReader.OnMouseXInput += GetMouseX;
        inputReader.OnMouseYInput += GetMouseY;
    }

    private void OnDisable()
    {
        if (!inputReader) return;

        inputReader.OnMouseXInput -= GetMouseX;
        inputReader.OnMouseYInput -= GetMouseY;
    }

    private void Awake()
    {
        if (!targetObject)
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
        _mouseX *= _mouseXSensitivity * Time.deltaTime;
        _mouseY *= _mouseYSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up, _mouseX, Space.World);
        transform.Rotate(Vector3.right, -_mouseY, Space.Self);

        transform.position = targetObject.position;
    }

}
