using UnityEngine;

[CreateAssetMenu(menuName = "PowerUps/AutoAim")]
public class AutoAim : PowerUpInfoSO
{
    [SerializeField] private float m_autoAimRadius;
    public Transform LockedEnemy;

    public override void Use(Transform playerTransform)
    {
        Collider[] colliders = Physics.OverlapSphere(playerTransform.transform.position, m_autoAimRadius);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        Debug.Log(colliders);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(playerTransform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            LockedEnemy = closestEnemy;
        }
        else
        {
            LockedEnemy = null;
        }
    }
}
