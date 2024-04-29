using System;
using System.Collections;
using UnityEngine;

public class GunVFX : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GunLogic gunLogic;

    private void OnEnable()
    {
        gunLogic.OnGunShoot += RenderVFX;
    }

    private void OnDisable()
    {
        gunLogic.OnGunShoot -= RenderVFX;
    }

    private void RenderVFX(Vector3 bulletOrigin, Vector3 bulletDestination)
    {
        StartCoroutine(RenderBullet(bulletOrigin, bulletDestination));
    }

    private IEnumerator RenderBullet(Vector3 bulletOrigin, Vector3 bulletDestination)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, bulletOrigin);
        lineRenderer.SetPosition(1, bulletDestination);

        yield return new WaitForSeconds(0.2f);

        lineRenderer.positionCount = 1;
    }
}
