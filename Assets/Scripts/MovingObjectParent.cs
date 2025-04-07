using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectParent : MonoBehaviour
{
    // Start is called before the first frame update
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
}
