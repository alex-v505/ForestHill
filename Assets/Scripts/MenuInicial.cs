using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Salir()
    {
        Debug.Log("Salir...");
        /*Solo es para pausar UNITY, dar sensacion de salir del juego
#if UNITY_EDITOR
        Debug.Break();
#endif
        */
        Application.Quit();
    }
}
