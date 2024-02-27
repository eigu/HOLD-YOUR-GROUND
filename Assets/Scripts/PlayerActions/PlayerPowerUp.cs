using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpList
{
  AutoAim
}

[System.Serializable]
public class PowerUpInfo
{
  public PowerUpList        Type;
  public GameObject         HUD;
  public GameObject         Button;
  public float              Duration;
  public int                Cost;
}

public class PlayerPowerUp : MonoBehaviour
{
  private Player            m_player;

  [SerializeField]
  private PowerUpList       _currentPowerUp;
  [SerializeField]
  private List<PowerUpInfo> _powerUpInfoList;

  private PowerUpInfo       m_currentPowerUpInfo;
  private float             m_powerUpTimer;
  private bool              m_isUsingPowerUp = false;

  [SerializeField]
  private float             _autoAimRadius;
  
  public Transform          LockedEnemy;

  private void Start()
  {
    m_currentPowerUpInfo = _powerUpInfoList.Find(info => info.Type == _currentPowerUp);

    m_player = FindObjectOfType<Player>();
  }

  private void Update()
  {
    m_powerUpTimer -= Time.deltaTime;

    ShowButton();
    
    PowerUp();
  }

  private void ShowButton()
  {
    if (m_player.KillCount == m_currentPowerUpInfo.Cost)
    {
      m_currentPowerUpInfo.Button.SetActive(true);
    }
  }

  private void PowerUp()
  {
    if (!InputManager.Instance.CheckIfPlayerIsUsingPowerUp()) return;
    if (m_isUsingPowerUp) return;
    if (m_player.KillCount < m_currentPowerUpInfo.Cost) return;

    m_player.KillCount -= m_currentPowerUpInfo.Cost;

    ActivatePowerUp(_currentPowerUp.ToString());
  }

  private void ActivatePowerUp(string methodName)
  {
    m_isUsingPowerUp = true;
    m_currentPowerUpInfo.HUD.SetActive(true);
    m_currentPowerUpInfo.Button.SetActive(false);

    m_powerUpTimer = m_currentPowerUpInfo.Duration;
    StartCoroutine(UsingPowerUp(methodName, m_currentPowerUpInfo.Duration));
  }

  private IEnumerator UsingPowerUp(string methodName, float duration)
  {
    StartCoroutine(DisablePowerUp(duration));

    float timer = 0f;

    while (timer < duration)
    {
      Invoke(methodName, 0f);
      timer += Time.deltaTime;
      yield return null;
    }
  }
   
  private IEnumerator DisablePowerUp(float duration)
  {
    yield return new WaitForSeconds(duration);
    m_isUsingPowerUp = false;
    m_currentPowerUpInfo.HUD.SetActive(false);
  }

  private void AutoAim()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, _autoAimRadius);
    float closestDistance = Mathf.Infinity;
    Transform closestEnemy = null;

    foreach (Collider collider in colliders)
    {
      if (collider.CompareTag("Enemy"))
      {
        float distance = Vector3.Distance(transform.position, collider.transform.position);
        if (distance < closestDistance)
        {
          closestDistance = distance;
          closestEnemy = collider.transform;
        }
      }
    }

    if (closestEnemy != null)
    {
      LockedEnemy = closestEnemy;
    }
    else
    {
      LockedEnemy = null;
    }
  }
}
