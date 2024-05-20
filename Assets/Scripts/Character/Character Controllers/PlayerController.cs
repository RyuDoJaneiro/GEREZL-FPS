using Inputs;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(GunLogic))]
public class PlayerController : CharacterManager
{
    [SerializeField] private Vector2 _playerInput;
    
    [SerializeField] private LayerMask _deathZoneLayer;
    [SerializeField] private LayerMask _winZoneLayer;
    [SerializeField] private GunLogic _gunLogic;
    private InputReader _inputReader;
    private Camera _mainCamera;
    private SceneryManager _sceneryManager;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _sceneryManager = GameObject.Find("Game Manager").GetComponent<SceneryManager>();
        _inputReader = GameObject.Find("Game Manager").GetComponent<InputReader>();
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
        _inputReader.OnPauseInput += PauseGame;
    }

    private void OnDisable()
    {
        _inputReader.OnMovementInput -= MoveCharacter;
        _inputReader.OnJumpInput -= Jump;
        _inputReader.OnShootInput -= _gunLogic.Shoot;
        _inputReader.OnReloadInput -= _gunLogic.Reload;
        _inputReader.OnPauseInput -= PauseGame;
    }

    private void FixedUpdate()
    {
        _characterController.Move(characterSpeed * Time.deltaTime * nextPosition);
        ApplyGravity();
        RotatePlayer();

        Collider[] deathColliders = Physics.OverlapSphere(transform.position, 10f, _deathZoneLayer);
        Collider[] winColliders = Physics.OverlapSphere(transform.position, 5f, _winZoneLayer);

        foreach (var collider in deathColliders)
            Death();

        foreach (var collider in winColliders)
        {
            gameObject.transform.Find("Player UI").gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            _sceneryManager.Victory();
        }
    }

    public override void MoveCharacter(Vector2 rawDirection)
    {
        Vector3 relativeDirection = new(rawDirection.x, 0.0f, rawDirection.y);
        relativeDirection = relativeDirection.x * transform.right + relativeDirection.z * transform.forward;

        nextPosition = relativeDirection.normalized;
    }

    private void RotatePlayer()
    {
        if (!_mainCamera)
        {
            Debug.LogError($"{name}: Main Camera is null." +
                "\nDisabling to avoid errors!");
            enabled = false;
            return;
        }

        transform.rotation = Quaternion.Euler(0f, _mainCamera.transform.eulerAngles.y, 0f);
    }

    private void PauseGame()
    {
        GameObject pauseScreen = GameObject.Find("Game Manager/Main Canvas/Options View");

        if (pauseScreen.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            pauseScreen.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            pauseScreen.SetActive(false);
        }
    }

    protected override void Death()
    {
        gameObject.transform.Find("Player UI").gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        _sceneryManager.Lose();
        base.Death();
    }
}
