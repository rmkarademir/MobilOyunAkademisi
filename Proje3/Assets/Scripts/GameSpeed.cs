using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameSpeed instance;
    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
