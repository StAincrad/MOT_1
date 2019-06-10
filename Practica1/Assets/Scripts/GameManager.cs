using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    //Variables públicas
    public static GameManager instance = null;
    public const float altoMundo = 6;
    public const float anchoMundo = 8;
    public Player servingPlayer;
    public Player winningPlayer;
    public State gameState;
    public Text marcadorR;
    public Text marcadorL;
    public Text enterToStart;

    //Variables privadas
    int playerLScore, playerRScore;
    private Ball ball;
    private Vector3 movementBall;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);
    }

    void Start()
    {
        servingPlayer = Player.left;
        gameState = State.start;
        playerLScore = 0;
        playerRScore = 0;
        ball = GameObject.Find("Ball").GetComponent<Ball>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            switch (gameState)
            {
                case State.start:
                    gameState = State.serve;
                    break;
                case State.serve:
                    gameState = State.play;
                    marcadorL.gameObject.SetActive(false);
                    marcadorR.gameObject.SetActive(false);
                    enterToStart.gameObject.SetActive(false);

                    if (servingPlayer == Player.left)
                    {
                        movementBall.x = Random.Range(-3.0f, -1.0f);
                        movementBall.y = Random.Range(-3.0f, 3.0f);
                        ball.NewMovement(movementBall);
                    }
                    else if (servingPlayer == Player.right)
                    {
                        movementBall.x = Random.Range(1.0f, 3.0f);
                        movementBall.y = Random.Range(-3.0f, 3.0f);
                        ball.NewMovement(movementBall);
                    }
                    break;
                case State.done:
                    gameState = State.serve;
                    ball.Reset();
                    playerLScore = 0;
                    playerRScore = 0;
                    break;
            }
        }

        //Controla el final del partido
        if (playerLScore == 10)
        {
            gameState = State.done;
            winningPlayer = Player.left;
            servingPlayer = OppositePlayer(winningPlayer);

            marcadorL.gameObject.SetActive(true);
            marcadorR.gameObject.SetActive(true);
            enterToStart.gameObject.SetActive(true);
            marcadorL.text = "Ganador";
            marcadorR.text = "Perdedor";
        }
        else if (playerRScore == 10)
        {
            gameState = State.done;
            winningPlayer = Player.right;
            servingPlayer = OppositePlayer(winningPlayer);

            marcadorL.gameObject.SetActive(true);
            marcadorR.gameObject.SetActive(true);
            enterToStart.gameObject.SetActive(true);
            marcadorL.text = "Perdedor";
            marcadorR.text = "Ganador";
        }
    }
    
    /// <summary>
    /// Sirve para saber en qué lado ha chocado la pelota
    /// para saber quién servirá en el siguiente saque y para
    /// sumar los puntos a cada jugador.
    /// </summary>
    public void WallHit(Player player)
    {
        switch (player)
        {
            case Player.left:
                playerRScore++;
                servingPlayer = Player.left;
                gameState = State.serve;
                break;
            case Player.right:
                playerLScore++;
                servingPlayer = Player.right;
                gameState = State.serve;
                break;
        }

        marcadorL.text = playerLScore.ToString();
        marcadorR.text = playerRScore.ToString();
        marcadorL.gameObject.SetActive(true);
        marcadorR.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sirve para saber quién servirá después de acabar un partido
    /// </summary>
    private Player OppositePlayer(Player winningPlayer)
    {
        if (winningPlayer == Player.left)
        {
            return Player.right;
        }
        else
        {
            return Player.left;
        }
    }
}
