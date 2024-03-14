using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artas_Ability_Logic : MonoBehaviour
{
    [SerializeField] private Cameo_Ability_List _list;
    [SerializeField] private Cameo_Ability_Stat_Holder _stat;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject iconHolder;
    [SerializeField] private GameObject Frostmorn;
    private List<Collider2D> enemyList;
    private bool abilityDone;
    void Awake()
    {
        _stat = GetComponent<Cameo_Ability_Stat_Holder>();
        _stat.keyUse = KeyCode.None;
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Start is called before the first frame update
    void Start()
    {
        _list = GameObject.FindGameObjectWithTag("Cameo_Ability_List").GetComponent<Cameo_Ability_List>();
        iconHolder = GameObject.FindWithTag("Icons_Holder_Cameo");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(_stat.keyUse) && !abilityDone && _stat.keyUse != KeyCode.None)
        {
            GetEnemiesList();
            if(enemyList.Count==0)
            {
                Debug.Log("break");
            }
            else 
            {
                abilityDone = true; 
                Instantiate(Frostmorn, player.transform.position, Quaternion.identity);
                _list.Remove(_stat.so_Cameo_Ability.myIndex);
                _stat.icon.GetComponent<Animator>().SetInteger("State",1);
                iconHolder.GetComponent<Cameo_Found_List>().Remove();
            }
        }
    }
    public void GetEnemiesList()
    {
        Collider2D hitColliders = Physics2D.OverlapCircle(player.transform.position, 1f, LayerMask.GetMask("Room"));
        if(hitColliders != null)
        {
           enemyList = hitColliders.gameObject.GetComponent<OnCamera>().enemies;
           //enemyListGet = true;
        }
    }
}
