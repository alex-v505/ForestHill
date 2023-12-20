using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Alice : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    private Animator animator;
    private BoxCollider2D collider;
    private Rigidbody2D rb;
    public bool vistaLado;
    public GameObject BulletPrefab;
    private float ultimoDisparo;
    private bool enSalto;
    private bool sentido=false;
    public int salud = 5;
    private int saludInicial = 0;
    public MenuMuerto muerto;

    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        saludInicial = salud;
    }

    // Update is called once per frame
    void Update()
    {

        // Si se mantiene presionada la tecla "S"
        if (vistaLado)
        {
            
            if (Input.GetKey(KeyCode.Space) && !enSalto)
            {
                Saltar();
            }

            if (Input.GetMouseButtonDown(0) && Time.time > ultimoDisparo + 0.25f)
            {
                Disparar();
                ultimoDisparo = Time.time;
            }

            // Actualizar la animación
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoverDerecha();
            sentido = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoverIzquierda();
            sentido = true;
        }

        // Resto del código (común para ambos casos)
        if (Input.GetKey(KeyCode.S))
        {
            MoverAbajo();
        }

        if (Input.GetKey(KeyCode.W))
        {
            MoverArriba();
        }


        // Actualizar la animación
        animator.SetBool("caminar", Input.GetKey(KeyCode.S)); 
        animator.SetBool("caminar_arriba", Input.GetKey(KeyCode.W));
        animator.SetBool("caminar_derecha", Input.GetKey(KeyCode.D));
        animator.SetBool("caminar_izquierda", Input.GetKey(KeyCode.A));

    }

    private void Disparar()
    {
        Vector3 direccion;
        if (sentido == true) direccion = Vector3.left; else direccion = Vector3.right;
        
        
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direccion * 1.2f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDireccion(direccion);
    }
    private void MoverAbajo()
    {
        float movimientoVertical = -1f;  // Cambiado a -1 para mover hacia abajo

        // Calcular la nueva posición
        Vector3 nuevaPosicion = transform.position + new Vector3(0f, movimientoVertical * velocidad * Time.deltaTime, 0f);

        // Mover al personaje
        transform.position = nuevaPosicion;
    }
    private void MoverDerecha()
    {
        float movimientoDerecha = 1f;  // Cambiado a -1 para mover hacia abajo

        // Calcular la nueva posición
        Vector3 nuevaPosicion = transform.position + new Vector3(movimientoDerecha * velocidad * Time.deltaTime, 0f, 0f);

        // Mover al personaje
        transform.position = nuevaPosicion;
    }
    private void MoverIzquierda()
    {
        float movimientoIzquierda= -1f;  // Cambiado a -1 para mover hacia abajo

        // Calcular la nueva posición
        Vector3 nuevaPosicion = transform.position + new Vector3(movimientoIzquierda * velocidad * Time.deltaTime, 0f, 0f);

        // Mover al personaje
        transform.position = nuevaPosicion;
    }

    private void MoverArriba()
    {
        float movimientoArriba = 1f;  // Cambiado a -1 para mover hacia abajo

        // Calcular la nueva posición
        Vector3 nuevaPosicion = transform.position + new Vector3(0f, movimientoArriba * velocidad * Time.deltaTime, 0f);

        // Mover al personaje
        transform.position = nuevaPosicion;
    }
    private void Saltar()
    {
        Vector3 direccion;
        if (sentido)
        {
            direccion = Vector3.left;
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f); // Invierte la escala en el eje X
        }
        else
        {
            direccion = Vector3.right;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // Restaura la escala en el eje X
        }
        animator.SetBool("saltar", true);
        // Calcular la nueva posición
        rb.AddForce(Vector2.up * fuerzaSalto);
        enSalto = true;

    }

    public void Hit()
    {
       
        
        salud = salud - 1;
        
        if (salud == 0)
        {
            animator.SetTrigger("muerte");
            
            StartCoroutine(EsperarFinAnimacionMuerte());
            
            collider.size = new Vector2(collider.size.y, collider.size.x);
        }
    }
    IEnumerator EsperarFinAnimacionMuerte()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

       
        muerto.MuertePersonaje();
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            
            animator.SetBool("saltar", false);
            enSalto = false;
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int saludManzana;
        if (collision.gameObject.tag == "Manzana")
        {
            
            saludManzana = salud + 1;
            if(saludManzana <= saludInicial)
            {
                Debug.Log("Manzan");
                salud += 1;
                Destroy(collision.gameObject);
            }

        }
    }
}
