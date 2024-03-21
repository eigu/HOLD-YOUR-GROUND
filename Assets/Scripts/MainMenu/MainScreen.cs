using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class TitleScreen : MonoBehaviour
{
    public GameObject mainMenuPanel;

    void Start()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameStart");
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
