using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
  [SerializeField]
  protected private int maxHP;
  public int CurrentHP
  {
    get { return maxHP; }
    set { maxHP = value; }
  }

  [SerializeField]
  protected float speed;


  protected virtual void Start()
  {
    CurrentHP = maxHP;
  }

  protected virtual void Update()
  {
    if (CurrentHP <= 0) Destroy(gameObject);
  }

  public virtual void DamageEntity(int damage)
  {
    if (CurrentHP > 0)
    {
      CurrentHP -= damage;
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.tag == gameObject.tag
      || other.CompareTag("Ground")) return;
    
    DamageEntity(1);
  }
}
