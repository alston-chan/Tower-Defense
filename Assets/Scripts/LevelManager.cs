using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private GameObject[] tilePrefabs; // A prefab for creating a single tile

    [SerializeField] private CameraMovement cameraMovement;

    [SerializeField] private Transform map;

    private List<int[]> path;

    // Dictionary that contains all the tiles in our game
    public Dictionary<Point, TileScript> Tiles { get; set; }

    private Point blueSpawn, redSpawn;
    public Point BlueSpawn { get { return blueSpawn; } }
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

        List<int[]> path = new List<int[]>
        {
            new int[] {0,0}, new int[] {1,0}, new int[] {1,1},new int[] {1,2},new int[] {1,3}, new int[] {1,4}, new int[] {1,5}, new int[] {1,6}, new int[] {2,6}, 
            new int[] {3,6}, new int[] {3,5}, new int[] {3,4}, new int[] {3,3}, new int[] {3,2}, new int[] {3,1}, new int[] {3,0}, new int[] {4,0}, new int[] {5,0},
            new int[] {5,1}, new int[] {5,2}, new int[] {5,3}, new int[] {5,4},new int[] {5,5}, new int[] {5,6}, new int[] {6,6},new int[] {7,6},new int[] {7,5}, new int[] {7,4},
            new int[] {7,3}, new int[] {7,2}, new int[] {7,1}, new int[] {7,0}, new int[] {8,0}, new int[] {9,0},new int[] {10,0},new int[] {10,1},new int[] {10,2}, new int[] {10,3},new int[] 
            {10,4}, new int[] {10,5},new int[] {10,6},new int[] {11,6}

        };

        this.path = path;

        foreach (int[] point in path)
        {
            Tiles[new Point(point[0], point[1])].IsPath = true;
        }
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


    // iterates over the tiles in the path
    public IEnumerator<TileScript> Path() 
    {
        List<int[]> path = new List<int[]>
        {
            new int[] {1,0}, new int[] {1,1},new int[] {1,2},new int[] {1,3}, new int[] {1,4}, new int[] {1,5}, new int[] {1,6}, new int[] {2,6}, 
            new int[] {3,6}, new int[] {3,5}, new int[] {3,4}, new int[] {3,3}, new int[] {3,2}, new int[] {3,1}, new int[] {3,0}, new int[] {4,0}, new int[] {5,0},
            new int[] {5,1}, new int[] {5,2}, new int[] {5,3}, new int[] {5,4},new int[] {5,5}, new int[] {5,6}, new int[] {6,6},new int[] {7,6},new int[] {7,5}, new int[] {7,4},
            new int[] {7,3}, new int[] {7,2}, new int[] {7,1}, new int[] {7,0}, new int[] {8,0}, new int[] {9,0},new int[] {10,0},new int[] {10,1},new int[] {10,2}, new int[] {10,3},new int[] 
            {10,4}, new int[] {10,5},new int[] {10,6},new int[] {11,6}

        };

        foreach (int[] point in path)
        {
            yield return Tiles[new Point(point[0], point[1])];
        }
    }

    /** Returns the string of the direction that the tower sprite should
    be facing when placed on tile X, Y based on the tiles in PATH */
    public string getDirection(int x, int y) {
        int[] up = new int[]{x, y - 1};
        int[] down = new int[]{x, y + 1};
        int[] left = new int[]{x - 1, y};
        int[] right = new int[]{x + 1, y};
        if (validTile(up[0], up[1])) {
            foreach (int[] coord in this.path) {
                if (Enumerable.SequenceEqual(coord, up)) {
                    return "up";
                }
            }
        }
        if (validTile(down[0], down[1])) {
            foreach (int[] coord in this.path) {
                if (Enumerable.SequenceEqual(coord, down)) {
                    return "down";
                }
            }
        }
        if (validTile(left[0], left[1])) {
            foreach (int[] coord in this.path) {
                if (Enumerable.SequenceEqual(coord, left)) {
                    return "left";
                }
            }
        }
        if (validTile(right[0], right[1])) {
            foreach (int[] coord in this.path) {
                if (Enumerable.SequenceEqual(coord, right)) {
                    return "right";
                }
            }
        }
        return "down";
    }

    /** Returns a bool that represents the validity of the tile
    at X, Y */
    private bool validTile(int x, int y) {
        return x >= 0 && x < 13 && y >= 0 && y < 7;
    }
}
