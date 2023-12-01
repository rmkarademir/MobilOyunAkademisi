using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using TMPro;

public class CharacterMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float tankSpeed = 20.0f,
                 tankRotateSpeed = 0.1f,
                 tankFireRange = 20f,
                 barrelRotateSpeed = 3.0f;
    private int ammo = 40;
    public GameObject rayPoint, tankBarrel;
    public ParticleSystem fireBust;
    public TextMeshProUGUI textAmmo;
    Vector3 movement,defaultBarrelPos;
    Quaternion rotate;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        defaultBarrelPos = tankBarrel.transform.localEulerAngles;
        textAmmo.text = ammo.ToString()+"/40";
    }

    // Update is called once per frame
    void Update()
    {
        movement = transform.position + (transform.forward * Input.GetAxis("Vertical") * tankSpeed * Time.deltaTime);
        rotate = transform.rotation * Quaternion.Euler(Vector3.up * (Input.GetAxis("Horizontal") * tankRotateSpeed * Time.deltaTime)); 
        {
        //transform.Translate(movement * tankSpeed * Time.deltaTime);
        // transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * tankSpeed * Time.deltaTime);
        // transform.Rotate(0f, Input.GetAxis ("Horizontal") * tankRotateSpeed, 0f);
        // if(Input.GetKey(KeyCode.W))
        // {
        //     //GetComponent<Rigidbody>().AddForce(transform.forward * tankSpeed * Time.deltaTime);
        //     GetComponent<Rigidbody>().velocity = transform.forward * tankSpeed * Time.deltaTime;
        // }
        // if(Input.GetKey(KeyCode.S))
        // {
        //     //GetComponent<Rigidbody>().AddForce(-transform.forward * tankSpeed * Time.deltaTime);
        //     GetComponent<Rigidbody>().velocity = -transform.forward * tankSpeed * Time.deltaTime;
        // }
        // if(Input.GetKey(KeyCode.D))
        // {
        //     transform.Rotate(0, tankRotateSpeed * Time.deltaTime, 0);
        // }
        // if(Input.GetKey(KeyCode.A))
        // {
        //     transform.Rotate(0, -tankRotateSpeed * Time.deltaTime, 0);
        // }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fireBust.Play();
            ammo--;
            textAmmo.text = ammo.ToString()+"/40";
            Hit();
        }
        if(Input.GetKey(KeyCode.O))
        {
            Debug.Log(tankBarrel.transform.rotation.x);
            if(tankBarrel.transform.rotation.x > -0.088f)
            {
                tankBarrel.transform.Rotate(-barrelRotateSpeed * Time.deltaTime, 0, 0);
            }
        }       
        if(Input.GetKey(KeyCode.L))
        {
            Debug.Log(tankBarrel.transform.rotation.x);
            if(tankBarrel.transform.rotation.x < 0.088f)
            {
                tankBarrel.transform.Rotate(barrelRotateSpeed * Time.deltaTime, 0, 0);
            }    
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            tankBarrel.transform.DOLocalRotate(defaultBarrelPos, 0.6f); 
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
        Ray ray = new Ray(rayPoint.transform.position,rayPoint.transform.forward);
        if(Physics.Raycast(ray, out hit, tankFireRange))
        {
            if(hit.collider.gameObject.tag == "Hedef")
            {
                ammo += 5;
                if(ammo>=40){ammo=40;}
                textAmmo.text = ammo.ToString()+"/40";
                hit.collider.gameObject.transform.DOShakeScale(0.5f, 5.0f, 20).OnComplete(
                    ()=>hit.collider.transform.DOScale(Vector3.zero,0.3f).OnComplete(
                        ()=>hit.collider.gameObject.SetActive(false)));
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("temas oldu");
        rb.isKinematic = true;
        //rb.velocity = Vector3.zero;
        rb.isKinematic = false;
    }
    void moveCharacter(Vector3 move, Quaternion rot)
    {
        rb.MovePosition(move);
        rb.MoveRotation(rot);
        //rb.velocity = move * tankSpeed * Time.deltaTime;
    }
}
