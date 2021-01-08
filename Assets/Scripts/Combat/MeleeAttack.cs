using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Component to add to objects that can melee attack.  When attack is called, shoots out a
 * raycast in many different directions, tracking each object hit.  Then calls appropriate damage taken on the target.  
 */
public class MeleeAttack : MonoBehaviour, IAttack
{
    public float AttackRange { get; set; } = 2;
    public GameObject DebugPrefab;

    private List<Quaternion> _rayCastRotation;
    private List<GameObject> _debugSpheres;

    public void Start() {

        //a list of all directions to cast rays in 
        _rayCastRotation = new List<Quaternion>();

        for (int i = -9; i < 10; i++) {
            _rayCastRotation.Add(Quaternion.AngleAxis(5 * i, Vector3.forward));
        }
    }

    public void Attack(Vector2 direction) {

        HashSet<GameObject> targetsHit = new HashSet<GameObject>();
        
        foreach (Quaternion q in _rayCastRotation) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, q * direction, AttackRange);
            if(hit) {
                Instantiate(DebugPrefab, hit.point, Quaternion.identity);//for adding debug spheres only
                targetsHit.Add(hit.collider.gameObject);
            }
        }

        foreach (GameObject target in targetsHit) {
            Health targetHealth = target.transform.parent.GetComponent<Health>();
            if (targetHealth != null) {
                targetHealth.Take1Damage();
            }
        }
    }
}
