using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum TowerType {BASIC, NOTSOBASIC, WIZARD}

public abstract class Tower : MonoBehaviour
{
    [SerializeField] private string projectileType;

    public TileScript tile;
    public SpriteRenderer spriteRenderer;

    private Monster target;
    public Monster Target { get { return target;  } }

    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;

    public TowerType TypeOfTower { get; protected set; }

    [SerializeField] private int damage;
    public float accuracy;
    private System.Random rand = new System.Random();
    [SerializeField] private bool canSeeCamo;
    [SerializeField] private int upgradeDamage;
    [SerializeField] private float upgradeAccuracy;
    public int price;

    [SerializeField] private float upgradeAttackCooldown;

    [SerializeField] private int upgradePrice;

    [SerializeField] private int upgradeMax = 3;

    private int upgradeCounter = 0;

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get { return projectileSpeed; } }

    // Start is called before the first frame update
    public virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Upgrade(int path) {
      if (upgradeCounter < upgradeMax && PlayerStats.Fish >= upgradePrice) {
        if (path == 0) {
            damage += upgradeDamage;
            PlayerStats.Fish -= upgradePrice;
        } else if (path == 1) {
            attackCooldown -= upgradeAttackCooldown;
            PlayerStats.Fish -= upgradePrice;
        } else {
            if (canSeeCamo != true) {
                canSeeCamo = true;
                PlayerStats.Fish -= upgradePrice;            
            }
        }
        upgradeCounter += 1;          
      }
        // For updating the art at the final upgrade
        SpriteRenderer parentSpR = transform.parent.GetComponent<SpriteRenderer>();
        Sprite parentSp = transform.parent.GetComponent<SpriteRenderer>().sprite;
        if (upgradeCounter == upgradeMax) {
            string ogSpriteName = parentSpR.name;
            string spriteBaseName = ogSpriteName.Substring(0, ogSpriteName.Length - 8);
            Debug.Log(parentSp.name);
            // string dir = ogSpriteName.Substring(0, ogSpriteName.Length - 7);
            string dir = parentSp.name.Substring(parentSp.name.Length - 1);
            // dir = dir.Substring(dir.Length - 1);
            Debug.Log(dir);
            if (spriteBaseName == "Base") {
                if (dir == "U") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/BGU");
                } else if (dir == "D") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/BGD");
                } else if (dir == "L") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/BGL");
                } else {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/BGR");
                }
            } else if (spriteBaseName == "SBL") {
                if (dir == "U") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/RLU");
                } else if (dir == "D") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/RLD");
                } else if (dir == "L") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/RLL");
                } else {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/RLR");
                }
            } else if (spriteBaseName == "Wizard") {
                if (dir == "U") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/WUpgU");
                } else if (dir == "D") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/WUpgD");
                } else if (dir == "L") {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/WUpgL");
                } else {
                    transform.parent.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Towers/WUpgR");
                }
            }
        }
    }

    public void Sell() {
        double sell_price = .7 * price;
        PlayerStats.Fish += (int) sell_price;
        Destroy(this.gameObject);
        Destroy(tile.tower);
        tile.myTower = null;
        tile.IsEmpty = true;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if (target != null && target.IsActive)
        {
            if (canAttack)
            {
                Shoot();
                canAttack = false;
            }
        }
    }

    private void Shoot()
    {
        // This is the current implementation of accuracy but we could change projectile class later
        float randomAccuracy = (float) rand.NextDouble();
        if (randomAccuracy > accuracy) {
          return;
        }

        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this, damage);
    }

    public void Select()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (!monster.getCamo() || (canSeeCamo)) 
            {
                target = monster;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();
            if (monster.getID() == target.getID())
            {
                target = null;
            }
        }
    }

    public class Stats {
        public int Price { get; set; }
        public int Damage { get; set; }
        public float AttackCooldown { get; set; }
        public bool CanSeeCamo { get; set; }
        public int UpgradeDamage { get; set; }
        public float UpgradeAttackCooldown { get; set; }
        public bool UpgradeCamo { get; set; }
        public Stats(int price, int dmg, float ac, bool csc, int ud, float uac) {
            Price = price;
            Damage = dmg;
            AttackCooldown = ac;
            CanSeeCamo = csc;
            UpgradeDamage = ud;
            UpgradeAttackCooldown = uac;
        }
    }

    public Stats GetStats() {
        return new Stats(price, damage, attackCooldown, canSeeCamo, upgradeDamage, upgradeAttackCooldown);
    }
}
