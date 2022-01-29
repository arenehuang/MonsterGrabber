//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour {

    [SerializeField] private bool _can_attack = true;
    [SerializeField] private bool _is_attacking = false;
    [SerializeField] private Movement _player_movement;
    [SerializeField] private Targeter _player_targeter;
    [SerializeField] private Rigidbody _player_rigidbody;

    [SerializeField] private Vector2 _last_raw_movement;
    [SerializeField] private float _attack_speed = 4f;

    [SerializeField] private float _current_damage = 1f;
    [SerializeField] private float _standard_damage = 1f;

    [SerializeField] private Enemy _enemy_to_attack;
    [SerializeField] private float _knock_back_force = 3f;

    [SerializeField] private Vector3 _attack_direction;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        AttackMovement();
    }

    public void SetMovement(InputAction.CallbackContext context) {
        if (context.ReadValue<Vector2>().magnitude > 0) {
            _last_raw_movement = context.ReadValue<Vector2>();
        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.performed && !_is_attacking) {
            _player_movement.CanMove = false;
            _is_attacking = true;

            _enemy_to_attack = _player_targeter.targeted_enemy;

            //If an enemy is targeted add force in their direction. Otherwise, addforce in direction of movement.
            //Add force in direction of targeted
            _attack_direction = new Vector3();
            if (_enemy_to_attack != null) {
                //Vector3 raw_direction = (this.transform.position - _enemy_to_attack.transform.position).normalized;
                _attack_direction = (_enemy_to_attack.transform.position - this.transform.position).normalized;
            }
            else {
                Debug.Log("Enemy to attack was null");
                _attack_direction = new Vector3(_last_raw_movement.x, 0, _last_raw_movement.y).normalized;
            }
        }
        
    }

    private void AttackMovement() {
        if (_is_attacking) {
            transform.Translate(_attack_direction * Time.deltaTime * _attack_speed);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && _is_attacking) {
            collision.gameObject.GetComponent<Enemy>().DoDamage(_current_damage);
            collision.rigidbody.AddExplosionForce(_knock_back_force, this.transform.position, 1.1f);

            if (collision.gameObject.GetComponent<Enemy>() == _enemy_to_attack) {
            }
        }
    }
}
