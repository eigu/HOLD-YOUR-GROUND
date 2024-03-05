using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float fireRate;
    private float timeToShoot;

    private void Start()
    {
        timeToShoot = fireRate;
    }

    private void Update()
    {
        timeToShoot -= Time.deltaTime;

        Shoot();
    }

    private void Shoot()
    {
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Infinity, enemyLayerMask)) return;

        Enemy enemy = hit.collider.GetComponent<Enemy>();

        if (!InputManager.Instance.CheckIfPlayerIsShooting()) return;

        if (timeToShoot > 0) return;

        if (enemy != null) enemy.DamageEntity(1); 

        timeToShoot = fireRate;
    }
}
