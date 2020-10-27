using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCannon : MonoBehaviour
{
    private Rigidbody rb = null;
    [SerializeField] private int health = 1; 
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Damage_cannon(int dmg, Vector3 damagePoint)
    {
        health -= dmg;
        if (health <= 0)
        {
            rb.AddForce(damagePoint * (500));
            rb.AddTorque(Vector3.one * 100);
            Destroy(gameObject, 8);
        }
    }

}
