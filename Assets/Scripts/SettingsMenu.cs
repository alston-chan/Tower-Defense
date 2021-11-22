using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] AudioSource musicSource;
    [SerializeField] Slider musicSlider;
    public void OpenSettingsMenu() {
        Time.timeScale = 0;
        settingsPanel.SetActive(true); 
    }

    public void CloseSettingsMenu() {
        Time.timeScale = 1;
        settingsPanel.SetActive(false);
    }
    
    public void ChangeMusicVolume() {
        musicSource.volume = musicSlider.value;
    }

    public void QuitGame() {
        Application.Quit();
    }
}