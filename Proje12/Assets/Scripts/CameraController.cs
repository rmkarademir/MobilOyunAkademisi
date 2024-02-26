using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float minXRot;
    public float maxXRot;
    private float curXRot;
    public float rotateSpeed;
    public float minZoom;
    public float maxZoom;
    private float curZoom;
    public float zoomSpeed;
    private Camera cam;
    void Start()
    {
        cam = Camera.main;
        curZoom = cam.transform.localPosition.y;
        curXRot = -50;
    }
    void Update()
    {
        //mouse topunu yuvarlama ile kamerayı y ekseninde hareket ettirerek zoom yapar
        curZoom += Input.GetAxis("Mouse ScrollWheel") * -zoomSpeed;
        curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
        cam.transform.localPosition = Vector3.up * curZoom;

        //mouse sağ tuşa basarak sağ-sol hareket ile kamerayı y ekseninde,  
        //yukarı-aşağı hareket ile kamerayı x ekseninde hareket ettirir
        if(Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            curXRot += -y * rotateSpeed;
            curXRot = Mathf.Clamp(curXRot, minXRot, maxXRot);
            transform.eulerAngles = new Vector3(curXRot, transform.eulerAngles.y + (x * rotateSpeed), 0.0f);
        }

        //ileri-geri ve sağ-sol yön tuşları ile kamera hareketi yapar
        Vector3 forward = cam.transform.forward;
        forward.y = 0.0f;
        forward.Normalize();
        Vector3 right = cam.transform.right;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = forward * moveZ + right * moveX;
        move.Normalize();
        move *= moveSpeed * Time.deltaTime;

        transform.position += move;


    }
}
