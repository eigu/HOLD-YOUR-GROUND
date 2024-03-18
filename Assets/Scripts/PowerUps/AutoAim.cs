using System.Collections;
using UnityEngine;

public class AutoAim : PowerUpInfo
{
    public Transform LockedEnemy;

    public override void Use(Transform playerTransform)
    {


        if (LockedEnemy == null)
        {
            LockedEnemy = FindNewEnemy(playerTransform);
        }
    }

    public override void End()
    {
        LockedEnemy = null;
    }

    private Transform FindNewEnemy(Transform playerTransform)
    {
        Camera mainCamera = Camera.main;

        Vector3 extents = new(Screen.width * .5f, Screen.height * .5f, 0f);

        RaycastHit[] hits = Physics.BoxCastAll(mainCamera.transform.position, extents, mainCamera.transform.forward, mainCamera.transform.rotation, Mathf.Infinity);

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("EnemyHead"))
            {
                Vector3 direction = hit.point - playerTransform.position;
                float distance = direction.magnitude;
                direction /= distance;

                if (IsTargetVisible(playerTransform, direction, distance))
                {
                    if (distance < closestDistance)
                    {
                        closestEnemy = hit.collider.transform;
                        closestDistance = distance;
                    }
                }
            }
        }

        return closestEnemy;
    }

    private bool IsTargetVisible(Transform playerTransform, Vector3 direction, float distance)
    {
        RaycastHit hit;

        if (Physics.Raycast(playerTransform.position, direction, out hit, distance))
        {
            if (hit.collider.CompareTag("EnemyHead"))
            {
                return true;
            }
        }

        return false;
    }
    }
