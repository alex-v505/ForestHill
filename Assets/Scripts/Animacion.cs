using System.Collections;
using UnityEngine;

public class Animacion : MonoBehaviour
{
    public Animator animator;
    public float tiempoEspera = 3f;
    public string triggerName = "Llamando";
    private BoxCollider2D boxCollider2D;

    void Start()
    {
        // Desactiva el componente Animator al inicio
        animator.enabled = false;

        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = false;
        // Inicia la corrutina para esperar un tiempo y luego ejecutar la animación
        StartCoroutine(EjecutarAnimacionDespuesDeTiempo());
        
    }

    IEnumerator EjecutarAnimacionDespuesDeTiempo()
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(tiempoEspera);

        // Activa el componente Animator y establece el trigger
        animator.enabled = true;
        animator.SetTrigger(triggerName);
        boxCollider2D.isTrigger = true;
    }

    // Este método será llamado por un AnimationEvent al final de la animación
    /*public void IniciarDialogoDespuesDeAnimacion()
    {
        // Aquí puedes activar tu sistema de diálogo
        if (dialogueSystem != null)
        {
            dialogueSystem.StartDialogue();
        }
    }*/
}
