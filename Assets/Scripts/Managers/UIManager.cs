using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private Player m_playerEntity;
    [SerializeField] private IntVariableSO _killCount;
    [SerializeField] private PowerUpInfoSO _powerUpInfo;

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
        m_playerEntity = FindObjectOfType<Player>();

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

        HealthBarFiller();
        ColorChanger();
        KillCounterFiller();
    }

    private void ShowPlayerDeathUI()
    {
        if (m_playerEntity.CurrentHP <= 0)
        {
            Time.timeScale = 0;
            _playerHPText.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            ShowCursor();
        }
    }

    private void HealthBarFiller()
    {
        float healthPercentage = (float)m_playerEntity.CurrentHP / (float)m_playerEntity.MaxHP;

        _playerHPBar.fillAmount = Mathf.Lerp(_playerHPBar.fillAmount, healthPercentage, lerpSpeed * Time.deltaTime);
    }

    private void ColorChanger()
    {
        float healthPercentage = (float)m_playerEntity.CurrentHP / (float)m_playerEntity.MaxHP;

        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercentage);

        _playerHPBar.color = healthColor;
    }

    private void KillCounterFiller()
    {
        for (int i = 0; i < _killCountBar.Length; i++)
        {
            _killCountBar[i].enabled = !DisplayKillCounter(_killCount.Value, i);
        }

        if (_killCount.Value >= _powerUpInfo.Cost) _powerUpButton.SetActive(true);
        if (_killCount.Value < _powerUpInfo.Cost) _powerUpButton.SetActive(false);
    }

    public void PowerUpUI()
    {
        StartCoroutine(PowerUpCoroutine(_powerUpInfo.Duration));
    }

    private IEnumerator PowerUpCoroutine(float duration)
    {
        _powerUpHUD.SetActive(true);

        float time = duration;
        while (time > 0)
        {
            _powerUpHUD.GetComponent<Image>().fillAmount = time / duration;
            time -= Time.deltaTime;
            yield return null;
        }

        _powerUpHUD.SetActive(false);
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
