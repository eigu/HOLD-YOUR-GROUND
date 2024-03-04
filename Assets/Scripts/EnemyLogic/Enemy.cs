using UnityEngine;

public class Enemy : Entity
{
  protected override void Start()
  {
        base.Start();

        Destroy(gameObject, 60f);
  }

  protected override void Update()
  {
    base.Update();;

    if (CurrentHP <= 0) FindObjectOfType<Player>().AddSP(1);
  }
}
