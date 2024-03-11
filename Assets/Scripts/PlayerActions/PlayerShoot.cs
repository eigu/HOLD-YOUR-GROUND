using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] int _bodyDamage;
    [SerializeField] int _headDamage;

    [SerializeField] float _fireRate;
    private float m_timeToShoot;

    [SerializeField] private Transform _muzzle;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private ParticleSystem _impact;
    [SerializeField] private TrailRenderer _tracer;
    [SerializeField] private float _tracerSpeed;

    [SerializeField] private GameEventSO _onPlayerShoot;
    [SerializeField] private GameEventSO _onHeadTargetted;
    [SerializeField] private GameEventSO _onBodyTargetted;
    [SerializeField] private GameEventSO _onNothingTargetted;

    private void Start()
    {
        m_timeToShoot = _fireRate;
    }

    private void Update()
    {
        m_timeToShoot -= Time.deltaTime;

        Aim();
    }

    private void Aim()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) return;

        if (!hit.collider.CompareTag("EnemyHead") ||
            !hit.collider.CompareTag("EnemyBody"))
        {
            _onNothingTargetted.TriggerEvent();
        }

        if (hit.collider.CompareTag("EnemyHead"))
        {
            _onHeadTargetted.TriggerEvent();
        }

        if (hit.collider.CompareTag("EnemyBody"))
        {
            _onBodyTargetted.TriggerEvent();
        }

        Shoot(hit);
    }

    private void Shoot(RaycastHit hit)
    {
        if (!InputManager.Instance.CheckIfPlayerIsShooting()) return;

        if (m_timeToShoot > 0) return;

        _onPlayerShoot?.TriggerEvent();

        m_timeToShoot = _fireRate;

        _muzzleFlash.Play();

        TrailRenderer tracer = Instantiate(_tracer, _muzzle.position, Quaternion.identity);

        StartCoroutine(SpawnTracer(tracer, hit));
    }

    private IEnumerator SpawnTracer(TrailRenderer tracer, RaycastHit hit)
    {
        float distance = Vector3.Distance(tracer.transform.position, hit.point);
        float duration = distance / _tracerSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            tracer.transform.position = Vector3.MoveTowards(tracer.transform.position, hit.point, _tracerSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        tracer.transform.position = hit.point;

        Instantiate(_impact, hit.point, Quaternion.LookRotation(hit.normal));

        DamageEnemy(hit.collider);

        Destroy(tracer.gameObject, tracer.time);
    }

    private void DamageEnemy(Collider collider)
    {
        Enemy enemy = collider.GetComponentInParent<Enemy>();

        if (enemy == null) return;

        if (collider.CompareTag("EnemyBody")) enemy.DamageEntity(_bodyDamage);

        if (collider.CompareTag("EnemyHead")) enemy.DamageEntity(_headDamage);
    }
}
