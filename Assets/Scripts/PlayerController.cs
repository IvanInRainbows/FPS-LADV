using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject camara;
    private Transform puntoBala;
    public GameObject balaPrefab;
    private float t;
    private float shotTime = 0;
    [Tooltip("La cantidad de disparos por segundo que hace el arma.")]
    public float cadencia;
    public float maxHP;
    internal float hp;
    public Text tx;
    // Start is called before the first frame update
    void Start()
    {
        cadencia = 1/cadencia;
        puntoBala = GameObject.Find("BulletInstantiatePosition").transform;
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        t+=Time.deltaTime;
        if((Input.GetKey(KeyCode.Mouse0) || Input.GetAxis("Mouse ScrollWheel") > 0f) && t >= shotTime + cadencia){
            Instantiate(balaPrefab, position: puntoBala.position, camara.transform.rotation).GetComponent<Bala>().shooter = this.transform;
            shotTime = t; 
        }
        if (hp <= 0){
            Destroy(this.gameObject);
        }
        tx.text = Math.Round(hp).ToString();
        if (Input.GetKeyDown(KeyCode.Y)){
            hp -= 10;
        }
    }
}
