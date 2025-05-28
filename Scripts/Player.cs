using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importar para manejar escenas

public class Player : Main  // Player hereda de Main
{
    
    public Transform GroundCheck; // Transform para comprobar si el jugador está en el suelo
    public LayerMask groundLayer; // LayerMask para definir qué es el suelo
    public float groundCheckRadius = 0.2f; // Radio de comprobación del suelo

    private float SaltoFuerza = 10f; // Fuerza del salto 
    private Rigidbody2D rb; // Rigidbody2D interacciones fisicas del jugador   
    private Vector2 movimiento; // Vector2 para almacenar el movimiento del jugador
    private bool isGrounded; // Variable para comprobar si el jugador está en el suelo
    private bool puedeDobleSalto = false; // Variable para controlar el doble salto
    private bool powerUpDobleSalto = false; // Variable para controlar si el jugador tiene el power-up de doble salto
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        // Capturar entrada del jugador
        movimiento.x = Input.GetAxis("Horizontal"); // Movimiento horizontal
    
        // Comprobar si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            // Reinicia el doble salto
            puedeDobleSalto = true;
        }

        // Si el jugador presiona el botón de salto y está en el suelo, aplicar fuerza de salto
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                // Si está en el suelo, aplicar salto normal
                rb.velocity = new Vector2(rb.velocity.x, SaltoFuerza);
            }
            else if (puedeDobleSalto && powerUpDobleSalto)
            {
                // Si no está en el suelo pero puede hacer un doble salto
                puedeDobleSalto = false; // Desactivar el doble salto
                rb.velocity = new Vector2(rb.velocity.x, SaltoFuerza);
            }

        }

        // Normalizar el vector de movimiento para evitar velocidad diagonal mayor
        if (movimiento.magnitude > 1)
        {
            movimiento.Normalize();
        }



    
    }

    void FixedUpdate()
    {
        // Aplicar movimiento al Rigidbody2D
        rb.velocity = new Vector2(movimiento.x * velocidad, rb.velocity.y);

    }

    // Método para registrar la colisión con enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
                // Si el jugador colisiona con un enemigo, perder vida
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Debug.Log("¡Colisionaste con un enemigo!");
            GameManager.instance.PerderVida();
        }
    

    }
    // Método para recoger power-ups y monedas
    void OnTriggerEnter2D(Collider2D collision)
    {   
        // Si el jugador colisiona con un power-up de doble salto, activar el power-up
        if (collision.gameObject.CompareTag("PowerUpDobleSalto"))
        {
            Debug.Log("¡Recogiste el power-up de doble salto!");
            powerUpDobleSalto = true; // Activar el power-up de doble salto
            Destroy(collision.gameObject); // Destruir el objeto del power-up
        }

        // Si el jugador colisiona con una moneda, llamar al metodo obtener moneda
        if (collision.CompareTag("Moneda"))
        {
            Debug.Log("Recogiste una moneda");
            GameManager.instance.ObtenerMoneda(); // Llamar al método para obtener moneda
            GameManager.instance.Victoria(); // Verificar si se cumple la condición de victoria
            Destroy(collision.gameObject);
        }
    }


}