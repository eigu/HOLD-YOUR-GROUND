using UnityEngine;

public class Bullet : Entity
{
    private InputManager inputManager;

    [SerializeField] private float destroyDelay;

    private Vector3 target;

    private void Awake()
    {
    }

    protected override void Start()
    {
        base.Start();

        inputManager = InputManager.Instance;

        target = inputManager.GetCrosshairPoint();

        Destroy(gameObject, destroyDelay);
    }

    protected override void Update()
    {
        base.Update();

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.Translate((target - transform.position).normalized * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground")) DamageEntity(1);
    }
}
