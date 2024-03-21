using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  public static InputManager Instance { get; private set; }

  private PlayerInput m_playerInput;

  private void Awake()
  {
    if (Instance == null) Instance = this;
    else if (Instance != this) Destroy(this);
  }

  private void Start()
  {

    m_playerInput = GetComponent<PlayerInput>();
  }

  public Vector2 GetMouseDelta()
  {
    InputAction playerLook = m_playerInput.actions.FindAction("Look");

    return playerLook.ReadValue<Vector2>();
  }

  public bool CheckIfPlayerIsShooting()
  {
    InputAction playerShoot = m_playerInput.actions.FindAction("Shoot");

    return playerShoot.IsPressed();
  }

  public bool CheckIfPlayerIsUsingPowerUp()
  {
    InputAction playerPowerUp = m_playerInput.actions.FindAction("PowerUp");

    return playerPowerUp.triggered;
  }

  public Vector3 GetCrosshairPoint()
  {
    Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

    return ray.GetPoint(100f);
  }
}
