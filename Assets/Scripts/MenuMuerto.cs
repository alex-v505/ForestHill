using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMuerto : MonoBehaviour
{
    public GameObject menuMuertoUI;

    private void Start()
    {
        menuMuertoUI.SetActive(false);
    }

    public void MuertePersonaje()
    {
        PausarJuego();
    }
    public void PausarJuego()
    {
        Time.timeScale = 0f; // Pausar el tiempo
        menuMuertoUI.SetActive(true); // Activar el menú de pausa
    }


    // Función para reiniciar la escena
    public void ReiniciarEscena()
    {
        Debug.Log("REANUDAR");
        Time.timeScale = 1f; // Reanudar el tiempo por si acaso estaba pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Función para salir al menú principal
    public void SalirAlMenuPrincipal()
    {
        Time.timeScale = 1f; // Reanudar el tiempo por si acaso estaba pausado
        SceneManager.LoadScene(0); // Reemplaza "MenuPrincipal" con el nombre de tu escena principal
    }
}
