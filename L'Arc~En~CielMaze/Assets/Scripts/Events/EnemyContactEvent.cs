using UnityEngine;

public class EnemyContactEvent : CodeControl.Message
{
    public Enemy enemy;
    public Transform transform;
    public EnemyContactEvent(Enemy enemy, Transform transform)
    {
        this.enemy = enemy;
        this.transform = transform;
    }
}
