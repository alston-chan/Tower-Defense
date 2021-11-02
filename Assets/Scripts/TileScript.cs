using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public bool IsEmpty { get; private set; }

    private Tower myTower;

    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);
    }

    private void OnMouseOver()
    {
        // Check to see if mouse is clicking a button or if player has clicked buy button
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty)
            {
                ColorTile(emptyColor);

                if (Input.GetMouseButtonDown(0))
                    PlaceTower();
            } 
            else
            {
                ColorTile(fullColor);
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetMouseButtonDown(0))
        {
            if (myTower != null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    private void PlaceTower()
    {
        if (GameManager.Instance.BuyTower())  // can only place tower if you can buy it first
        {
            // Debug.Log(GridPosition.X + ", " + GridPosition.Y);
            string dir = LevelManager.Instance.getDirection(GridPosition.X, GridPosition.Y);
            string ogSpriteName = GameManager.Instance.ClickedBtn.TowerPrefab.GetComponent<SpriteRenderer>().name;
            string spriteBaseName = ogSpriteName.Substring(0, ogSpriteName.Length - 1);
            GameObject t = GameManager.Instance.ClickedBtn.TowerPrefab; 
            Debug.Log(spriteBaseName + ", " + dir);
            Debug.Log("Before: " + t);
            if (spriteBaseName == "Base") {
                if (dir == "up") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/BaseU") as GameObject;
                } else if (dir == "down") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/BaseD") as GameObject;
                } else if (dir == "left") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/BaseL") as GameObject;
                } else {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/BaseR") as GameObject;
                }
            } else if (spriteBaseName == "SBL") {
                if (dir == "up") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/SBLU") as GameObject;
                } else if (dir == "down") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/SBLD") as GameObject;
                } else if (dir == "left") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/SBLL") as GameObject;
                } else {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/SBLR") as GameObject;
                }
            } else if (spriteBaseName == "Wizard") {
                if (dir == "up") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/WizardU") as GameObject;
                } else if (dir == "down") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/WizardD") as GameObject;
                } else if (dir == "left") {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/WizardL") as GameObject;
                } else {
                    t = Resources.Load("Assets/Resources/Prefabs/Towers/WizardR") as GameObject;
                }
            }
            Debug.Log("After: " + t);
            GameObject tower = Instantiate(t, WorldPosition, Quaternion.identity);
            //tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

            tower.transform.SetParent(transform);

            this.myTower = tower.GetComponentInChildren<Tower>();

            IsEmpty = false;

            ColorTile(Color.white);

            Hover.Instance.Deactivate();
        }
        

        
    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}
