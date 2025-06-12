using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    private Rigidbody rb;
    public int fuerza;
    public float damage;
    internal GameObject shooter;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fuerza);
        GameObject.Destroy(this.gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                Enemigo enemy = collision.gameObject.GetComponent<Enemigo>();
                enemy.vida -= damage;
                enemy.target = shooter.transform;
                break;
            case "Prop":
                Destroy(collision.gameObject);
                break;
            default:
                break;
        }
        Destroy(transform.gameObject);
    }
}
