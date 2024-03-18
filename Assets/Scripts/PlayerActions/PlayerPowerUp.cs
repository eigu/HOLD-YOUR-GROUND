using System.Collections;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private IntReference _killCount;

    [SerializeField] private PowerUpInfo _currentPowerUpInfo;

    private bool m_isPoweringUp = false;

    private void Start()
    {
        _killCount.Variable.Value = 0;
    }

    private void Update()
    {
        PowerUp();
    }

    public void AddSP(int value)
    {
        if (_killCount.Variable.Value < _currentPowerUpInfo.Cost) _killCount.Variable.Value += value;
    }

    private void PowerUp()
    {
        if (!InputManager.Instance.CheckIfPlayerIsUsingPowerUp()) return;
        if (m_isPoweringUp) return;
        if (_killCount.Variable.Value < _currentPowerUpInfo.Cost) return;

        _currentPowerUpInfo.OnPowerUpStart.TriggerEvent();
    }

    public void PowerUpStart()
    {
        if (_currentPowerUpInfo == null) return;

        StartCoroutine(PowerUpStartCoroutine(_currentPowerUpInfo.OnPowerUpAnimation.Duration));
    }

    private IEnumerator PowerUpStartCoroutine(float duration)
    {
        m_isPoweringUp = true;
        _killCount.Variable.Value -= _currentPowerUpInfo.Cost;

        float time = 0;

        while (time < duration - (duration * .5f))
        {
            time += Time.deltaTime;
            yield return null;
        }

        _currentPowerUpInfo.OnPowerUp.TriggerEvent();
    }

    public void ActivatePowerUp()
    {
        StartCoroutine(PowerUpCoroutine(_currentPowerUpInfo.Duration));
    }

    private IEnumerator PowerUpCoroutine(float duration)
    {
        float time = 0;

        while (time < duration)
        {
            _currentPowerUpInfo.Use(transform);
            time += Time.deltaTime;
            yield return null;
        }

        _currentPowerUpInfo.End();
        m_isPoweringUp = false;
    }
}
