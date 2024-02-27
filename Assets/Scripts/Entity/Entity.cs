using UnityEngine;

public class Entity : MonoBehaviour
{
  public int MaxHP;
  public int CurrentHP
  {
    get { return MaxHP; }
    set { MaxHP = value; }
  }

  [SerializeField]
  protected private float _speed;

  protected virtual void Start()
  {

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
