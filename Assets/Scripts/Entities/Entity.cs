using UnityEngine;

public class Entity : MonoBehaviour
{
    [field: SerializeField] public int MaxHP { get; set; }
    [field: SerializeField] public int CurrentHP { get; set; }
    [field: SerializeField] public float MaxSpeed { get; set; }
    [field: SerializeField] public float CurrentSpeed { get; set; }

    protected virtual void Start()
    {
        CurrentHP = MaxHP;
    }

    protected virtual void Update()
    {
        if (CurrentHP <= 0) Destroy(gameObject);
    }

    public virtual void DamageEntity(int damage)
    {
        if (CurrentHP > 0) CurrentHP -= damage;

        CurrentHP = Mathf.Clamp(CurrentHP, 0, MaxHP);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) DamageEntity(100);

        if (other.tag == gameObject.tag
            || other.CompareTag("Ground")) return;

        DamageEntity(1);
    }
}
