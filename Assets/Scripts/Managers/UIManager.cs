using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Player playerEntity;
    [SerializeField] private IntVariableSO KillCount;
    [SerializeField] private PowerUpInfoSO PowerUpInfo;

    [SerializeField] private TextMeshProUGUI _playerHPText;
    [SerializeField] public Image _playerHPBar;

    [SerializeField] private Image[] _killCountBar;
    [SerializeField] private GameObject _powerUpButton;
    [SerializeField] private GameObject _powerUpHUD;

    private float lerpSpeed = 3f;

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

        HideCursor();
    }

    private void Update()
    {
        ShowPlayerDeathUI();

        UpdatePlayerUI(playerEntity);
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
        if (playerEntity.CurrentHP <= 0)
        {
            Time.timeScale = 0;
            _playerHPText.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            ShowCursor();
        }
    }

    private void HealthBarFiller()
    {
        float healthPercentage = (float)playerEntity.CurrentHP / (float)playerEntity.MaxHP;

        _playerHPBar.fillAmount = Mathf.Lerp(_playerHPBar.fillAmount, healthPercentage, lerpSpeed * Time.deltaTime);
    }

    private void ColorChanger()
    {
        float healthPercentage = (float)playerEntity.CurrentHP / (float)playerEntity.MaxHP;

        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

        _playerHPBar.color = healthColor;
    }

    private void KillCounterFiller()
    {
        for (int i = 0; i < _killCountBar.Length; i++)
        {
            _killCountBar[i].enabled = !DisplayKillCounter(KillCount.Value, i);
        }

        if (KillCount.Value >= PowerUpInfo.Cost) _powerUpButton.SetActive(true);
        if (KillCount.Value < PowerUpInfo.Cost) _powerUpButton.SetActive(false);
    }

    public void PowerUpUI()
    {
        _powerUpHUD.SetActive(true);
    }

    private bool DisplayKillCounter(float killCount, int pointNumber)
    {
        return (pointNumber >= killCount);
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
