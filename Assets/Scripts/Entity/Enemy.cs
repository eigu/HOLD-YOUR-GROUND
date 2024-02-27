using UnityEngine;

public class Enemy : Entity
{
  private Transform player;

  protected override void Start()
  {
    base.Start();

    player = FindObjectOfType<Player>().transform;
  }

  protected override void Update()
  {
    base.Update();

    MoveTowardsPlayer();

    if (CurrentHP <= 0) FindObjectOfType<Player>().AddSP(1);
  }

  private void MoveTowardsPlayer()
  {
    if (player != null)
    {
      transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * _speed);
    }
  }
}
