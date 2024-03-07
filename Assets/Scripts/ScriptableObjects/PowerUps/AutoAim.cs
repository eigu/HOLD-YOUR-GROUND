using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/AutoAim")]
public class AutoAim : PowerUpInfoSO
{
    public Transform LockedEnemy;

    public override void Use(Transform playerTransform)
    {
        Camera mainCamera = Camera.main;

        Vector3 extents = new Vector3(Screen.width * .15f, Screen.height * .21f, 0f);

        LayerMask enemyLayerMask = LayerMask.GetMask("EnemyLayer");

        RaycastHit[] hits = Physics.BoxCastAll(mainCamera.transform.position, extents, mainCamera.transform.forward, mainCamera.transform.rotation, Mathf.Infinity, enemyLayerMask);

        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(playerTransform.position, hit.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }

        LockedEnemy = closestEnemy;
    }
}
