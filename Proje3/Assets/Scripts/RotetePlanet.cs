using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotetePlanet : MonoBehaviour
{
    GameSpeed _gameSpeed;
    public GameObject sun;
    public float speed = 1f;
    float gameSpeed;
    void Start()
    {
        _gameSpeed = GameSpeed.instance;
    }
    void Update()
    {
        gameSpeed = _gameSpeed.speed;
        transform.RotateAround(sun.transform.position, sun.transform.up,(speed * gameSpeed) * Time.deltaTime);
    }
}
