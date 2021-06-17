using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject tile; // A prefab for creating a single tile

    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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

    /// <summary>
    /// Creates our level
    /// </summary>

    private void CreateLevel()
    {
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height)); // Top left corner is (0, 0), Set sprites to top left pivot

        for (int y = 0; y < 5; y++) // The y positions
        {
            for (int x = 0; x < 5; x++) // The x positions
            {
                PlaceTile(x, y, worldStart);
            }
        }

    }

    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
        // Creates a new tile and makes a reference to that tile in the newTile variable
        GameObject newTile = Instantiate(tile);

        // Uses the new tile variable to change the position of the tile
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }
}
