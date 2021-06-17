using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public Vector2 WorldPosition
    {
        get
        {
            return GetComponent<SpriteRenderer>().bounds.center;
        }
    }

    public void Setup(Point gridPos, Vector3 worldPos)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;

        LevelManager.Instance.Tiles.Add(gridPos, this);
    }
}
