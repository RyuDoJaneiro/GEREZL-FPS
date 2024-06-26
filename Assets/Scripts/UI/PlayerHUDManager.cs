using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUDManager : MonoBehaviour
{
    [Header("Script references")]
    [SerializeField] private GunLogic _gunLogic;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Image _crossHair;
    [SerializeField] private TextMeshProUGUI _ammoCount;
    private CameraController _cameraController;

    private void OnEnable()
    {
        _cameraController = Camera.main.GetComponent<CameraController>();
        if (!_cameraController)
        {
            Debug.LogError($"{name}: The cameraController reference is null!\n" +
                $"Disabling component to avoid errors!");
            enabled = false;
            return;
        }

        gameObject.GetComponent<CharacterManager>().OnCharacterDamaged += HandleHealthBarUpdate;
        _cameraController.OnEnemyDetection += ChangeCrossHairColor;
        _gunLogic.OnAmmoUpdate += HandleAmmoUpdate;
    }

    private void OnDisable()
    {
        _cameraController.OnEnemyDetection -= ChangeCrossHairColor;
        gameObject.GetComponent<CharacterManager>().OnCharacterDamaged -= HandleHealthBarUpdate;
        _gunLogic.OnAmmoUpdate -= HandleAmmoUpdate;
    }

    private void ChangeCrossHairColor(bool enemyDetectionStatus)
    {
        if (enemyDetectionStatus)
            _crossHair.color = Color.red;
        else
            _crossHair.color= Color.white;
    }

    private void HandleAmmoUpdate(int actualAmmo)
    {
        _ammoCount.text = actualAmmo.ToString(); 
    }

    public void HandleHealthBarUpdate(float amount)
    {
        _healthBar.value -= amount;

        if (_healthBar.value < 0)
            _healthBar.value = 0;
    }
}
