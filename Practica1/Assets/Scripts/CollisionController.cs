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
    private GameManager gm;
    void Start()
    {
        playerLTransform = GameObject.Find("Paddle1").GetComponent<Transform>();
        playerRTransform = GameObject.Find("Paddle2").GetComponent<Transform>();
        ballTransform = GameObject.Find("Ball").GetComponent<Transform>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (HorizontalCollision())
        {
            ball.Reset();
        }
    }
    /// <summary>
    /// Sirve para detectar las colisiones horizontales
    /// </summary>
    private bool HorizontalCollision()
    {
        if (ballTransform.position.x + ball.Width() / 2 >= GameManager.anchoMundo / 2)
        {
            gm.WallHit(Player.right);
            return true;
        }
        else if (ballTransform.position.x - ball.Width() / 2 <= -GameManager.anchoMundo / 2)
        {
            gm.WallHit(Player.left);
            return true;
        }
        else return false;
          
    }
}
