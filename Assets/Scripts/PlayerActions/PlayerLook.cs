using UnityEngine;

public class PlayerLook : MonoBehaviour
{
  private PlayerPowerUp playerPowerUp;

  [SerializeField] private Vector3 playerOrientation;
  [SerializeField] private Transform cameraHolder;
  [SerializeField] private Transform playerHand;

  [SerializeField, Range(0, 2)] private float sensitivity;
  [SerializeField, Range(0, 100)] private float smoothing;

  private Vector2 smoothedDelta = Vector2.zero;

  private void Start()
  {
    playerPowerUp = GetComponent<PlayerPowerUp>();
  }

  private void Update()
  {
    Look();
  }

  private void Look()
  {
    Vector3 mouseDelta = InputManager.Instance.GetMouseDelta();
    mouseDelta *= sensitivity;
    smoothedDelta = Vector2.Lerp(smoothedDelta, mouseDelta, 1f / smoothing);

    if (playerPowerUp.lockedEnemy == null)
    {
      playerOrientation.x += mouseDelta.x;
      playerOrientation.y -= mouseDelta.y;
    }
    
    if (playerPowerUp.lockedEnemy != null)
    {
      var lockedEnemyPosition = playerPowerUp.lockedEnemy.transform.position;
      Vector3 lookDirection = lockedEnemyPosition - transform.position;
      Quaternion horizontalRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));

      float angle = Quaternion.Angle(transform.rotation, horizontalRotation);

      playerOrientation.x += angle;
      playerOrientation.y = -lockedEnemyPosition.y;
    }
    
    playerOrientation.x = Mathf.Repeat(playerOrientation.x, 360f);
    playerOrientation.y = Mathf.Clamp(playerOrientation.y, -30f, 30f);

    cameraHolder.rotation = Quaternion.Euler(new Vector3(playerOrientation.y, transform.rotation.eulerAngles.y, 0));
    transform.rotation = Quaternion.Euler(new Vector3(0, playerOrientation.x, 0));

    HandAim();
  }

  private void HandAim()
  {
    if (playerHand != null)
    {
      playerHand.LookAt(InputManager.Instance.GetCrosshairPoint());
    }
  }
}
