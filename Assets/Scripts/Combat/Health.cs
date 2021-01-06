using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHP;
    private int _currentHP;

    private void Start() {
        _currentHP = MaxHP;
    }
    public void Take1Damage() {
        _currentHP--;
        Debug.Log("Taking damage! Only " + _currentHP + " HP left monkaS");
        if (_currentHP == 0) {
            Debug.Log("Died :((");
            gameObject.SetActive(false);
        }
    }
}
