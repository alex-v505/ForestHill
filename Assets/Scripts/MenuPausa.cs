using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menuPausaUI;
    public bool activado = false;

    private void Start()
    {
        if(activado==false)
        {
            menuPausaUI.SetActive(false);
        }
        else
        {
            menuPausaUI.SetActive(true);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(Time.timeScale);
            if (Time.timeScale == 0f)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }
    void PausarJuego()
    {
        Time.timeScale = 0f; // Pausar el tiempo
        menuPausaUI.SetActive(true); // Activar el menú de pausa
    }
    public void ReanudarJuego()
    {
        
        Time.timeScale = 1f; // Reanudar el tiempo
        menuPausaUI.SetActive(false); // Desactivar el menú de pausa
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
