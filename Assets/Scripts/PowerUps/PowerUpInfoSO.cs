using UnityEngine;

public abstract class PowerUpInfo : MonoBehaviour
{
    public GameEventSO OnPowerUp;
    public float Duration;
    public int Cost;

    public abstract void Use(Transform playerTransform);
}
