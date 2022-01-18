using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VehicleSelector : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip startSound;
    public GameObject[] carList;

    private int selectedVehicle = 0;
    private AudioSource source;

    private const int LevelSelectScreen = 2;
    private const int MenuLocation = 0;

    private const string selectedCharacterDataName = "Selected Vehicle";

    void Start()
    {
        HideAll();
        carList[selectedVehicle].SetActive(true);
        source = GetComponent<AudioSource>();
    }

    private void HideAll()
    {
        foreach (GameObject car in carList)
        {
            car.SetActive(false);
        }
    }

    public void NextCar()
    {
        source.PlayOneShot(clickSound);
        carList[selectedVehicle].SetActive(false);
        selectedVehicle = (selectedVehicle + 1) % carList.Length;
        carList[selectedVehicle].SetActive(true);
    }

    public void PreviousCar()
    {
        source.PlayOneShot(clickSound);
        carList[selectedVehicle].SetActive(false);
        selectedVehicle--;
        if (selectedVehicle < 0)
        {
            selectedVehicle = carList.Length - 1;
        }
        carList[selectedVehicle].SetActive(true);
    }

    public void ExitMenu()
    {
        source.PlayOneShot(clickSound);
        StartCoroutine(WaitToExit());
    }

    public void startGame()
    {
        source.PlayOneShot(startSound);
        PlayerPrefs.SetInt(selectedCharacterDataName, selectedVehicle);
        StartCoroutine(WaitToPlay());
    }

    private IEnumerator WaitToExit()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(MenuLocation);
    }

    private IEnumerator WaitToPlay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }
}
