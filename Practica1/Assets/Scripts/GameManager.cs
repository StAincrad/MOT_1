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
    private int playerLScore, playerRScore;
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
                    ActivePanels(false);

                    if (servingPlayer.Equals(Player.left))
                    {
                        movementBall = new Vector3(Random.Range(-3.0f, -1.0f), Random.Range(-3.0f, 3.0f));
                        ball.NewMovement(movementBall);
                    }
                    else if (servingPlayer.Equals(Player.right))
                    {
                        movementBall = new Vector3(Random.Range(1.0f, 3.0f),Random.Range(-3.0f, 3.0f));
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

        if (playerRScore.Equals(10) || playerLScore.Equals(10))
            WhoWin();
    }
    
    /// <summary>
    /// Show who is the winner and loser
    /// </summary>
    private void WhoWin()
    {
        gameState = State.done;
        ActivePanels(true);

        if (playerLScore.Equals(10))
        {
            winningPlayer = Player.left;
            marcadorL.text = "Ganador";
            marcadorR.text = "Perdedor";
        }
        else
        {
            winningPlayer = Player.right;
            marcadorL.text = "Perdedor";
            marcadorR.text = "Ganador";
        }
        servingPlayer = OppositePlayer(winningPlayer);
    }
    /// <summary>
    /// Activate or deactive infomation panels
    /// </summary>
    /// <param name="status">True for activate false otherwise</param>
    private void ActivePanels(bool status)
    {
        marcadorL.gameObject.SetActive(status);
        marcadorR.gameObject.SetActive(status);
        enterToStart.gameObject.SetActive(status);
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
    /// Determinate who will serve
    /// </summary>
    private Player OppositePlayer(Player winningPlayer)
    {
        return winningPlayer.Equals(Player.left) ? Player.left : Player.right;
    }
}
