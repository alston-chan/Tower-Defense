using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBtn : MonoBehaviour
{
    [SerializeField] public GameObject towerPrefab;

    [SerializeField] private Sprite sprite;

    public GameObject TowerPrefab { get => towerPrefab; }
    public Sprite Sprite { get => sprite; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
