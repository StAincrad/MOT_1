using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    //Variables privadas
    private Vector3 movement;
    private Vector3 startPosition;
    private float width, height;
    private Vector3 size;

    void Start()
    {
        movement = Vector3.zero;
        startPosition = gameObject.transform.position;

        size = GetComponent<Collider>().bounds.size;
        width = size.x;
        height = size.y;
    }

    void LateUpdate()
    {
        transform.Translate(movement*Time.deltaTime);
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
    /// Devuelve el vector de la velocidad actual de la bola
    /// </summary>
    public Vector3 Movement()
    {
        return movement;
    }

    /// <summary>
    /// Moverá la bola en una dirección
    /// </summary>
    /// <param name="move">
    /// Dirección hacia la que se moverá la bola
    /// </param>
    public void NewMovement(Vector3 move)
    {
        movement = move;
    }
}
