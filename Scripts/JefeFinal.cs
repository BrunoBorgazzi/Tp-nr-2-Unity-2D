using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : MonoBehaviour
{

    public GameObject proyectilPrefab;       // Prefab del proyectil
    public Transform puntoDisparo;           // Posición desde donde dispara
    public float velocidadProyectil = 10f;   // Velocidad del proyectil
    public float tiempoEntreDisparos = 2f;   // Tiempo entre cada disparo

    private float tiempoDisparo;
    private Transform jugador;

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Verifica si es tiempo de disparar
        if (Time.time >= tiempoDisparo)
        {
            Disparar();
            tiempoDisparo = Time.time + tiempoEntreDisparos;
        }
    }

    void Disparar()
    {
        if (jugador == null) return;

        // Calcula la dirección hacia el jugador
        Vector2 direccion = (jugador.position - puntoDisparo.position).normalized;

        // Instancia el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

        // Le da velocidad al proyectil
        Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
        rb.velocity = direccion * velocidadProyectil;
    }
}


