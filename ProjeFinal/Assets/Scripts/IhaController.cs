using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using TMPro;
public class IhaController : MonoBehaviour
{
    GameManager gameManager;
    public float ihaSpeed = 20.0f,
                 ihaRotateSpeed = 0.1f,
                 ihaFireRange = 100f;
    public int ihaAmmo;
    public GameObject rayPoint, ihaCamera;
    public TextMeshProUGUI textTarget;
    Vector3 movement,defaultCameraPos;
    Quaternion rotate;
    Rigidbody rb;
    Camera cam;
    void Start()
    {
        gameManager = GameManager.instance;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        defaultCameraPos = ihaCamera.transform.localEulerAngles;
        //textAmmo.text = ammo.ToString()+"/40";
    }
    void Update()
    {
        movement = transform.position + (transform.forward * Input.GetAxis("Vertical") * ihaSpeed * Time.deltaTime);
        rotate = transform.rotation * Quaternion.Euler(Vector3.up * (Input.GetAxis("Horizontal") * ihaRotateSpeed * Time.deltaTime)); 
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = ihaFireRange;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(rayPoint.transform.position, mousePos - rayPoint.transform.position, Color.red);
        
        if(Input.GetMouseButtonDown(0))
        {
            Hit();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            cam.transform.DOLocalRotate(defaultCameraPos, 0.6f); 
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameManager.ChangeGameModeTank();
        }
    }
    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            moveCharacter(movement,rotate);
        }
        else
        {
            rb.isKinematic = true;
            //rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
    }
    void Hit()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, ihaFireRange))
        {
            if(hit.collider.gameObject.tag == "Hedef")
            {
                gameManager.target++;
                textTarget.text = gameManager.target.ToString()+"/"+gameManager.totalTarget.ToString();
                hit.collider.gameObject.transform.DOShakeScale(0.5f, 5.0f, 20).OnComplete(
                    ()=>hit.collider.transform.DOScale(Vector3.zero,0.3f).OnComplete(
                        ()=>hit.collider.gameObject.SetActive(false)));
            }
        }
    }

    void moveCharacter(Vector3 move, Quaternion rot)
    {
        rb.MovePosition(move);
        rb.MoveRotation(rot);
        //rb.velocity = move * tankSpeed * Time.deltaTime;
    }
}
