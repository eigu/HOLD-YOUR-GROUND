using UnityEngine;

public abstract class PowerUpInfoSO : ScriptableObject
{
    public GameEventSO OnPowerUp;
    public float Duration;
    public int Cost;

    public abstract void Use(Transform playerTransform);
}
