using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cinemachine;
public class LevelManager : MonoBehaviour
{
    public Level[] levels;    
    public int currentLevel;
    private Player _player;
    private Vector3 playerDefaultPosition;
    private GameManager _gameManager;
    private UIManager _uIManager;
    private GameObject[] obstacleVerticalObjects;
    private GameObject[] obstacleHorizontalObjects;
    private GameObject[] collectableObjects;
    public CinemachineVirtualCamera virtualCamera; //CinemachineVirtualCamera
    //public TextMeshProUGUI text;
    private void Start() 
    {
        currentLevel = PlayerPrefs.GetInt("PlayerLevel");
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        _uIManager = GameObject.FindWithTag("MainUI").GetComponent<UIManager>();
        playerDefaultPosition = _player.transform.position;
        Time.timeScale = 1;
        obstacleVerticalObjects = GameObject.FindGameObjectsWithTag("ObstacleVertical");
        obstacleHorizontalObjects = GameObject.FindGameObjectsWithTag("ObstacleHorizontal");
        collectableObjects = GameObject.FindGameObjectsWithTag("Collectable");
    }
    public void StartLevel()
    {
        levels[currentLevel % levels.Length].gameObject.SetActive(true);//leveller icerisinde suan secili olan leveli aktif eder
        
        //virtualCamera.DOFieldOfView(40f, 1f);
        _player.transform.position = playerDefaultPosition;
        Time.timeScale = 1;
        if(virtualCamera.m_Lens.FieldOfView != 40.0f)
            DOTween.To(x => virtualCamera.m_Lens.FieldOfView = x, 100, 40, 1f);
        _player.animator.SetBool("isRunning",false);
        //text.text = "LEVEL "+(currentLevel+1);
        foreach (var obstacle in obstacleHorizontalObjects)
        {
            obstacle.GetComponent<ObstacleManager>().HorizontalObstacleMove();
        }
        foreach (var obstacle in obstacleVerticalObjects)
        {
            obstacle.GetComponent<ObstacleManager>().VerticalObstacleMove();
        }
        foreach (var collectable in collectableObjects)
        {
            collectable.GetComponent<CollectableManager>().CollectableMove();
        }
    }
    public void StartNextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("PlayerLevel",currentLevel);
        StartLevel();
        _uIManager.menuUI.SetActive(true);// start butonunu aktif etmek icin
    }
    public void EndLevel()
    {
        _player.animator.SetBool("isRunning",false);
        Time.timeScale = 0;
        DOTween.To(x => virtualCamera.m_Lens.FieldOfView = x, 40, 100, 1f);
        levels[currentLevel % levels.Length].ResetLevel();// level class i altindaki o levelde bulunan nesneleri eski yerlerinde aktif eden fonksiyonu cagirir
        levels[currentLevel % levels.Length].gameObject.SetActive(false);
        _uIManager.endUI.SetActive(true);
        //_uIManager.endUI.GetComponentInChildren<Animation>().Play();
    }  
    public void ResetGame()
    {
        _player.animator.SetBool("isRunning",false);
        Time.timeScale = 0;
        DOTween.To(x => virtualCamera.m_Lens.FieldOfView = x, 40, 100, 1f);
        levels[currentLevel % levels.Length].ResetLevel();// level class i altindaki o levelde bulunan nesneleri eski yerlerinde aktif eden fonksiyonu cagirir
        levels[currentLevel % levels.Length].gameObject.SetActive(false);
        currentLevel = 0;
        _player.objectCount = 0;// player scriptindeki totalCoffeCount degerini degistirir
        //text.text = "LEVEL "+(currentLevel+1);
        //_player.text.text = "0";
        _uIManager.resetUI.SetActive(true);
    }
}
