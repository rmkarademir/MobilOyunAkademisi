using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObstacleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HorizontalObstacleMove()
    {
        Vector3 pos = transform.localPosition;
        //transform.DOMove(new Vector3(pos.x,-0.8f,pos.z+8), 1f).SetEase(Ease.OutSine).SetLoops(-1,LoopType.Yoyo);
        if(pos.z < -3)
        {
            transform.DOLocalMove(new Vector3(pos.x,-0.8f,pos.z+8), 1f).SetLoops(-1,LoopType.Yoyo);
            //transform.DOLocalMove( new Vector3(pos.x,-0.8f,pos.z+8), 1f).OnComplete(()=> HorizontalObstacleMove());
        }
        else if(pos.z > 3)
        {
            transform.DOLocalMove(new Vector3(pos.x,-0.8f,pos.z-8), 1f).SetLoops(-1,LoopType.Yoyo);
            //transform.DOLocalMove( new Vector3(pos.x,-0.8f,pos.z-8), 1f).OnComplete(()=> HorizontalObstacleMove());
        }    
    }
    public void VerticalObstacleMove()
    {
        Vector3 pos = transform.localPosition;
        transform.DOLocalMove( new Vector3(pos.x-4f,-0.8f,pos.z), 1f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    } 
}
