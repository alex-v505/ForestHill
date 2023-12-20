using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogueScript;
    private bool playerDetected;
    private Animator objectAnimator;

    // Detectar colisi�n con el jugador
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
        // Si perdemos la colisi�n con el jugador, desactivar playerDetected y ocultar el indicador
        if (collision.tag == "Player")
        {
            playerDetected = false;
            dialogueScript.ToggleIndicator(playerDetected);
            dialogueScript.EndDialogue();
        }
    }

    // Mientras est� detectado, si interactuamos, iniciar el di�logo
    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            objectAnimator = GetComponent<Animator>();
            if (objectAnimator != null)
            {
                // Desactivar el Animator si est� presente
                objectAnimator.enabled = false;
            }

            dialogueScript.StartDialogue();
        }
    }
}
