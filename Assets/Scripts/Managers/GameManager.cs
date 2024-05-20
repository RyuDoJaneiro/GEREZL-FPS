using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void HandlePopUp(GameObject popUp)
    {
        if (popUp.activeSelf == false)
            popUp.SetActive(true);
        else
            popUp.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
