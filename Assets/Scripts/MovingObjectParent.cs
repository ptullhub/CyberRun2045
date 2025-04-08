using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all obstacles so they stop moving on a game over
public class MovingObjectParent : MonoBehaviour
{
    // Hardcoded destroy position
    private float destroyXPosition = -10f;

    public Vector2 spawnLocation;
    public void Start()
    {
        GameManager.GameInstance.onGameOver.AddListener(StopMoving);
    }

    private void StopMoving()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void OnDestroy()
    {
        GameManager.GameInstance.onGameOver.RemoveListener(StopMoving);
    }
    
    // Destroy object if it gets to -10 on the x
    void Update()
    {
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }

}
