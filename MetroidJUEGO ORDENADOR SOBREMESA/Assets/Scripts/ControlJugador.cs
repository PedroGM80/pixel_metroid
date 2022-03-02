using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlJugador : MonoBehaviour
{

    public int velocidad;
    public int fuerzaSalto;
    public AudioClip saltoSfx;
    public AudioClip vidaSfx;
    public int puntuacion;
    public int numVidas;
    public int tiempoNivel;
    public Canvas canvas;
    
    

    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animacion;
    private AudioSource audiosource;
    private bool vulnerable;
    private float tiempoInicio;
    private int tiempoEmpleado;
    private ControlHUD hud;
    private ControlDatosJuego datosjuego;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        tiempoInicio = Time.time;
        vulnerable = true;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
        hud = canvas.GetComponent<ControlHUD>();
        datosjuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
    }

    private void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradaX * velocidad, fisica.velocity.y);

    }

    private void Update()
    
    {
        //Salto
         if (Input.GetKeyDown(KeyCode.Space) && TocarSuelo())   {     
            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audiosource.PlayOneShot(saltoSfx); 
         }

    // si va hacia la derecha flipX = false (no volteo)
    if (fisica.velocity.x > 0) sprite.flipX = false;
    // si va hacia la izquierda volteo flipX = true
    else if (fisica.velocity.x < 0) sprite.flipX = true;

      animarjugador();
    hud.SetPowerUpsTxt(GameObject.FindGameObjectsWithTag("PowerUp").Length);
         if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
      GanarJuego();
    
    //Actualiza tiempo Empleado
    tiempoEmpleado = (int)(Time.time - tiempoInicio);
    hud.SetTiempoTxt(tiempoNivel - tiempoEmpleado);
    
    //comprueba si hemos consumido el tiempo del nivel
    if (tiempoNivel - tiempoEmpleado < 0) FinJuego();
     } 

private void GanarJuego()
{
    puntuacion = (numVidas * 100) + (tiempoNivel - tiempoEmpleado);
    datosjuego.Puntuacion = puntuacion;
    datosjuego.Ganado = true;
    SceneManager.LoadScene("FinNivel");
}

     private void animarjugador()
     {
     //jugador Saltando
      if (!TocarSuelo()) animacion.Play("JugadorSaltando");
     //jugador Corriendo
     else if ((fisica.velocity.x > 1 || fisica.velocity.x < -1) && fisica.velocity.y == 0)
       animacion.Play("JugadorCorriendo");
       //jugador Parado
       else if ((fisica.velocity.x < 1 || fisica.velocity.x > -1) && fisica.velocity.y == 0)
       animacion.Play("JugadorParado");

    }

    private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0,-2f,0) , Vector2.down, 0.2f);  
        return toca.collider != null;
    }

    public void FinJuego()
    {
         
        datosjuego.Ganado = false;
        SceneManager.LoadScene("FinNivel");

    }

     public void IncrementarPuntos(int cantidad)
     {
         puntuacion += cantidad;
     }
     public void QuitarVida()
    {    if (vulnerable)
         {
             audiosource.PlayOneShot(vidaSfx); 
             vulnerable = false;
             numVidas --;
             hud.SetVidasTxt(numVidas);
             if (numVidas == 0) FinJuego();
             Invoke("HacerVulnerable", 1f);
             sprite.color = Color.red;
        }
     }   
     private void HacerVulnerable()
       {   
            vulnerable = true;
            sprite.color = Color.white;
       }
    
}
