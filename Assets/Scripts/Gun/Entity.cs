using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float StartingHealth;
    private float health;
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            Debug.Log(health);

            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Health = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
