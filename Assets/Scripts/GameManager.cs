using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    public UpgradeBtn HoveredBtn { get; set; }

    public ObjectPool Pool { get; set; }

    private Tower selectedTower;

    [SerializeField]
    public GameObject statsPanel;

    [SerializeField]
    private Text statsText;

    [SerializeField]
    private GameObject upgradePanel;

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
        int tower_cost = ClickedBtn.towerPrefab.GetComponentInChildren<Tower>().price;
        if (PlayerStats.Fish >= tower_cost)
        {
            PlayerStats.Fish -= tower_cost;  // TESTING: every time a tower is placed, 100 fish (currency) will be subtracted.
            return true;
        } else return false;
    }

    public Tower getSelectedTower() {
        return selectedTower;
    }

    public void SelectTower(Tower tower)  // click on tower after it is already placed
    {
        if (selectedTower != null)
            selectedTower.Select();

        selectedTower = tower;
        selectedTower.Select();
        string tooltip = string.Empty;
        tooltip = string.Format("Price: {0} \nDamage: {1} \nAttack Speed: {2} \nCamo Detection: {3} \nDescription: {4}", 
                    selectedTower.GetStats().Price, selectedTower.GetStats().Damage, selectedTower.GetStats().AttackCooldown, selectedTower.GetStats().CanSeeCamo, "");
        GameManager.Instance.SetTooltipText(tooltip);
        statsPanel.SetActive(true);   
        upgradePanel.SetActive(true);
        
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
        statsPanel.SetActive(false);
        upgradePanel.SetActive(false); 
    }

    public void UpgradeToolTips(string type) {
        if (selectedTower != null) {
            string tooltip = string.Empty;
            switch (type) {
                case "Damage":
                    tooltip = string.Format("Damage Upgrade: {0}", selectedTower.GetStats().UpgradeDamage);
                    break;
                case "AttackSpeed":
                    tooltip = string.Format("Attack Cooldown Upgrade: {0}", selectedTower.GetStats().UpgradeAttackCooldown);
                    break;
                case "Camo":
                    tooltip = "Allows tower to see camo";
                    break;
            }

            GameManager.Instance.SetTooltipText(tooltip); 
        }
        upgradePanel.SetActive(false);
    }

    public void UpgradeDamage()
    {
        selectedTower.Upgrade(0);
        SelectTower(selectedTower);
        GameManager.Instance.ShowStats();
    }

    public void UpgradeAttackCooldown() 
    {
        selectedTower.Upgrade(1);
    }

    public void UpgradeCamo() 
    {
        selectedTower.Upgrade(2);
    }

    public void SellTower()
    {
        selectedTower.Sell();
        statsPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void ShowStats() {
        statsPanel.SetActive(!statsPanel.activeSelf);
    }

    public void SetTooltipText(string txt, bool red=false) {
        statsText.text = txt;
        if (red)
            statsText.color = new Color(1, 0, 0, 1);
        else
            statsText.color = new Color(0, 0, 0, 1);
    }

    // public void StartWave()
    // {
    //     StartCoroutine(SpawnWave());
    // }

    // private IEnumerator SpawnWave()
    // {
    //     int monsterIndex = Random.Range(0, 4);

    //     string type = string.Empty;

    //     switch (monsterIndex)
    //     {
    //         case 0:
    //             type = "Basic";
    //             break;
    //         case 1:
    //             type = "Runner";
    //             break;
    //         case 2:
    //             type = "Tanker";
    //             break;
    //         case 3:
    //             type = "Sneak";
    //             break;
    //     }

    //     Monster monster = Pool.GetObject(type).GetComponent<Monster>();
    //     monster.Spawn();
        
    //     yield return new WaitForSeconds(2.5f);
    // }
}
