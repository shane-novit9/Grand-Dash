using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private AudioClip clickNoise;

    private AudioSource audioSource;

    private const int VehicleSelect = 1;
    private const int LevelSelect = 2;

    private void Start()
    {
        settingsPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayButton()
    {
        // Enter Level Select Screen
        audioSource.PlayOneShot(clickNoise);
        Invoke("LoadStoryMode", 2f);
        
    }

    private void LoadStoryMode()
    {
        int currentLvl = PlayerPrefs.GetInt(currentLvlDataName, 3);
        PlayerPrefs.SetInt(currentLvlDataName, currentLvl);
        SceneManager.LoadScene(currentLvl);
    }

    public void VehicleSelectButton()
    {
        // Enter Vehicle Select Screen
        audioSource.PlayOneShot(clickNoise);
        Invoke("LoadVehicleSelect", 2f);
        
    }

    private void LoadVehicleSelect()
    {
        SceneManager.LoadScene(VehicleSelect);
    }

    public void SettingsButton()
    {
        // Open Settings
        audioSource.PlayOneShot(clickNoise);
        menuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void ExitSettings()
    {
        // Return to menu
        audioSource.PlayOneShot(clickNoise);
        settingsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
