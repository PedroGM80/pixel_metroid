using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlFinNivel : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinalTexto;
    private ControlDatosJuego datosjuego;

    void start()
        {
            datosjuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
            string mensajeFinal = (datosjuego.Ganado) ? "HAS GANADO!!" : "HAS PERDIDO!!";
            if (datosjuego.Ganado) mensajeFinal += "Puntuacion:" + datosjuego.Puntuacion;

            mensajeFinalTexto.text = mensajeFinal;

        }
    
   
}
