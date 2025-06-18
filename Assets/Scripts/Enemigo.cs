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
    private float attackTime;
    private float attCooldown;
    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        target = null;
        rb = transform.GetComponent<Rigidbody>();
        animator = this.GetComponentInChildren<Animator>();
        attackTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > attackTime + attCooldown)
        {
            animator.SetFloat("speed", new Vector2(rb.velocity.x, rb.velocity.z).magnitude);
            if (target != null)
            {
                transform.LookAt(target);
                transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
                rb.AddForce(transform.forward * speed * Time.deltaTime);
            }
        }
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == target.gameObject.name)
        {
            target = null;
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", true);
            animator.Play("Z_Attack");
            //animator.GetCurrentAnimatorClipInfo(1);
            attackTime = Time.time;
            attCooldown = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            rb.velocity = new Vector3(0, 0, 0);
            //collision.gameObject.GetComponent<PlayerController>().hp -= damage;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().hp -= damage * Time.deltaTime;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("attack", false);
            //animator.Play("Z_Idle");
        }
    }
}
