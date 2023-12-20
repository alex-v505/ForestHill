using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    public GameObject Alice;
    private float ultimoDisparo;
    private Animator animator;
    public float velocidad;
    public int salud = 3;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Alice == null) { return; }
        Vector3 direccion = Alice.transform.position - transform.position;
        if(direccion.x >=0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        float distancia = Mathf.Abs(Alice.transform.position.x - transform.position.x);
        //Debug.Log(distancia.ToString());
        if(distancia < 7.0f && Time.time > ultimoDisparo + 0.25f)
        {
            Atacar();
            ultimoDisparo = Time.time;
        }
    }
    private void Atacar()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, Alice.transform.position, velocidad* Time.deltaTime);
        animator.SetBool("atacar", true);
    }
    public void Hit()
    {
        salud = salud - 1;
        if (salud == 0) Destroy(gameObject);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Alice jugador = collision.collider.GetComponent<Alice>();

            if (jugador != null)
            {
                jugador.Hit();
            }
        }
    }
}
