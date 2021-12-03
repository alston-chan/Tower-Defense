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

    public void ShowInfo(string type) {
        string tooltip = string.Empty;
        switch (type) {
            case "Chick":
                Tower t = towerPrefab.GetComponentInChildren<Tower>();
                tooltip = string.Format("<b>Chick</b> \nPrice: {0} \nDamage: {1} \nAttack Speed: {2} \nCamo Detection: {3} \nDescription: {4}", 
                    t.GetStats().Price, t.GetStats().Damage, t.GetStats().AttackCooldown, t.GetStats().CanSeeCamo, "Basic Tower");
                break;
            case "Snowball":
                Tower s = towerPrefab.GetComponentInChildren<Tower>();
                tooltip = string.Format("<b>Snowball</b> \nPrice: {0} \nDamage: {1} \nAttack Speed: {2} \nCamo Detection: {3} \nDescription: {4}", 
                    s.GetStats().Price, s.GetStats().Damage, s.GetStats().AttackCooldown, s.GetStats().CanSeeCamo, "Snowball Tower");
                break;
            case "Wizard":
                Tower w = towerPrefab.GetComponentInChildren<Tower>();
                tooltip = string.Format("<b>Wizard</b> \nPrice: {0} \nDamage: {1} \nAttack Speed: {2} \nCamo Detection: {3} \nDescription: {4}", 
                    w.GetStats().Price, w.GetStats().Damage, w.GetStats().AttackCooldown, w.GetStats().CanSeeCamo, "Wizard Tower");
                break;
        }

        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
