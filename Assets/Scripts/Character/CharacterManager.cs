using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class CharacterManager : MonoBehaviour, IHitteable
{
    protected CharacterController _characterController;
    protected Vector3 nextPosition;

    [Header("Character Info.")]
    [SerializeField] protected string characterName;
    [SerializeField] protected float characterMaxHealth = 100f;
    [SerializeField] protected float characterActualHealth = 100f; 

    [Header("Character Movement")]
    [SerializeField] protected float characterSpeed = 3f;
    [SerializeField] protected float jumpForce = 1f;

    [Header("Character Gravity")]
    [SerializeField] protected bool isGrounded;
    [SerializeField] protected float characterGravity = -9.81f;
    [SerializeField] protected float characterGravityMultiplier = 3.0f;
    [SerializeField] protected float fallVelocity;

    public event Action<float> OnCharacterDamaged = delegate { };

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

    public virtual void MoveCharacter(Vector2 rawDirection)
    {
        nextPosition = new Vector3(rawDirection.x, 0f, rawDirection.y);
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

    public virtual void ReceiveDamage(float damageAmount)
    {
        characterActualHealth -= damageAmount;
        OnCharacterDamaged?.Invoke(damageAmount);

        if (characterActualHealth <= 0)
            Death();
    }

    protected virtual void Death()
    {
        Debug.Log($"{name}: Has died!");
        Destroy(gameObject);
    }
}
