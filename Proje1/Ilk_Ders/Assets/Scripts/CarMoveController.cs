using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove( new Vector3( 0f, 0.85f, 0f ), 4f ).SetLoops( -1, LoopType.Yoyo ).SetEase( Ease.Linear );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
