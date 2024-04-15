using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private GunDatapack gunData;
    [SerializeField] private float gunMaxDistance = 30f;

    public GunDatapack GunData { get => gunData; set => gunData = value; }
    public float GunMaxDistance { get => gunMaxDistance; set => gunMaxDistance = value; }

    public void Shoot()
    {
        Ray bullet = new(transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(bullet, out RaycastHit hitInfo, GunMaxDistance))
        {
            Debug.Log($"{name}: I shooted to {hitInfo.transform.name}");
            IHitteable hitObj = hitInfo.transform.GetComponent<IHitteable>();

            hitObj?.ReceiveDamage(GunData.damagePerBullet);
        }
    }

    public void Reload()
    {
        Debug.Log($"{name} is reloading...");
    }
}
