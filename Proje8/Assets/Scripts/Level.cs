using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform objectRoot;
    private List<Transform> objects;
    public List<Vector3> objectDefaultPosition;
    public void Start()
    {
        FindObject();
    }

    private void FindObject()
    {
        objects = new List<Transform>();
        objectDefaultPosition = new List<Vector3>();
        for (int i = 0; i < objectRoot.childCount; i++)// nesnelerin bagli oldugu ana nesnenin alt nesne sayisi kadar doner
        {
            objects.Add(objectRoot.GetChild(i).transform);// ana nesnenin alt nesnelerini nesnelerin tutulacagi listeye ekler
            objectDefaultPosition.Add(objectRoot.GetChild(i).transform.position);// ana nesnenin alt nesnelerinin pozisyon bilgilerinin tutulacagi listeye ekler
        }
    }
    public void ResetLevel()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].position = objectDefaultPosition[i];
            objects[i].SetParent(objectRoot);
            objects[i].gameObject.SetActive(true);
        }
    }

    void Update()
    {
        
    }
}
