using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public Scene sceneScript;
    private bool playerDetected;

    // Detectar activador con el jugador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTRA");
        // Si activamos al jugador, habilitar playerDetected y mostrar el indicador
        if (collision.tag == "Player")
        {
            playerDetected = true;
            sceneScript.ToggleIndicator(playerDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si perdemos el activador con el jugador, desactivar playerDetected y ocultar el indicador
        if (collision.tag == "Player")
        {
            playerDetected = false;
            sceneScript.ToggleIndicator(playerDetected);
            //sceneScript.EndScene();
        }
    }

    // Mientras esté detectado, si interactuamos, iniciar el cambio de escena
    private void Update()
    {
        if (playerDetected && Input.GetKeyDown(KeyCode.P))
        {
            sceneScript.ChangeScene();
        }
    }
}
