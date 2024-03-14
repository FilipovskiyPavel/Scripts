using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus_gun : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float startCooldown;
    [SerializeField] private int bulletAmount;
    [SerializeField] private int damage;
    [SerializeField] private string SFX;
    private const float radius = 0.2F;     
    private float cooldown;
    private Collider2D _coll;
    private GameObject enemyList;
    void Start()
    {
        _coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (cooldown <= 0)
        {
            GetEnemiesList();
            if(enemyList.GetComponent<OnCamera>().enemies.Count != 0)
            {
                StartCoroutine(ActiveColl());
                cooldown = startCooldown;
            }
        }
        else 
        {
            cooldown -= Time.deltaTime;
        }
    }
    private void GetEnemiesList()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Room"));
        if(hitColliders != null)
        {
           enemyList = hitColliders.gameObject;
        }
    }
    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;
        for (int i = 0; i <=_numberOfProjectiles -1; i++)
        {
            float projectileDirXPosition = spawnPoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = spawnPoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;
            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = (projectileVector - spawnPoint.transform.position).normalized;
            GameObject tmpObj = Instantiate(bullet, new Vector3(projectileDirXPosition, projectileDirYPosition), Quaternion.Euler(projectileMoveDirection));
            tmpObj.transform.right = tmpObj.transform.position - spawnPoint.transform.position;
            angle += angleStep;
        }
    }
    IEnumerator ActiveColl()
    {
        SoundManager.Instance.Play(SFX);
        SpawnProjectile(bulletAmount);
        _coll.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _coll.enabled = false;
        yield return new WaitForSeconds(0.4f);
        SpawnProjectile(bulletAmount);
        _coll.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _coll.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy_hitZone_FromTop"))
        {
            other.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
