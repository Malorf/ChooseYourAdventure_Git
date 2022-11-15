using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellColision : MonoBehaviour
{
    public float spellDamage;
    public float timeDestroy;
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag =="EnemyWolf")
        {
            col.gameObject.GetComponent<EnemyAiWolf>().ApplyDammage(spellDamage);
        }
        if (col.gameObject.tag == "EnemySpider")
        {
            col.gameObject.GetComponent<EnemySpider>().ApplyDammage(spellDamage);
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyAi>().ApplyDammage(spellDamage);
        }
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject, 0.1f);
        }
    }

}
