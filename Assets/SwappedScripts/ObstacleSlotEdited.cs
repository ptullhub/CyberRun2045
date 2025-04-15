using System;
using System.Collections;
using System.Collections.Generic;
using JT.Enums;
using UnityEngine;

public class ObstacleSlot : MonoBehaviour
{

    public ObstacleTable table;
    public Obstacle currentObstacle;

    public SpriteRenderer spriteRenderer;
    [SerializeField]
    GameObject shadow;

    public static Action<int> UpdateScore;

    public Obstacles AssignObstacle()
    {
        int rng = UnityEngine.Random.Range(0,table.obstacleChances.Count - 1);

        currentObstacle = table.obstacleChances[rng];
        spriteRenderer.sprite = currentObstacle.ObstacleSprite;

        // Replace with switch
        switch (currentObstacle.ObstacleType)
        {
            case (Obstacles.Rock):
                spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
                shadow.SetActive(true);
                break;
            case (Obstacles.Empty):
                shadow.SetActive(false);
                break;
            case (Obstacles.Crop):
                spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(0,0,-45);
                shadow.SetActive(true);
                break;

        }
        /*
        if(currentObstacle.ObstacleType == Obstacles.Rock)
        {
            spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else
        {
            spriteRenderer.gameObject.transform.rotation = Quaternion.Euler(0,0,-45);
        }

        if(currentObstacle.ObstacleType == Obstacles.Empty)
        {
            shadow.SetActive(false);
        }
        else
        {
            shadow.SetActive(true);
        }
        */

        gameObject.SetActive(true);
        return table.obstacleChances[rng].ObstacleType;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Made contact.");
        UpdateScore.Invoke(currentObstacle.PointValue);
        gameObject.SetActive(false);

    }

}