using UnityEngine;
using System;
using System.Collections;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [Header("Gun Settings")]
    [SerializeField] private GunDatapack _gunData;
    [SerializeField] private float _gunMaxDistance = 100f;
    [SerializeField] private Transform _gunCanonTransform;
    [SerializeField] private int _gunAmmo;

    public GunDatapack GunData { get => _gunData; set => _gunData = value; }

    public event Action<Vector3, Vector3> OnGunShoot = delegate { };
    public event Action<int> OnAmmoUpdate = delegate { };

    private void Awake()
    {
        _mainCamera = Camera.main;
        _gunAmmo = _gunData.maxAmmo;
    }

    public void Shoot()
    {
        if (_gunAmmo <= 0)
            return;

        Ray bullet = new(_mainCamera.transform.position, _mainCamera.transform.forward);

        if (Physics.Raycast(bullet, out RaycastHit hitInfo, _gunMaxDistance))
        {
            IHitteable hittedObject = hitInfo.collider.GetComponent<IHitteable>();
            hittedObject?.ReceiveDamage(_gunData.damagePerBullet);

            _gunAmmo--;
            OnAmmoUpdate?.Invoke(_gunAmmo);
            OnGunShoot?.Invoke(_gunCanonTransform.position, hitInfo.point);
            Debug.Log($"Gun shooted at: {hitInfo.transform.name}");

            if (_gunAmmo <= 0)
                Reload();
        }

    }

    public void Reload()
    {
        Debug.Log($"{name} is reloading...");
        StartCoroutine(ReloadSequence());
    }

    private IEnumerator ReloadSequence()
    {
        yield return new WaitForSeconds(2f);

        _gunAmmo = _gunData.maxAmmo;
    }
}
