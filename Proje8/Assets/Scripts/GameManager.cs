using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState{
    Start,
    Pause,
    Reset,
    End
}
public class GameManager : MonoBehaviour
{
    private LevelManager _levelManager;
    public GameState currentGameState;
    void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        currentGameState = GameState.Pause;// baslangicta oyun durumu pause modunda
    }

    void Update()
    {
        if(currentGameState == GameState.End)
        {
            _levelManager.EndLevel();
            currentGameState = GameState.Pause;
        }
        else if(currentGameState == GameState.Reset)
        {
            _levelManager.ResetGame();
            currentGameState = GameState.Pause;
        }
    }

    public void StartGame(){
        currentGameState = GameState.Start;// oyun durumu start modunda
        _levelManager.StartLevel();
    } 
}
