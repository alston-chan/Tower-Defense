using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTower : Tower
{

    public override void Start()
    {
        TypeOfTower = TowerType.WIZARD;
        base.Start();
    }
}
