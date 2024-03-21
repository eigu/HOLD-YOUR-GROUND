using UnityEngine;

public abstract class PowerUpInfo : MonoBehaviour
{
    public GameEventSO OnPowerUpStart;
    public GameEventSO OnPowerUp;
    public float Duration;
    public int Cost;

    public TransformAnimationSO OnGunDrawIn;
    public TransformAnimationSO OnGunDrawOut;

    public abstract void Use(Transform playerTransform);
    public abstract void End();
}
