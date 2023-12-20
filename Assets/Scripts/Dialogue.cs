using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Campos
    // Ventana
    public GameObject window;
    // Indicador
    public GameObject indicator;
    // Componente de texto
    public TMP_Text dialogueText;
    // Componente de imagen (nuevo)
    public Image dialogueImage;
    // Componente de etiqueta (nuevo)
    public TMP_Text dialogueLabel;

    // Lista de di�logos
    public List<DialogueData> dialogues;
    // Velocidad de escritura
    public float writingSpeed;
    // �ndice en el di�logo
    private int index;
    // �ndice de caracteres
    private int charIndex;
    // Booleano de inicio
    private bool started;
    // Booleano de espera para el siguiente
    private bool waitForNext;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    // Iniciar di�logo
    public void StartDialogue()
    {
        if (started)
            return;

        // Booleano para indicar que hemos comenzado
        started = true;

        // Mostrar la ventana
        ToggleWindow(true);
        // Ocultar el indicador
        ToggleIndicator(false);
        // Comenzar con el primer di�logo
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        // Iniciar �ndice en cero
        index = i;
        // Restablecer el �ndice de caracteres
        charIndex = 0;
        // Borrar el texto del componente de di�logo
        dialogueText.text = string.Empty;
        // Establecer los componentes de imagen y etiqueta seg�n los datos del di�logo (nuevo)
        dialogueImage.sprite = dialogues[index].image;
        dialogueLabel.text = dialogues[index].label;
        // Comenzar a escribir
        StartCoroutine(Writing());
    }

    // Finalizar di�logo
    public void EndDialogue()
    {
        // Se desactiva el inicio
        started = false;
        // Desactivar tambi�n la espera para el siguiente
        waitForNext = false;
        // Detener todos los IEnumerators
        StopAllCoroutines();
        // Ocultar la ventana
        ToggleWindow(false);

        Debug.Log("Di�logo finalizado. Animator desactivado.");
    }

    // L�gica de escritura
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index].text;
        // Escribir el car�cter
        dialogueText.text += currentDialogue[charIndex];
        // Aumentar el �ndice de caracteres
        charIndex++;
        // Asegurarse de haber llegado al final de la oraci�n
        if (charIndex < currentDialogue.Length)
        {
            // Esperar x segundos
            yield return new WaitForSeconds(writingSpeed);
            // Reiniciar el mismo proceso
            StartCoroutine(Writing());
        }
        else
        {
            // Finalizar esta oraci�n y esperar la siguiente
            waitForNext = true;
        }
    }

    private void Update()
    {
        if (!started)
            return;

        if (waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;

            // Verificar si estamos dentro del alcance de la lista de di�logos
            if (index < dialogues.Count)
            {
                // Si es as�, obtener el siguiente di�logo
                GetDialogue(index);
            }
            else
            {
                // Si no, finalizar el proceso de di�logo
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}

// Nueva clase para representar datos de di�logo
[System.Serializable]
public class DialogueData
{
    public string text;
    public Sprite image; // Imagen para el di�logo (nuevo)
    public string label; // Etiqueta para el di�logo (nuevo)
}
