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

    // Lista de diálogos
    public List<DialogueData> dialogues;
    // Velocidad de escritura
    public float writingSpeed;
    // Índice en el diálogo
    private int index;
    // Índice de caracteres
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

    // Iniciar diálogo
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
        // Comenzar con el primer diálogo
        GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
        // Iniciar índice en cero
        index = i;
        // Restablecer el índice de caracteres
        charIndex = 0;
        // Borrar el texto del componente de diálogo
        dialogueText.text = string.Empty;
        // Establecer los componentes de imagen y etiqueta según los datos del diálogo (nuevo)
        dialogueImage.sprite = dialogues[index].image;
        dialogueLabel.text = dialogues[index].label;
        // Comenzar a escribir
        StartCoroutine(Writing());
    }

    // Finalizar diálogo
    public void EndDialogue()
    {
        // Se desactiva el inicio
        started = false;
        // Desactivar también la espera para el siguiente
        waitForNext = false;
        // Detener todos los IEnumerators
        StopAllCoroutines();
        // Ocultar la ventana
        ToggleWindow(false);

        Debug.Log("Diálogo finalizado. Animator desactivado.");
    }

    // Lógica de escritura
    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index].text;
        // Escribir el carácter
        dialogueText.text += currentDialogue[charIndex];
        // Aumentar el índice de caracteres
        charIndex++;
        // Asegurarse de haber llegado al final de la oración
        if (charIndex < currentDialogue.Length)
        {
            // Esperar x segundos
            yield return new WaitForSeconds(writingSpeed);
            // Reiniciar el mismo proceso
            StartCoroutine(Writing());
        }
        else
        {
            // Finalizar esta oración y esperar la siguiente
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

            // Verificar si estamos dentro del alcance de la lista de diálogos
            if (index < dialogues.Count)
            {
                // Si es así, obtener el siguiente diálogo
                GetDialogue(index);
            }
            else
            {
                // Si no, finalizar el proceso de diálogo
                ToggleIndicator(true);
                EndDialogue();
            }
        }
    }
}

// Nueva clase para representar datos de diálogo
[System.Serializable]
public class DialogueData
{
    public string text;
    public Sprite image; // Imagen para el diálogo (nuevo)
    public string label; // Etiqueta para el diálogo (nuevo)
}
