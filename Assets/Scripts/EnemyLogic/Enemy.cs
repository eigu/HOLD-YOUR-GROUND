using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private GameEventSO _onEnemyDeath;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();;

        if (CurrentHP <= 0) _onEnemyDeath?.TriggerEvent();
    }
}
