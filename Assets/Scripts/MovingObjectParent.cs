using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all obstacles so they stop moving on a game over
public class MovingObjectParent : MonoBehaviour
{
    // Hardcoded destroy position
    private float destroyXPosition = -10f;
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
    
    // Probably could employ object pooling here
    void Update()
    {
        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }

}
