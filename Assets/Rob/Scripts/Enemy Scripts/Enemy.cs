//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] protected float _current_health = 5f;
    [SerializeField] protected float _max_health = 5f;
    [SerializeField] protected float _pickupable_health = 1f;

    [SerializeField] protected float _pickupable_timer = 3f;

    [SerializeField] protected bool _did_pickupable_check = false;

    [SerializeField] protected float _attack_cooldown = 3f;
    [SerializeField] protected float _agro_distance = 3f;

    [SerializeField] protected Transform _target_to_attack;
    [SerializeField] protected bool _pickupable = false;
    [SerializeField] protected bool _aggroed = false;

    [SerializeField] protected List<Transform> _way_points;

    [SerializeField] protected SpriteRenderer _enemy_sprite_renderer;
    [SerializeField] protected Color _default_color;
    [SerializeField] protected Color _flash_color;
    [SerializeField] protected float _flash_timer = 0.5f;

    public bool PickUpAble {
        get { return _pickupable; }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        CheckHealth();
    }

    public void DoDamage(float damage) {
        _current_health -= damage;
        _did_pickupable_check = false;
    }

    protected void Attack() { 
    }

    protected void Wander() { 
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

    public void Use() { 
    }
}
