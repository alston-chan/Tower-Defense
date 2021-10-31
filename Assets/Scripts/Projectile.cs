using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Monster target;

    private Tower parent;

    private TowerType towerType;

    [SerializeField] private int damage;

    public int getDamage()
    {
        return damage;
    }
    
    private int pierce;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (target != null && target.IsActive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, parent.ProjectileSpeed * Time.deltaTime);
        }
        else if (!target.IsActive)
        {
            GameManager.Instance.Pool.ReleaseObject(gameObject);
        }
    }


    public void Initialize(Tower parent, int towerDamage)
    {
        this.target = parent.Target;
        this.parent = parent;
        this.towerType = parent.TypeOfTower;
        this.damage = towerDamage;
    }
}
