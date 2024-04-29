using UnityEngine;
using System;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    [Header("Gun Settings")]
    [SerializeField] private GunDatapack _gunData;
    [SerializeField] private float _gunMaxDistance = 100f;
    [SerializeField] private Transform _gunCanonTransform;

    public GunDatapack GunData { get => _gunData; set => _gunData = value; }

    public event Action<Vector3, Vector3> OnGunShoot = delegate { };

    public void Shoot()
    {
        Ray bullet = new(mainCamera.transform.position, mainCamera.transform.forward);

        if (Physics.Raycast(bullet, out RaycastHit hitInfo, _gunMaxDistance))
        {
            IHitteable hittedObject = hitInfo.collider.GetComponent<IHitteable>();
            hittedObject?.ReceiveDamage(_gunData.damagePerBullet);

            OnGunShoot?.Invoke(_gunCanonTransform.position, hitInfo.point);
            Debug.Log($"Gun shooted at: {hitInfo.transform.name}");
        }

    }

    public void Reload()
    {
        Debug.Log($"{name} is reloading...");
    }
}
