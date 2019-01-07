using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BasePickup
{
    private Enemy enemy;
    public void Initialize(Enemy enemy)
    {
        this.enemy = enemy;
        transform.localScale *= 2;
        GetComponent<BoxCollider>().size = new Vector3(3f, 3, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DispatchEnemyContactEvent();
        }
    }

    private void DispatchEnemyContactEvent()
    {
        CodeControl.Message.Send(new EnemyContactEvent(enemy, transform));
    }
}
