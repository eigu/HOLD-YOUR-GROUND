using System.Collections;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private IntReference _killCount;

    [SerializeField] private PowerUpInfo _currentPowerUpInfo;

    private bool m_isPoweringUp = false;

    private void Update()
    {
        PowerUp();
    }

    private void PowerUp()
    {
        if (!InputManager.Instance.CheckIfPlayerIsUsingPowerUp()) return;
        if (m_isPoweringUp) return;
        if (_killCount.Variable.Value < _currentPowerUpInfo.Cost) return;

        _currentPowerUpInfo.OnPowerUp.TriggerEvent();
    }

    public void ActivatePowerUp()
    {
        if (_currentPowerUpInfo == null) return;

        StartCoroutine(UsingPowerUp(_currentPowerUpInfo.Duration));
    }

    private IEnumerator UsingPowerUp(float duration)
    {
        m_isPoweringUp = true;
        _killCount.Variable.Value -= _currentPowerUpInfo.Cost;

        float time = 0;

        while (time < duration)
        {
            _currentPowerUpInfo.Use(transform);
            time += Time.deltaTime;
            yield return null;
        }

        m_isPoweringUp = false;
    }
}
