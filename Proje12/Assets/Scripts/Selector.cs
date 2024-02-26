using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour
{
    public static Selector instance;
    private Camera cam;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cam = Camera.main;
    }

    public Vector3 GetCurrentTilePosition()//mouse konumunu verir
    {
        if(EventSystem.current.IsPointerOverGameObject())//Canvasta bir yere tıklandı ise
        {
            return new Vector3(0,-99,0);
        }
        //tıklanan zemini algılar
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        float rayOut = 0.0f;
        if(plane.Raycast(ray, out rayOut))
        {
            Vector3 newPos = ray.GetPoint(rayOut)-new Vector3(0.05f,0,0.05f);//planedeki karolara tam oturması için 0,05 kaydırma yapar
            newPos = new Vector3(Mathf.CeilToInt(newPos.x),0,Mathf.CeilToInt(newPos.z));//planedeki karolara tam oturması için küsüratları yukarı yuvarlar
            return newPos;
        }
        return new Vector3(0,-99,0);
    }
    void Update()
    {
        
    }
}
