using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    public float velocidad;
    public Vector3 posicionFin;

    private Vector3 posicionInicio;
    private bool moviendoAFin;
    
    void Start()
    {
        posicionInicio = transform.position;
        moviendoAFin = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }

    private void MoverEnemigo()
    {
        //1. Calcular la posicion de Destino
        Vector3 posicionDestinio = (moviendoAFin) ? posicionFin : posicionInicio;
        //2. Mover el enemigo
        transform.position = Vector3.MoveTowards(transform.position, posicionDestinio, velocidad * Time.deltaTime);

        //Cambio de direcci�n
        if (transform.position == posicionFin) moviendoAFin = false;
        if (transform.position == posicionInicio) moviendoAFin = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Si el objeto que ha colisionado con el Enemigo es el Jugado
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().QuitarVida();
        }
    }

}
