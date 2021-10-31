using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : Tower
{
    public override void Start()
    {
        TypeOfTower = TowerType.BASIC;
        base.Start();
    }
}
