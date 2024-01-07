using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    Vector3 hareketYonleri;
    Rigidbody rb;
    public float speed = 50.0f;
    public float rotationSpeed = 20.0f;
    float yRotation;
    Vector3 movement;
    Quaternion rotate;
    public AudioSource audioSourceIdle, audioSourceWalk, audioSourceWrongWalk;
    //public AudioClip idleSound,
   //                  walkSound,
   //                  wrongRoadSound;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = transform.position + (transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime);
        rotate = transform.rotation * Quaternion.Euler(Vector3.up * (Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime));

        //MoveCharacter();
                
        // hareketYonleri = Input.GetAxis("Horizontal") * speed * Vector3.right + Vector3.forward * Input.GetAxis("Vertical") * speed;
        // rb.velocity = hareketYonleri;
        // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hareketYonleri), rotationSpeed * Time.deltaTime);
        // yRotation += rotationSpeed * Input.GetAxis("Horizontal");
        // transform.eulerAngles = new Vector3(0, yRotation, 0);

    }
    void FixedUpdate()
    {
        if(Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //audioSourceIdle.Stop();
            //audioSourceWalk.Play();
            animator.SetBool("isRunning",true);
            moveCharacter(movement,rotate);
        }
        else
        {
            //audioSourceWalk.Stop();
            //audioSourceIdle.Play();
            animator.SetBool("isRunning",false);
            rb.isKinematic = true;
            //rb.velocity = Vector3.zero;
            rb.isKinematic = false;
        }
    }
    void moveCharacter(Vector3 move, Quaternion rot)
    {
        rb.MovePosition(move);
        rb.MoveRotation(rot);
        //rb.velocity = move * tankSpeed * Time.deltaTime;
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if(other.tag == "DogruYol")
    //     {
    //         audioSourceIdle.Stop();
    //         audioSourceWrongWalk.Stop();
    //         audioSourceWalk.Play();
    //     }
    // }
    // private void OnTriggerExit(Collider other)
    // {
    //     if(other.tag == "DogruYol")
    //     {
    //         audioSourceWalk.Stop();
    //         audioSourceWrongWalk.Play();
    //     }
    // }
}
