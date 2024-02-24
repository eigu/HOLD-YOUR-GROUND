using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected private int maxHP;
    public int currentHP;

    [SerializeField] protected float speed;

    private bool hasTakenDamage = false;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }

    protected virtual void Update()
    {
        if (currentHP <= 0) Destroy(gameObject);
    }

    protected virtual void DamageEntity(int damage)
    {
        if (!hasTakenDamage && currentHP > 0)
        {
            hasTakenDamage = true;
            currentHP -= damage;
        }

        StartCoroutine(ResetDamageFlag());
    }

    private IEnumerator ResetDamageFlag()
    {
        yield return new WaitForSeconds(0.3f);
        hasTakenDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == gameObject.tag
            || other.CompareTag("Ground")) return;
        
        DamageEntity(1);
    }
}
