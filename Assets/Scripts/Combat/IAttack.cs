using UnityEngine;

public interface IAttack
{
    float AttackRange { get; set; }
    void Attack(Vector2 dir);
}
