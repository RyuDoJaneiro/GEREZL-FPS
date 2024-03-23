using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLogic : MonoBehaviour
{
    public GunDatapack GunData {  get; set; }

    public void Shoot()
    {
        Debug.Log($"{GunData.gunName} shooted by {name}!");
    }

    public void Reload()
    {
        Debug.Log($"{name} is reloading...");
    }
}
