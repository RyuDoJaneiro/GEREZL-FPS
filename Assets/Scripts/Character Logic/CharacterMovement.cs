using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [Header("Character Info.")]
    [SerializeField] protected string characterName;

    [Header("Character Movement")]
    [SerializeField] protected float characterSpeed = 3f;
    [SerializeField] protected float jumpForce = 1f;

    [Header("Character Gravity")]
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float characterGravity;
    [SerializeField] protected float fallVelocity;

    private void Reset()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnValidate()
    {
        _characterController ??= GetComponent<CharacterController>();
    }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    protected void MoveCharacter(Vector3 desiredDirection)
    {
        // Set gravity
        desiredDirection.y = fallVelocity;

        Vector3 nextPosition = characterSpeed * Time.deltaTime * desiredDirection.normalized;
        _characterController.Move(nextPosition);
    }

    protected void Jump()
    {
        Debug.Log($"{name}: The jump logic is not implemented yet!");
    }

    protected void ApplyGravity()
    {
        isGrounded = _characterController.isGrounded;

        if (!isGrounded)
        {
            fallVelocity -= characterGravity * Time.deltaTime;
            fallVelocity = Mathf.Clamp(fallVelocity, -5, 20);
        }
    }
}
