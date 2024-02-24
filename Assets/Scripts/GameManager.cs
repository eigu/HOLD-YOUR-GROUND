using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player playerEntity;

    [SerializeField] private TextMeshProUGUI playerHPText;
    [SerializeField] private TextMeshProUGUI playerSPText;
    [SerializeField] private GameObject gameOverScreen;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        playerEntity = FindObjectOfType<Player>();

        Time.timeScale = 1;

        HideCursor();
    }

    private void Update()
    {
        UpdatePlayerUI(playerEntity);
        ShowPlayerDeathUI();
    }

    private void UpdatePlayerUI(Player playerScript)
    {
        playerHPText.text = "HP: " + playerScript.currentHP;
        playerSPText.text = "SP: " + playerScript.currentSP;
    }

    private void ShowPlayerDeathUI()
    {
        if (!playerEntity)
        {
            Time.timeScale = 0;
            playerHPText.gameObject.SetActive(false);
            gameOverScreen.SetActive(true);
            ShowCursor();
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
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
