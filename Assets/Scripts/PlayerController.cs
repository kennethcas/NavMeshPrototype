using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    //public Camera cam;
    //public NavMeshAgent agent;

    public float movementSpeed = 3;
    public float jumpForce = 300;
    public float timeBeforeNextJump = 1.2f;

    private float canAttack = 0f;
    public float timeBeforeNextAttack = 1.2f;

    Animator anim;
    Rigidbody rb;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //MovePlayer();
        ControlPlayer();
    }

    void ControlPlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            anim.SetInteger("Walk", 1);

            rb.velocity = movement;
            //transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
        }
        else {
            anim.SetInteger("Walk", 0);
        }

        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        /*if (Input.GetButtonDown("Jump") && Time.time > canJump)
        {
                rb.AddForce(0, jumpForce, 0);
                canJump = Time.time + timeBeforeNextJump;
                anim.SetTrigger("jump");
        }*/

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > canAttack)
        {
            canAttack = Time.time + timeBeforeNextAttack;
            anim.SetTrigger("attack");
        }


    }

    /*void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0)) //if left mouse clicked
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //MOVE
                agent.SetDestination(hit.point);

            }
        }
    }*/
}