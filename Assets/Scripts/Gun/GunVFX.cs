using System;
using System.Collections;
using UnityEngine;

public class GunVFX : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GunLogic _gunLogic;

    private void Awake()
    {
        _lineRenderer = FindAnyObjectByType<LineRenderer>();

        if (_lineRenderer == null)
            Debug.Log($"{name}: The _lineRenderer is null!");
    }

    private void OnEnable()
    {
        _gunLogic.OnGunShoot += RenderVFX;
    }

    private void OnDisable()
    {
        _gunLogic.OnGunShoot -= RenderVFX;
    }

    private void RenderVFX(Vector3 bulletOrigin, Vector3 bulletDestination)
    {
        StartCoroutine(RenderBullet(bulletOrigin, bulletDestination));
    }

    private IEnumerator RenderBullet(Vector3 bulletOrigin, Vector3 bulletDestination)
    {
        _lineRenderer.positionCount = 2;
        _lineRenderer.SetPosition(0, bulletOrigin);
        _lineRenderer.SetPosition(1, bulletDestination);

        yield return new WaitForSeconds(0.2f);

        _lineRenderer.positionCount = 1;
    }
}
