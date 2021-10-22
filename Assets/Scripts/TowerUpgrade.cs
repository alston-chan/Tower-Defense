using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade
{

    public int Price { get; private set; }

    public int Damage { get; private set; }

    public float SlowingFactor { get; private set; }

    public float AttackSpeed { get; private set; }


    public TowerUpgrade(int price, int damage, float slowingFactor, float attackSpeed)
    {
        this.Damage = damage;
        this.Price = price;
        this.SlowingFactor = slowingFactor;
        this.AttackSpeed = attackSpeed;
    }
}
