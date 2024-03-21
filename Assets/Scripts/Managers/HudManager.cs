using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }

    private Player m_playerEntity;
    private EnemySpawner m_enemySpawner;
    private PowerUpInfo m_currentPowerUpInfo;

    //Josh Added
    public GameObject inGamePannel;
    public AudioSource audioSource;
    public Slider volumeSlider;

    public AudioSource audioSource2;
    public Slider volumeSlider2;


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

        inGamePannel.SetActive(false);

        audioSource2.volume = volumeSlider2.value / 100f;
        audioSource.volume = volumeSlider.value / 100f;
    }

    private void Update()
    {
        UpdatePlayerUI(m_playerEntity);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inGamePannel != null)
            {
                inGamePannel.SetActive(true);
                Time.timeScale = 0;
                ShowCursor();
            }
             //Time.timeScale = inGamePannel.activeSelf ? 0f : 1f;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            inGamePannel.SetActive(false);
            Time.timeScale = 1;
            HideCursor();
        }
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

    public void CloseIGM()
    {
        inGamePannel.SetActive(false);
        Time.timeScale = 1;
        HideCursor();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void SetBGMVolume()
    {
        audioSource.volume = volumeSlider.value / 100f;

        if (audioSource != null && volumeSlider != null)
        {
        
            audioSource.volume = volumeSlider.value;
        }
        else
        {
            Debug.LogWarning("AudioSource or VolumeSlider is not assigned!");
        }
    }

    public void SetSFXVolume()
    {
        audioSource2.volume = volumeSlider2.value / 100f;

        if (audioSource2 != null && volumeSlider2 != null)
        {

            audioSource2.volume = volumeSlider2.value;
        }
        else
        {
            Debug.LogWarning("AudioSource2 or VolumeSlider2 is not assigned!");
        }
    }
    
}
