using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    private Rigidbody2D rb;
    private Vector2 Direccion;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Direccion * velocidad;
    }

    public void SetDireccion( Vector2 direccion)
    {
        Direccion = direccion;
    }
    public void DestruirBullet()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Alice alice = collision.GetComponent<Alice>();
        EnemigoScript enemigo = collision.GetComponent<EnemigoScript>();
        if (alice != null)
        {
            alice.Hit();
        }

        if (enemigo != null)
        {
            enemigo.Hit();
        }

        DestruirBullet();
    }
}
