using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    public ObjectPool Pool { get; set; }

    private Tower selectedTower;

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleEscape();
    }

    public void PickTower(TowerBtn towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover.Instance.Activate(towerBtn.Sprite);
    }

    public bool BuyTower()
    {
        int tower_cost = 100;  // pass in tower cost once implemented
        if (PlayerStats.Fish >= tower_cost)  // do nothing if can't afford tower
        {
            PlayerStats.Fish -= tower_cost;  // TESTING: every time a tower is placed, 100 fish (currency) will be subtracted.
            return true;
        } else return false;
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower != null)
            selectedTower.Select();

        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
    }

    public void UpgradeDamage()
    {
        selectedTower.Upgrade(0);
    }

    public void UpgradeAttackCooldown() 
    {
        selectedTower.Upgrade(1);
    }

    public void UpgradeCamo() 
    {
        selectedTower.Upgrade(2);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        int monsterIndex = Random.Range(0, 4);

        string type = string.Empty;

        switch (monsterIndex)
        {
            case 0:
                type = "Basic";
                break;
            case 1:
                type = "Runner";
                break;
            case 2:
                type = "Tanker";
                break;
            case 3:
                type = "Sneak";
                break;
        }

        Monster monster = Pool.GetObject(type).GetComponent<Monster>();
        monster.Spawn();
        
        yield return new WaitForSeconds(2.5f);
    }
}
