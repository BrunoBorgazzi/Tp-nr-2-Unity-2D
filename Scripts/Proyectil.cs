using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    public float tiempoDeVida = 5f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida); // Se destruye después de un tiempo
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.PerderVida(); // Llama al método para perder una vida
            Destroy(gameObject); // Destruye el proyectil
        }
        else if (!collision.isTrigger)
        {
            Destroy(gameObject); // Se destruye al chocar con algo que no sea trigger
        }
    }
}
