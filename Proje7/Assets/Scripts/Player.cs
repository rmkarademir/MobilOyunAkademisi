using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private List<GameObject> objects;// topladigimiz nesneleri ekledigimiz liste
    public int objectCount = 0;// toplanan nesne sayisi
    public float forwardSpeed = 5;
    private float firstTouchX;
    //public float sensivity = 0.5f;
    public TextMeshProUGUI text,textScore;
    private bool isGodMod = false;
    private MeshRenderer meshRenderer;
    public Animator animator;
    void Start()
    {
        objects = new List<GameObject>();
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        meshRenderer = transform.GetComponent<MeshRenderer>();
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (_gameManager.currentGameState != GameState.Start)
        {
            return;
        }
        animator.SetBool("isRunning",true);
        Vector3 moveVector = new Vector3(-1*forwardSpeed*Time.deltaTime,0,0);
        transform.position += moveVector;
        textScore.text = $"Puan: {objectCount}";
        text.text = $"Puan: {objectCount}";
        {// if (_gameManager.currentGameState != GameState.Start)
        // {
        //     return;
        // }

    
        // for (int i = 0; i < objects.Count; i++)
        // {
        //     objects[i].transform.position = new Vector3(
        //     objects[i].transform.position.x,
        //     //Mathf.Lerp(transform.position.x, transform.position.x+(20*(i+1)), 0.9f*Time.deltaTime),
        //     objects[i].transform.position.y,
        //     Mathf.Lerp(transform.position.z, objects[i].transform.position.z, 0.1f*Time.deltaTime));// toplanan nesnelerin pozisyonlarını x ve y ekseninde aynen alir, z ekseninde player ile nesne arasindaki farki 0.01f hiziyla kapatarak ayni hizaya getirir
        // }
    
        // Vector3 moveVector = new Vector3(-1*forwardSpeed*Time.deltaTime,0,0); // x ekseninde - yonde ilerler
        // float diff = 0;
        // if (Input.GetMouseButtonDown(0))
        // {
        //     firstTouchX = Input.mousePosition.x; //mouse un z eksenindeki degerini alir
        // }
        // else if(Input.GetMouseButton(0))//mouse un sol tusuna basildigi surece
        // {
        //     float lastTouchX = Input.mousePosition.x;
        //     diff = lastTouchX - firstTouchX; // mouse un son degeri ile ilk degeri arasindaki farki bulurs
        //     //float z = Mathf.Clamp(z,-2,2)diff*Time.deltaTime;
        //     moveVector += new Vector3(0,0,diff*Time.deltaTime*sensivity);// moveVector'e x eksenindeki hareketi ekler
        //     firstTouchX = lastTouchX;
        // }
        // transform.position += moveVector;
        // Vector3 newPos = transform.position;
        // newPos.z = Mathf.Clamp(newPos.z,-4f,4f);
        // transform.position = newPos;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Collectable":
                objectCount++;
                other.gameObject.SetActive(false);
            break;
            case "ObstacleHorizontal":
                if(!isGodMod)
                {
                    //animator.SetBool("isRunning",false);
                    _gameManager.currentGameState = GameState.Reset;
                }
            break;
            case "ObstacleVertical":
                if(!isGodMod)
                {
                    //animator.SetBool("isRunning",false);
                    _gameManager.currentGameState = GameState.Reset;
                }
            break;
            case "PowerUp1":
                //StartCoroutine(SpeedUp());
            break;
            case "PowerUp2":
                StartCoroutine(GodMood());
            break;
            case "Finish":
                //animator.SetBool("isRunning",false);
                _gameManager.currentGameState = GameState.End;
            break;
        }
    }
    IEnumerator SpeedUp(){
        //var tempColor = meshRenderer.material.color;
        forwardSpeed*=2;
        //meshRenderer.material.color = Color.blue;
        yield return new WaitForSeconds(2);
        forwardSpeed/=2;
        //meshRenderer.material.color = tempColor;
    }
    IEnumerator GodMood(){
        //var tempColor = meshRenderer.material.color;
        isGodMod = true;
        //meshRenderer.material.color = Color.red;
        transform.DOScale(1.4f,0.3f);
        yield return new WaitForSeconds(2);
        transform.DOScale(1,0.3f);
        isGodMod = false;
        //meshRenderer.material.color = tempColor;
    }
}
