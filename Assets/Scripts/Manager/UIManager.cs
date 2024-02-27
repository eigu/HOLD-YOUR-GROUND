using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
  public static UIManager Instance { get; private set; }

  private Player playerEntity;

  [SerializeField]
  private TextMeshProUGUI _playerHPText;
  [SerializeField]
  public Image            _playerHPBar;
  [SerializeField]
  private Image[]         _killCount;

  private float lerpSpeed;

  [SerializeField]
  private GameObject gameOverScreen;

  private void Awake()
  {
    if (Instance == null) Instance = this;
    else if (Instance != this) Destroy(this);
  }

  private void Start()
  {
    playerEntity = FindObjectOfType<Player>();

    Time.timeScale = 1;

    lerpSpeed = 3f * Time.deltaTime;

    HideCursor();
  }

  private void Update()
  {
    UpdatePlayerUI(playerEntity);

    ShowPlayerDeathUI();
  }

  private void UpdatePlayerUI(Player playerScript)
  {
    _playerHPText.text = "HP: " + playerScript.CurrentHP;

    HealthBarFiller();
    ColorChanger();
    KillCounterFiller();
  }

  private void ShowPlayerDeathUI()
  {
    if (!playerEntity)
    {
      Time.timeScale = 0;
      _playerHPText.gameObject.SetActive(false);
      gameOverScreen.SetActive(true);
      ShowCursor();
    }
  }

  void HealthBarFiller()
  {
    _playerHPBar.fillAmount = Mathf.Lerp(_playerHPBar.fillAmount, (playerEntity.CurrentHP / playerEntity.MaxHP), lerpSpeed);
  }

  void ColorChanger()
  {
    Color healthColor = Color.Lerp(Color.red, Color.green, (playerEntity.CurrentHP / playerEntity.MaxHP));
    _playerHPBar.color = healthColor;
    _playerHPText.color = healthColor;
  }

  private void KillCounterFiller()
  {
    for (int i = 0; i < _killCount.Length; i++)
    {
      _killCount[i].enabled = !DisplayKillCounter(playerEntity.KillCount, i);
    }
  }

  private bool DisplayKillCounter(float killCount, int pointNumber)
  {
    return ((pointNumber) >= killCount);
  }

  private void HideCursor()
  {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  private void ShowCursor()
  {
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
  }
}
