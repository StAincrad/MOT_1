using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    //Variables públicas
    public KeyCode up, down; //Controles de las palas
    public float speed; //Velocidad de las palas

    //Variables privadas
    float limY;
    float width, height;
    Vector3 size;

    void Start()
    {
        limY = GameManager.altoMundo / 2;

        size = GetComponent<Collider>().bounds.size;
        width = size.x;
        height = size.y;
    }
    void Update()
    {
        if (Input.GetKey(up) && GetComponent<Renderer>().bounds.max.y < limY)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(down) && GetComponent<Renderer>().bounds.min.y > -limY)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
        }
    }

    /// <summary>
    /// Devuelve el ancho de la pala
    /// </summary>
    public float Width()
    {
        return width;
    }

    /// <summary>
    /// Devuelve el alto de la pala
    /// </summary>
    public float Height()
    {
        return height;
    }
}
