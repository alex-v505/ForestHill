using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Creditos : MonoBehaviour
{
    public Text textoUI;
    public string[] textos;
    public float duracionTransicion = 1.0f;
    public float tiempoEntreCambios = 3.0f;
    public float tiempoEsperaFinal = 5.0f; // 5 segundos
    public String NombreEscena;
    private int indiceTextoActual;
    private bool enTransicion = false;

    private void Start()
    {
        if (textoUI == null || textos.Length == 0)
        {
            Debug.LogError("Por favor, asigna un objeto Text y al menos un texto en el inspector.");
            return;
        }

        textoUI.text = textos[0];
        indiceTextoActual = 0;

        // Inicia la rutina de cambio automático
        StartCoroutine(CambiarTextoAutomaticamente());
    }

    private void Update()
    {
        // Puedes mantener este método vacío si no necesitas manejar eventos de teclado
    }

    private System.Collections.IEnumerator CambiarTextoAutomaticamente()
    {

        while (indiceTextoActual < textos.Length)
        {
            yield return new WaitForSeconds(tiempoEntreCambios);

            // Your existing code...

            if (indiceTextoActual < textos.Length - 1)
            {
                if (!enTransicion)
                {
                    enTransicion = true;
                    indiceTextoActual++;
                    StartCoroutine(TransicionTexto());
                }
            }
            else
            {
                // If you are at the last text, break out of the loop
                break;
            }
        }

        // Wait for the final seconds
        yield return new WaitForSeconds(tiempoEsperaFinal);

        // Load the new scene
        SceneManager.LoadScene(NombreEscena);
    }

    private System.Collections.IEnumerator TransicionTexto()
    {
        float tiempoInicio = Time.time;
        string textoDestino = textos[indiceTextoActual];

        while (Time.time - tiempoInicio < duracionTransicion)
        {
            float progreso = (Time.time - tiempoInicio) / duracionTransicion;
            textoUI.color = new Color(1f, 1f, 1f, 1f - progreso); // Fades out
            yield return null;
        }

        // Actualiza el texto y reinicia el color
        textoUI.text = textoDestino;
        textoUI.color = Color.white;

        enTransicion = false;
    }
}
