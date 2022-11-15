using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMaladie : MonoBehaviour
{
    public float maladieTotal;
    public float timeDestroy;
    void Start()
    {
        Destroy(gameObject, timeDestroy);

}

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyAi>().ApplyDOTDammage(maladieTotal);
        }
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
