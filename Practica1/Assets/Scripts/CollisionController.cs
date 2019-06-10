using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour {

    //Variables públicas
    public Paddle playerLeft, playerRight;
    public Ball ball;

    //Variables privadas
    private Transform playerLTransform;
    private Transform playerRTransform;
    private Transform ballTransform;
    private Vector3 movementBall;

    void Start()
    {
        playerLTransform = GameObject.Find("Paddle1").GetComponent<Transform>();
        playerRTransform = GameObject.Find("Paddle2").GetComponent<Transform>();
        ballTransform = GameObject.Find("Ball").GetComponent<Transform>();
    }

    void Update()
    {
        if (HorizontalCollision())
        {
            ball.Reset();
        }

        if (VerticalCollision())
        {
            //Cuando se detecte la colisión vertical, la bola cambiará de sentido
            movementBall = ball.Movement();
            movementBall.y = -movementBall.y;
            ball.NewMovement(movementBall); 
        }

        if (AABBCollision())
        {
            Debug.Log("Colisión AABB");
            movementBall = ball.Movement();

            //Incrementa la velocidad al chocar y cambia de sentido
            if (movementBall.x > 0) movementBall.x += 0.5f;
            else if (movementBall.x < 0) movementBall.x -= 0.5f;
            movementBall.x = -movementBall.x;
            movementBall.y = Random.Range(-3.0f, 3.0f);
            ball.NewMovement(movementBall);
        }
    }
    /// <summary>
    /// Sirve para detectar las colisiones horizontales
    /// </summary>
    private bool HorizontalCollision()
    {
        if (ballTransform.position.x + ball.Width() / 2 >= GameManager.anchoMundo / 2)
        {
            GameManager.instance.WallHit(Player.right);
            return true;
        }
        else if (ballTransform.position.x - ball.Width() / 2 <= -GameManager.anchoMundo / 2)
        {
            GameManager.instance.WallHit(Player.left);
            return true;
        }
        else return false;
          
    }

    /// <summary>
    /// Detecta la colisión vertical de la bola
    /// </summary>
    private bool VerticalCollision()
    {
        return ballTransform.position.y + ball.Height() / 2 >= GameManager.altoMundo / 2 
            || ballTransform.position.y - ball.Height() / 2 <= -GameManager.altoMundo / 2;
    }

    /// <summary>
    /// Colisión AABB entre la bola y las palas
    /// </summary>
    private bool AABBCollision()
    {
        //(ball.position.x + ball.width/2, ball.position.y + ball.height/2) --> esquina superior derecha de la bola
        //(ball.position.x + ball.width/2, ball.position.y - ball.height/2) --> esquina inferior derecha de la bola
        //(ball.position.x - ball.width/2, ball.position.y + ball.height/2) --> esquina superior izquierda de la bola
        //(ball.position.x - ball.width/2, ball.position.y - ball.height/2) --> esquina inferior izquierda de la bola

        return
            //Colision AABB con la pala derecha
            (ballTransform.position.x + ball.Width() / 2 >= playerRTransform.position.x - playerRight.Width() / 2 &&
            ballTransform.position.x + ball.Width() / 2 < playerRTransform.position.x + playerRight.Width() / 2 &&
            ballTransform.position.y - ball.Height() / 2 < playerRTransform.position.y + playerRight.Height() / 2 &&
            ballTransform.position.y + ball.Height() / 2 > playerRTransform.position.y - playerRight.Height() / 2)
            ||
            //Colisión AABB con la pala izquierda
            (ballTransform.position.x - ball.Width() / 2 <= playerLTransform.position.x + playerLeft.Width() / 2 &&
            ballTransform.position.x - ball.Width() / 2 > playerLTransform.position.x - playerLeft.Width() / 2 &&
            ballTransform.position.y - ball.Height() / 2 < playerLTransform.position.y + playerLeft.Height() / 2 &&
            ballTransform.position.y + ball.Height() / 2 > playerLTransform.position.y - playerLeft.Height() / 2);
    }
}
