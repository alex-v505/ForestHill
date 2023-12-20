using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    // Window
    public GameObject window;
    // Indicator
    public GameObject indicator;

    public String NombreEscena;
    
    private bool started;

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
        ChangeScene();
    }

    public void ChangeScene()
    {
        if ( Input.GetKeyDown(KeyCode.P))
        {
          

            
            SceneManager.LoadScene(NombreEscena);
        }
    }
}
