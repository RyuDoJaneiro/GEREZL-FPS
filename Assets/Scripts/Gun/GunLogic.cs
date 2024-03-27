using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    [SerializeField] private GunDatapack gunData;
    [SerializeField] private float gunMaxDistance = 30f;

    public GunDatapack GunData { get => gunData; set => gunData = value; }

    public void Shoot()
    {
        Ray bullet = new Ray(transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(bullet, out RaycastHit hitInfo, gunMaxDistance))
        {
            Debug.Log($"{name}: I shooted to {hitInfo.transform.name}");
        }

        Debug.DrawRay(bullet.origin, bullet.direction * gunMaxDistance, Color.green);
    }

    public void Reload()
    {
        Debug.Log($"{name} is reloading...");
    }
}
