using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int armor;
    [SerializeField] private int dodge;
    [SerializeField] private bool isCamo;
    [SerializeField] private float speed;

    // private GameObject[] path;

    private IEnumerator<TileScript> path;
    public Point GridPosition { get; set; }

    [SerializeField] private Vector3 destination;

    public bool IsActive { get; set; }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (IsActive)
        {
    /**             
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                Release();
            }
    */      
            if (transform.position == LevelManager.Instance.RedPortal.transform.position)
            { 
                Release();
            }

            else if (transform.position == destination) 
            {
                if (path.MoveNext()) 
                {
                    destination = path.Current.transform.position;
                }
                else 
                {
                    destination = LevelManager.Instance.RedPortal.transform.position;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        }
    }

    public void Spawn()
    {
        transform.position = LevelManager.Instance.BluePortal.transform.position;

        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));

        destination = LevelManager.Instance.RedPortal.transform.position;

        path = LevelManager.Instance.Path();
        
        if (path.MoveNext())
        {
            destination = path.Current.transform.position;
        }

    }

    public IEnumerator Scale(Vector3 from, Vector3 to)
    {
        IsActive = false;

        float progress = 0;

        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;

        IsActive = true; 
    }

    private void Release()
    {
        IsActive = false;
        GridPosition = LevelManager.Instance.BlueSpawn;

        GameManager.Instance.Pool.ReleaseObject(gameObject);
    }
}
