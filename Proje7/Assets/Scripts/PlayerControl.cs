using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour,IDragHandler
{
    public Transform player;
    private GameManager _gameManager;
    public float forwardSpeed = 5;
    public float sensivity = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = player.position;
        pos.z = Mathf.Clamp(pos.z + (eventData.delta.x / sensivity),-4f,4f);
        player.position = pos;
    }
}
