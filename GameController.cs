using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] carObjects;
    public GameObject player;
    public Transform startPosition;

    private string SelectedCharacterDataName = "Selected Vehicle";
    private int selectedCar = 0;

    public GameObject mainCamera;
    private CameraFollow cameraFollowScript;

    void Start()
    {
        cameraFollowScript = mainCamera.GetComponent<CameraFollow>();
        selectedCar = PlayerPrefs.GetInt(SelectedCharacterDataName, 0);
        player = Instantiate(carObjects[selectedCar], startPosition.position, carObjects[selectedCar].transform.rotation);
        cameraFollowScript.setTarget(player.transform);
    }
}
