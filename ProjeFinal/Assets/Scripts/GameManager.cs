using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameModeTank = true,
                GameModeIha = false;
    public GameObject Tank,TankCam,Iha,IhaCamTop,IhaCam,ammoTextBox1,ammoTextBox2,targets;
    public int target = 0, totalTarget = 0;
    void Awake()
    {
        instance = this;
        totalTarget = targets.transform.childCount;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ChangeGameModeIha()
    {
        TankCam.SetActive(false);
        Tank.SetActive(false);
        ammoTextBox1.SetActive(false);
        ammoTextBox2.SetActive(false);
        Iha.SetActive(true);
        StartCoroutine(WaitForFunction());
        

    }
    public void ChangeGameModeTank()
    {
        TankCam.SetActive(true);
        Tank.SetActive(true);
        ammoTextBox1.SetActive(true);
        ammoTextBox2.SetActive(true);
        Iha.SetActive(false);
        IhaCamTop.SetActive(true);
        
    }
    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(2); 
        IhaCamTop.SetActive(false);
    }
}
