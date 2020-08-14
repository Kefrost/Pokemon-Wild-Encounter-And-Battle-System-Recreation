using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;

    public Animator UpperPart;

    public Animator BottomPart;

    // Start is called before the first frame update
    void Start()
    {
        UpperPart = GameObject.FindGameObjectWithTag("Upper").GetComponent<Animator>();
        BottomPart = GameObject.FindGameObjectWithTag("Bottom").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal =  Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        

        Vector3 move =  new Vector3(horizontal, vertical, 0).normalized;

        if (move != Vector3.zero)
        {
            UpperPart.SetFloat("Horizontal", horizontal);
            UpperPart.SetFloat("Vertical", vertical);
            BottomPart.SetFloat("Horizontal", horizontal);
            BottomPart.SetFloat("Vertical", vertical);
        }

        UpperPart.SetFloat("Speed", move.sqrMagnitude);
        BottomPart.SetFloat("Speed", move.sqrMagnitude);

        if (horizontal == 1 && vertical == 1 || horizontal == -1 && vertical == -1 || horizontal == 1 && vertical == -1 || horizontal == -1 && vertical == 1)
        {
            move = new Vector3(0, 0, 0);
        }

        transform.Translate(move * Time.deltaTime * speed);

        //this.gameObject.transform.position += move * Time.deltaTime * speed;
    }
}
