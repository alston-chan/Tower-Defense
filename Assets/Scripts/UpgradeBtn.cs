using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBtn : MonoBehaviour
{
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
            case "Damage":
                tooltip = type;
                break;
            case "AttackSpeed":
                tooltip = type;
                break;
            case "Camo":
                tooltip = type;
                break;
        }

        GameManager.Instance.SetTooltipText(tooltip);
        GameManager.Instance.ShowStats();
    }
}
