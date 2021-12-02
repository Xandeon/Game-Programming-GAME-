// author - Samuel Adetunji
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanic : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    [SerializeField] public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
