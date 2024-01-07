using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CollectableMove()
    {
        transform.DORotate(new Vector3(0,360,0),1f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
        // Vector3 pos = transform.localPosition;
        // if(pos.z >= 4)
        // {
        //     pos.z = -8;
        // }
        // transform.DOLocalMove(new Vector3(pos.x,pos.y,pos.z+4),1f).OnComplete(()=>CollectableMove());
    }
}
