using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    protected CharacterController _characterController;
    protected Vector3 nextPosition;

    [Header("Character Info.")]
    [SerializeField] protected string characterName;

    [Header("Character Movement")]
    [SerializeField] protected float characterSpeed = 3f;
    [SerializeField] protected float jumpForce = 1f;

    [Header("Character Gravity")]
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float characterGravity = -9.81f;
    [SerializeField] protected float characterGravityMultiplier = 3.0f;
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

    protected void MoveCharacter()
    {
        _characterController.Move(characterSpeed * Time.deltaTime * nextPosition);
    }

    protected void Jump()
    {
        if (!isGrounded) return;

        fallVelocity += jumpForce;
    }

    protected void ApplyGravity()
    {
        isGrounded = _characterController.isGrounded;

        if (isGrounded && fallVelocity < 0.0f)
        {
            fallVelocity = -1.0f;
        }
        else
        {
            fallVelocity += characterGravity * characterGravityMultiplier * Time.deltaTime;
        }

        nextPosition.y = fallVelocity;
    }
}
