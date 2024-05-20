using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunDatapack : ScriptableObject
{
    [Header("Gun Info")]
    public string gunName = "Gun";

    [Header("Gun Stats")]
    public int bulletsPerShoot = 1;
    public float damagePerBullet = 1f;
    public float timeBetweenShoots = 1f;
    public int maxAmmo = 1;
}
