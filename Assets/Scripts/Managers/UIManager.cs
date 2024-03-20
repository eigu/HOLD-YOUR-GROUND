using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Player m_playerEntity;
    [SerializeField] private TextMeshProUGUI _playerHPText;
    [SerializeField] private GameObject _gameOverScreen;

    void Start()
    {
        m_playerEntity = FindObjectOfType<Player>();
        HideCursor();
        Time.timeScale = 1;
    }

    void Update()
    {
        ShowPlayerDeathUI();
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
