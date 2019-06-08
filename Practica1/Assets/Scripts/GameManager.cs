using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Variables públicas
    public static GameManager instance = null;
    public const float altoMundo = 6;
    public const float anchoMundo = 8;
    public Player servingPlayer;
    public Player winningPlayer;
    public State gameState;

    //Variables privadas
    int playerLScore, playerRScore;

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
                    break;
                case State.done:
                    gameState = State.serve;

                    playerLScore = 0;
                    playerRScore = 0;
                    break;
            }
        }

        //Controla el final del partido
        if (playerLScore >= 10)
        {
            gameState = State.done;
            winningPlayer = Player.left;
            servingPlayer = OppositePlayer(winningPlayer);
        }
        else if (playerRScore >= 10)
        {
            gameState = State.done;
            winningPlayer = Player.right;
            servingPlayer = OppositePlayer(winningPlayer);
        }
    }

    /// <summary>
    /// Sirve para saber el estado del juego
    /// </summary>
    public State GameState()
    {
        return gameState;
    }

    /// <summary>
    /// Sirve para saber quién saca; hacia dónde irá la bola
    /// </summary>
    public Player ServingPlayer()
    {
        return servingPlayer;
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
                break;
            case Player.right:
                playerLScore++;
                servingPlayer = Player.right;
                break;
        }
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
