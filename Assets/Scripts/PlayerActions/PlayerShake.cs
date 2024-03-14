using Cinemachine;
using UnityEngine;

public class PlayerShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource _screenShake;

    public void ScreenShake(Vector3 direction)
    {
        _screenShake.GenerateImpulseWithVelocity(direction);
    }
}
