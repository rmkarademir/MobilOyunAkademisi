using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;
    private LevelManager _levelManager;
    public Button btnStart, btnReset, btnEnd;
    public GameObject menuUI, endUI,resetUI;
    //public Button btnNextLevel;

    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();//GameManager Tag altinda GameManager script ine baglar 
        _levelManager = GameObject.FindWithTag("GameManager").GetComponent<LevelManager>();//GameManager Tag altinda LevelManager script ine baglar 
        SetBindings();
        menuUI.SetActive(true);
    }

    private void SetBindings()
    {
        btnStart.onClick.AddListener(()=>
        {
            _gameManager.StartGame();
            Time.timeScale = 1;
            menuUI.SetActive(false);
        }
        );
        btnEnd.onClick.AddListener(()=>
        {
            _levelManager.StartLevel();
            endUI.SetActive(false);
            menuUI.SetActive(true);
        }
        );
        btnReset.onClick.AddListener(()=>
        {
            _levelManager.StartLevel();
            resetUI.SetActive(false);
            menuUI.SetActive(true);
        }
        );
    }
    void Update()
    {
        
    }
}
