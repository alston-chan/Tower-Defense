using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject[] tilePrefabs; // A prefab for creating a single tile

    [SerializeField] private CameraMovement cameraMovement;

    [SerializeField] private Transform map;

    // Dictionary that contains all the tiles in our game
    public Dictionary<Point, TileScript> Tiles { get; set; }

    private Point blueSpawn, redSpawn;
    [SerializeField] private GameObject bluePortalPrefab, redPortalPrefab;

    public Portal BluePortal { get; set; }
    public Portal RedPortal { get; set; }

    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    void Start()
    {
        // Executes the create level function
        CreateLevel();
    }


    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();
        
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
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }
        }

        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();

    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // Parses the tile type to an int, so that we can use it as an indexer when we create a new tile
        int tileIndex = int.Parse(tileType); // "1" = 1
        
        // Creates a new tile and makes a reference to that tile in the newTile variable
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

        // Uses the new tile variable to change the position of the tile 
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);
    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        //string[] data = bindData.text.Split(Environment.NewLine.ToCharArray());

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);

        GameObject bluePortalObject = Instantiate(bluePortalPrefab, Tiles[blueSpawn].WorldPosition, Quaternion.identity);
        BluePortal = bluePortalObject.GetComponent<Portal>();
        BluePortal.name = "BluePortal";

        redSpawn = new Point(11, 6);

        GameObject redPortalObject = Instantiate(redPortalPrefab, Tiles[redSpawn].WorldPosition, Quaternion.identity);
        RedPortal = redPortalObject.GetComponent<Portal>();
        RedPortal.name = "RedPortal";
    }
}
