using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    //Variables privadas
    private Vector3 movement;
    private Vector3 startPosition;
    private float width, height;
    private Vector3 size;
    private GameManager gm;
    
    void Start()
    {
        movement = Vector3.zero;
        startPosition = gameObject.transform.position;

        size = GetComponent<Collider>().bounds.size;
        width = size.x;
        height = size.y;

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void LateUpdate()
    {
       

        //Controla las colisiones laterales
        if (transform.position.x > GameManager.anchoMundo / 2 || transform.position.x < -GameManager.anchoMundo / 2)
        {
            Reset();
            movement.x = Random.Range(-2.0f, 2.0f) * Time.deltaTime;
            movement.y = Random.Range(-2.0f, 2.0f)*Time.deltaTime;
        }

        //Controla la colisión con el techo
        if (GetComponent<Renderer>().bounds.max.y > GameManager.altoMundo / 2 || GetComponent<Renderer>().bounds.min.y < -GameManager.altoMundo / 2)
        {
            movement.y = -movement.y;
        }

        transform.Translate(movement);
    }

    /// <summary>
    /// Resetea la posición y velocidad de la bola
    /// </summary>
    public void Reset()
    {
        transform.position = startPosition;
        movement.x = 0;
        movement.y = 0;
    }

    /// <summary>
    /// Devuelve el ancho de la bola
    /// </summary>
    public float Width()
    {
        return width;
    }

    /// <summary>
    /// Devuelve el alto de la bola
    /// </summary>
    public float Height()
    {
        return height;
    }

    /// <summary>
    /// Moverá la bola en una dirección
    /// </summary>
    /// <param name="move">
    /// Dirección hacia la que se moverá la bola
    /// </param>
    public void Movement(Vector3 move)
    {
        movement = move;
    }
}
