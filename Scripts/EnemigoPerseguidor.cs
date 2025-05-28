using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoPerseguidor : Enemigo
{
    public Transform jugador;
    public Transform radioDeteccion; // Transform para el radio de detección del jugador
    private float velocidadMin = 1f; // Velocidad mínima cuando está cerca del jugador
    private float velocidadMax = 5f;  // Velocidad máxima cuando está lejos del jugador
    private bool jugadorDetectado = false; // Variable para saber si el jugador está detectado
    private float lejaniaDeteccion = 5f; // Variable para calcular la velocidad a la que deberia acelerar el enemigo
    void Update()
    {
        // Comprobar si el jugador está dentro del rango de detección
        if (jugadorDetectado==true)
        {
            ChasePlayer(); // Llamar al método para perseguir al jugador
        }
        Patrol(); // Llamar al método de patrullaje
       


    }

    // Método para perseguir al jugador
    void ChasePlayer()
    {
        
        float distance = Mathf.Abs(jugador.position.x - transform.position.x);

        // Calcular t como porcentaje de cercanía (más lejos = más velocidad)
        float t = Mathf.Clamp01(distance / lejaniaDeteccion);
        float speed = Mathf.Lerp(velocidadMin, velocidadMax, t);

        // Dirección solo en X
        float direction = Mathf.Sign(jugador.position.x - transform.position.x);

        // Mover al enemigo solo en el eje X
        transform.position += new Vector3(direction * speed * Time.deltaTime, 0f, 0f);
    }


    // Método para detectar la cercania al jugador
    void OnTriggerEnter2D(Collider2D radioDeteccion)
    {
        if (radioDeteccion.CompareTag("Player"))
        {
            jugadorDetectado = true; // El jugador ha sido detectado    
            Debug.Log("Jugador detectado, persiguiendo...");
        }

    }

}   



