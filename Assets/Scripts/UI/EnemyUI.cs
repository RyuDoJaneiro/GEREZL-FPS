using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [Header("Script References")]
    private Camera _mainCamera;
    [SerializeField] private Slider _enemyHealthBar;

    private void OnEnable()
    {
        gameObject.GetComponent<CharacterManager>().OnCharacterDamaged += HandleHealthBarUpdate;
    }

    private void OnDisable()
    {
        gameObject.GetComponent<CharacterManager>().OnCharacterDamaged -= HandleHealthBarUpdate;
    }

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        _enemyHealthBar.gameObject.transform.rotation = Quaternion.LookRotation(_mainCamera.transform.position - transform.position, Vector3.up);
    }

    public void HandleHealthBarUpdate(float amount)
    {
        if (!_enemyHealthBar.gameObject.activeSelf)
            _enemyHealthBar.gameObject.SetActive(true);

        _enemyHealthBar.value -= amount;

        if (_enemyHealthBar.value < 0)
            _enemyHealthBar.value = 0;
    }
}
