using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;

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
        if (!InputManager.Instance.CheckIfPlayerIsShooting()) return;
        if (timeToShoot > 0) return;

        Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

        timeToShoot = fireRate;
    }
}
