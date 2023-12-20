using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class VidaScript : MonoBehaviour
{
    public Slider saludBarra;
    public Alice alice;

    private void Start()
    {
        alice = GameObject.FindGameObjectWithTag("Player").GetComponent<Alice>();
        saludBarra = GetComponent<Slider>();
        saludBarra.maxValue = alice.salud;
        saludBarra.value = alice.salud;
    }
    private void Update()
    {
        SetSalud();
    }
    public void SetSalud()
    {
        saludBarra.value = alice.salud;
    }
}
