using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    //Variables privadas
    private Vector3 movement;
    private Vector3 startPosition;
    private Vector3 size;
    private float width, height;
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

        if (transform.position.x > GameManager.anchoMundo / 2 || transform.position.x < -GameManager.anchoMundo / 2)
        {
            Reset();
            movement.x = Random.Range(-2.0f, 2.0f) * Time.deltaTime;
            movement.y = Random.Range(-2.0f, 2.0f)*Time.deltaTime;
        }

        if (GetComponent<Renderer>().bounds.max.y > GameManager.altoMundo / 2 || GetComponent<Renderer>().bounds.min.y < -GameManager.altoMundo / 2)
        {
            movement.y = -movement.y;
        }

        transform.Translate(movement);
    }

    public void Reset()
    {
        transform.position = startPosition;
        movement.x = 0;
        movement.y = 0;
    }

    public float Width()
    {
        return width;
    }

    public float Height()
    {
        return height;
    }
}
