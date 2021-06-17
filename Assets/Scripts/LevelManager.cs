using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs; // A prefab for creating a single tile

    [SerializeField] private CameraMovement cameraMovement;

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update - Use this for initialization
    void Start()
    {
        // Executes the create level function
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void CreateLevel()
    {
        string[] mapData = ReadLevelText();

        // Calculate map X and Y size
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        // Calculates the world start point, which is at the top left corner of the screen (0, 0), Set sprites to top left pivot
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); 

        for (int y = 0; y < mapY; y++) // The y positions
        {
            char[] newTiles = mapData[y].ToCharArray();
            
            for (int x = 0; x < mapX; x++) // The x positions
            {
                maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

    }

    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // Parses the tile type to an int, so that we can use it as an indexer when we create a new tile
        int tileIndex = int.Parse(tileType); // "1" = 1
        
        // Creates a new tile and makes a reference to that tile in the newTile variable
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);

        // Uses the new tile variable to change the position of the tile
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);

        return newTile.transform.position;
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        //string[] data = bindData.text.Split(Environment.NewLine.ToCharArray());

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }
}
