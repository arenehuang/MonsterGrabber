//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] protected float _current_health = 5f;
    [SerializeField] protected float _max_health = 5f;

    [SerializeField] protected float _attack_cooldown = 3f;
    [SerializeField] protected float _agro_distance = 3f;

    [SerializeField] protected Transform _target_to_attack;
    [SerializeField] protected bool _pickupable = false;
    [SerializeField] protected bool _aggroed = false;

    [SerializeField] protected List<Transform> _way_points;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void DoDamage(float damage) {
        _current_health -= damage;
    }

    protected void Attack() { 
    }

    protected void Wander() { 
    }
}
