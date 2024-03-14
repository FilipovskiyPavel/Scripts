using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun_bullet_bonus : MonoBehaviour
{
    [SerializeField] private Transform _trans;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask canHitThis;
    [SerializeField] private float distance;
    [SerializeField] private GameObject destroyEffect;
    private GameObject oldEnemy;
    // Start is called before the first frame update
    void Start()
    {
        _trans = GetComponent<Transform>();
        Invoke(nameof(DestroyBullet), lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, canHitThis);
        if (hitInfo.collider != null) 
        {
            
            //SoundManager.Instance.Stop("Bullet_Pistol");
            if(hitInfo.collider.CompareTag("Enemy"))
            {
                GameObject currentEnemy = hitInfo.transform.gameObject;
                if(currentEnemy != oldEnemy)
                {
                    currentEnemy.GetComponent<EnemyHealth>().TakeDamage(damage);
                    oldEnemy = currentEnemy;
                    Instantiate(destroyEffect, transform.position, transform.rotation);
                }
            }

            else
            {
                DestroyBullet();
                Instantiate(destroyEffect, transform.position, transform.rotation);

            }
        }
        _trans.Translate(speed * Time.deltaTime * Vector3.right);
    }
    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
