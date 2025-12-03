using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    public GameObject playerPrefeb;

    public Vector3 SpawnPoint = new Vector3(0, 1, 10);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public Player InstantiatePlayer()
    {
        return Instantiate(playerPrefeb, new Vector3(0, 1, 10), Quaternion.identity).GetComponent<Player>();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50, 26951); //Æ÷Æ®

    }
    private void OnApplicationQuit()
    {
        Server.Stop();
    }
}
