using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpList
{
  AutoAim
}

[System.Serializable]
public class PowerUpInfo
{
  public PowerUpList type;
  public GameObject hud;
  public float duration;
  public int cost;
}

public class PlayerPowerUp : MonoBehaviour
{
  private Player player;

  [SerializeField] private PowerUpList currentPowerUp;
  [SerializeField] private List<PowerUpInfo> powerUpInfoList;
  private PowerUpInfo currentPowerUpInfo;
  private float powerUpTimer;
  private bool isUsingPowerUp = false;

  [SerializeField] private float autoAimRadius;
  public Transform lockedEnemy;


  private void Start()
  {
    currentPowerUpInfo = powerUpInfoList.Find(info => info.type == currentPowerUp);

    player = FindObjectOfType<Player>();
  }

  private void Update()
  {
    powerUpTimer -= Time.deltaTime;

    PowerUp();
  }

  private void PowerUp()
  {
    if (!InputManager.Instance.CheckIfPlayerIsUsingPowerUp()) return;
    if (isUsingPowerUp) return;
    if (player.CurrentSP < currentPowerUpInfo.cost) return;

    player.CurrentSP -= currentPowerUpInfo.cost;

    ActivatePowerUp(currentPowerUp.ToString());
  }

  private void ActivatePowerUp(string methodName)
  {
    isUsingPowerUp = true;
    currentPowerUpInfo.hud.SetActive(true);

    powerUpTimer = currentPowerUpInfo.duration;
    StartCoroutine(UsingPowerUp(methodName, currentPowerUpInfo.duration));
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
    isUsingPowerUp = false;
    currentPowerUpInfo.hud.SetActive(false);
  }

  private void AutoAim()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, autoAimRadius);
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
      lockedEnemy = closestEnemy;
    }
    else
    {
      lockedEnemy = null;
    }
  }
}
