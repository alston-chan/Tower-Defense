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
                tooltip = string.Format("<b>Chick</b> \nDamage: {0} \nAttack Speed: {1} \nCamo Detection: {2} \nDescription: {3}", 
                    t.GetStats().Damage, t.GetStats().AttackCooldown, t.GetStats().CanSeeCamo, "Basic Tower");
                break;
            case "Snowball":
                tooltip = string.Format("<b>Snowball</b>");
                break;
            case "Wizard":
                tooltip = string.Format("<b>Wizard</b>");
                break;
        }

        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
