using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSoBasicTower : Tower
{

    public override void Start()
    {
        TypeOfTower = TowerType.NOTSOBASIC;
        base.Start();
    }
}
