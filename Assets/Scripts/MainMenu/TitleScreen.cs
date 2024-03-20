using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public Slider volumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // to hide settings pannel at start.
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void OpenSettings()
    {
        // opening the settings tab.
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        // closing the settings tab.
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SetVolume()
    {
        // for master volume slider
        audioSource.volume = volumeSlider.value;
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ReturnToMM()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}
