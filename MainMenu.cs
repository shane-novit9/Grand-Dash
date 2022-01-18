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
        StartCoroutine(WaitForLevelLoad());
        
    }

    public void VehicleSelectButton()
    {
        // Enter Vehicle Select Screen
        audioSource.PlayOneShot(clickNoise);
        StartCoroutine(WaitForSelectorLoad());
        
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

    private IEnumerator WaitForLevelLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(LevelSelect);
    }

    private IEnumerator WaitForSelectorLoad()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(VehicleSelect);
    }
}
