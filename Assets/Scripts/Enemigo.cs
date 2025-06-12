using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using System;

public class Enemigo : MonoBehaviour
{
    public float speed;
    public float damage;
    internal Transform target;
    private Rigidbody rb;
    public float vida;
    internal Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = null;
        rb = transform.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
        if (target != null)
        {
            transform.LookAt(target);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            rb.AddForce(transform.forward * speed * Time.deltaTime);
        }
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == target.gameObject.name){
            target = null;
            rb.velocity = new Vector3(0,0,0);
        }
    }

}
