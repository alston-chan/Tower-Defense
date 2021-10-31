using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{

    TowerUpgrade dmg = new TowerUpgrade(50, 10, 0, 0);
    TowerUpgrade slow = new TowerUpgrade(50, 0, 5, 0);

    public override void Start()
    {
        TypeOfTower = TowerType.BASIC;
        Upgrades = new TowerUpgrade[] 
            {
                dmg,
                slow,
            };
        base.Start();
    }
}
