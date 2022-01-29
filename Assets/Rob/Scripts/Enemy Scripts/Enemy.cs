//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] protected float _current_health = 5f;
    [SerializeField] protected float _max_health = 5f;
    [SerializeField] protected float _pickupable_health = 1f;

    [SerializeField] protected float _current_speed = 3f;
    [SerializeField] protected float _walk_speed = 3f;
    [SerializeField] protected float _chase_speed = 3f;

    [SerializeField] protected float _pickupable_timer = 3f;

    [SerializeField] protected bool _did_pickupable_check = false;

    [SerializeField] protected float _attack_cooldown = 3f;
    [SerializeField] protected float _agro_distance = 3f;

    [SerializeField] protected Transform _target_to_attack;
    [SerializeField] protected bool _pickupable = false;
    [SerializeField] protected bool _aggroed = false;

    [SerializeField] protected Transform _area_waypoint;
    [SerializeField] protected float _distance_from_waypoint;
    [SerializeField] protected float _max_distance_from_waypoint;


    [SerializeField] protected SpriteRenderer _enemy_sprite_renderer;
    [SerializeField] protected Color _default_color;
    [SerializeField] protected Color _flash_color;
    [SerializeField] protected float _flash_timer = 0.5f;

    [SerializeField] protected Transform _player_transform;
    [SerializeField] protected bool _is_walking = true;
    [SerializeField] protected float _walk_time = 2f;
    [SerializeField] protected float _walk_wait_timer = 2f;

    [SerializeField] protected float _current_distance_from_player = 1000f;
    [SerializeField] protected float _deaggro_distance = 5f;


    public bool PickUpAble {
        get { return _pickupable; }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        CheckHealth();
    }

    public void TakeDamage(float damage) {
        _current_health -= damage;
        _did_pickupable_check = false;
    }

    protected virtual void Attack() { 
    }

    protected virtual void Wander() {

    }

    protected virtual void Follow() { 
    }

    protected void CheckHealth() {
        if (_current_health <= 0) {
            Destroy(this.gameObject);
        }

        if (_current_health <= _pickupable_health && !_did_pickupable_check) {
            _did_pickupable_check = true;
            StartCoroutine(PickUpableCooldown());
        }
    }

    protected IEnumerator PickUpableCooldown() {
        _pickupable = true;
        StartCoroutine(DoFlash());
        yield return new WaitForSeconds(_pickupable_timer);
        _pickupable = false;
        StopCoroutine(DoFlash());
    }

    protected IEnumerator DoFlash() {
        if (_pickupable) {
            _enemy_sprite_renderer.color = _flash_color;
            yield return new WaitForSeconds(_flash_timer);
            _enemy_sprite_renderer.color = _default_color;
            yield return new WaitForSeconds(_flash_timer);
            StartCoroutine(DoFlash());
        }
        else { 
        yield return new WaitForSeconds(0f);
        }
    }

    public virtual void Use() {
        Debug.Log("Being used");
    }

    protected virtual void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            _player_transform = other.transform;
            _aggroed = true;
        }
    }
}
