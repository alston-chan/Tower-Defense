using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private string projectileType;
    
    private SpriteRenderer spriteRenderer;

    private Monster target;
    public Monster Target { get { return target;  } }

    private Queue<Monster> monsters = new Queue<Monster>();

    private bool canAttack = true;
    private float attackTimer;
    [SerializeField] private float attackCooldown;

    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get { return projectileSpeed; } }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
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
        
        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
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
        Projectile projectile = GameManager.Instance.Pool.GetObject(projectileType).GetComponent<Projectile>();

        projectile.transform.position = transform.position;

        projectile.Initialize(this);
    }

    public void Select()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            monsters.Enqueue(other.GetComponent<Monster>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            target = null;
        }
    }
}
