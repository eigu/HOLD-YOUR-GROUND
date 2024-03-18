using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Player m_playerEntity;
    private EnemySpawner m_enemySpawner;
    private PowerUpInfo m_currentPowerUpInfo;

    [SerializeField] private IntVariableSO _killCount;

    [SerializeField] private TextMeshProUGUI _playerHPText;
    [SerializeField] public Image _playerHPBar;

    [SerializeField] private Image[] _killCountBar;
    [SerializeField] private GameObject _powerUpButton;
    [SerializeField] private GameObject _powerUpHUD;
    [SerializeField] private Image _powerUpHUDTimer;

    [SerializeField] private TextMeshProUGUI _enemyWave;
    [SerializeField] private TextMeshProUGUI _enemyRemaining;

    [SerializeField] private Image[] _crosshair;
    [SerializeField] private GameObject _hitmarker;

    private float lerpSpeed = 3f;

    [SerializeField] private GameObject _gameOverScreen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(this);
    }

    private void Start()
    {
        m_playerEntity = FindObjectOfType<Player>();
        m_currentPowerUpInfo = FindObjectOfType<PowerUpInfo>();
        m_enemySpawner = FindObjectOfType<EnemySpawner>();

        Time.timeScale = 1;

        HideCursor();
    }

    private void Update()
    {
        ShowPlayerDeathUI();

        UpdatePlayerUI(m_playerEntity);
    }

    private void UpdatePlayerUI(Player playerScript)
    {
        _playerHPText.text = "HP: " + playerScript.CurrentHP;
        _enemyWave.text = "Wave: " + m_enemySpawner.CurrentWave;
        _enemyRemaining.text = "Enemies: " + m_enemySpawner.CurrentEnemiesRemaining;

        HealthBarFiller();
        HealthBarColorChanger();
        KillCounterFiller();
    }

    private void ShowPlayerDeathUI()
    {
        if (m_playerEntity.CurrentHP <= 0)
        {
            Time.timeScale = 0;
            _playerHPText.gameObject.SetActive(false);
            _gameOverScreen.SetActive(true);
            ShowCursor();
        }
    }

    private void HealthBarFiller()
    {
        float healthPercentage = (float)m_playerEntity.CurrentHP / (float)m_playerEntity.MaxHP;

        _playerHPBar.fillAmount = Mathf.Lerp(_playerHPBar.fillAmount, healthPercentage, lerpSpeed * Time.deltaTime);
    }

    private void HealthBarColorChanger()
    {
        float healthPercentage = (float)m_playerEntity.CurrentHP / (float)m_playerEntity.MaxHP;

        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

        _playerHPBar.color = healthColor;
    }

    private void KillCounterFiller()
    {
        for (int i = 0; i < _killCountBar.Length; i++)
        {
            _killCountBar[i].enabled = DisplayKillCounter(_killCount.Value, i);
        }

        if (_killCount.Value >= m_currentPowerUpInfo.Cost) _powerUpButton.SetActive(true);
        if (_killCount.Value < m_currentPowerUpInfo.Cost) _powerUpButton.SetActive(false);
    }

    private bool DisplayKillCounter(float killCount, int pointNumber)
    {
        return (pointNumber + 1) * 2 <= killCount;
    }

    public void CrosshairColorChanger(string bodyPart)
    {
        Color crosshairColor = Color.white;

        if (bodyPart == "Head") crosshairColor = Color.red;
        if (bodyPart == "Body") crosshairColor = Color.yellow;
        if (bodyPart == "Nothing") crosshairColor = Color.white;

        for (int i = 0; i < _crosshair.Length; i++)
        {
            _crosshair[i].color = crosshairColor;
        }
    }

    public void ShowHitmarker()
    {
        StartCoroutine(HitmarkerCoroutine());
    }

    private IEnumerator HitmarkerCoroutine()
    {
        _hitmarker.SetActive(true);

        yield return new WaitForSeconds(.3f);

        _hitmarker.SetActive(false);
    }

    public void PowerUpUI()
    {
        StartCoroutine(PowerUpCoroutine(m_currentPowerUpInfo.Duration));
    }

    private IEnumerator PowerUpCoroutine(float duration)
    {
        _powerUpHUD.SetActive(true);

        float time = duration;
        while (time > 0)
        {
            _powerUpHUDTimer.fillAmount = time / duration;
            time -= Time.deltaTime;
            yield return null;
        }

        _powerUpHUD.SetActive(false);
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
