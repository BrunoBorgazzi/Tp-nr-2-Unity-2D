using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask whatIsGround;

    private bool movingRight = false;

    protected virtual void VUpdate()
    {
        
    }

    void Update()
    {
    
         Patrol(); // Llama al método de patrullaje
        
       
    }


    public void Patrol()
    {
        // Movimiento
        transform.Translate(Vector2.right * speed * Time.deltaTime * (movingRight ? 1 : -1));

        // Raycasts
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f, whatIsGround);
        RaycastHit2D wallInfo = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, 0.1f, whatIsGround);

        if (!groundInfo.collider || wallInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        movingRight = !movingRight;

        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
