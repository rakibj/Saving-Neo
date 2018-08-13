using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectScript : MonoBehaviour {

    ObjectPooler objectPooler;
    public GameObject enemyDeathEffect;

    void Start () {
        objectPooler = ObjectPooler.instance;
	}

    public void ShowEnemyDeathEffect(Transform effectTransform)
    {
        Debug.Log("Showing Effect");
        GameObject effectgo = objectPooler.SpawnFromPool("enemydestroy", effectTransform.position, Quaternion.identity);
        effectgo.GetComponent<ParticleSystem>().Play();
    }

    public void ShowEnemyDeathEffect1(Transform effectTransform)
    {
        Instantiate(enemyDeathEffect, effectTransform.position, Quaternion.identity);
    }
}
