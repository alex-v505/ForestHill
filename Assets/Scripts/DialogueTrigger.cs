using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool playerDetected;
    private Animator objectAnimator;

    // Detectar colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si activamos al jugador, habilitar playerDetected y mostrar el indicador
        if (collision.tag == "Player")
        {
            playerDetected = true;
            dialogueScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si perdemos la colisión con el jugador, desactivar playerDetected y ocultar el indicador
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }

    // Mientras esté detectado, si interactuamos, iniciar el diálogo
    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            objectAnimator = GetComponent<Animator>();
            if (objectAnimator != null)
            {
                // Desactivar el Animator si está presente
                objectAnimator.enabled = false;
            }

            dialogueScript.StartDialogue();
        }
    }
}
