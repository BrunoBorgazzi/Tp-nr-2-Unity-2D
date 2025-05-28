using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;   // Instancia del GameManager para el patrón Singleton
    public int puntaje = 0; // Puntuación del jugador  

    public int vidas = 3; // Número de vidas del jugador
    private bool gameOver = false; // Estado del juego

    // Asegurarse de que el GameManager sea un Singleton
    void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // No destruir el GameManager al cambiar de escena
    }

    void Update()
    {
        // Si estás en Game Over y presionas R reinicia el juego
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarJuego();
        }
    }

    // Método para perder una vida
    public void PerderVida()
    {
        vidas--;
        Debug.Log("Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            GameOver();
        }
        else
        {
            // Reiniciar la escena actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // Método para manejar el Game Over
    private void GameOver()
    {
        gameOver = true;
        Time.timeScale = 0f; // Detener el juego
        Debug.Log("Game Over. ¿Quieres reiniciar? Presiona R");
    }

    // Método para reiniciar el juego
    private void ReiniciarJuego()
    {
        Time.timeScale = 1f;      // Reanudar el juego
        vidas = 3;                // Reiniciar las vidas manualmente
        gameOver = false;
        puntaje = 0;             // Reiniciar la puntuación
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

    // Método para manejar la victoria
    public void Victoria()
    {
        if (puntaje >= 10f)
        {
            gameOver = true;
            Time.timeScale = 0f; // Detener el juego
            Debug.Log("¡Victoria! Tu puntuación final es: " + puntaje);
        }
    }


    // Método para aumentar la puntuación   
    public void ObtenerMoneda()
    {
        puntaje++;
        Debug.Log("Puntuación: " + puntaje);
    }
}

 