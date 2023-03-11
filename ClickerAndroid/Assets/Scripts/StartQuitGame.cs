using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartQuitGame : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject gamePanel;

    public AudioSource openClosePanelSource;

    public void StartPauseGame()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        gamePanel.SetActive(!gamePanel.activeSelf);
        openClosePanelSource.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
