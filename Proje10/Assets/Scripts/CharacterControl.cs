using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Animator anim;
    bool onTouch = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                switch (hit.transform.tag)
                {
                    case "Character":
                        if(onTouch == false)
                        {
                            anim.SetTrigger("onTouch");
                            onTouch = true;
                        }    
                        else
                        {
                            anim.SetTrigger("onTouch2");
                            onTouch = false;
                        }
                    break;
                }
            }   
        }    
    }
}
