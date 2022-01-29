//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour {

    [SerializeField] private bool _can_attack = true;
    [SerializeField] private bool _is_attacking = false;
    [SerializeField] private Transform _player_transform;
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

    [SerializeField] private Animator _player_animator;
    [SerializeField] private SpriteRenderer _player_sprite_renderer;

    [SerializeField] private Grab _player_grab;

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
        if (context.performed && !_is_attacking && _player_grab.HeldEnemy == null) {
            _player_animator.SetBool("Attacking", true);

            _player_movement.CanMove = false;
            _is_attacking = true;

            _enemy_to_attack = _player_targeter.targeted_enemy;

            _attack_direction = new Vector3();

            if (_enemy_to_attack != null) {
                _attack_direction = (_enemy_to_attack.transform.position - _player_transform.position).normalized;
            }
            else {
                //Debug.Log("Enemy to attack is null");
                _attack_direction = new Vector3(_last_raw_movement.x, 0, _last_raw_movement.y).normalized;
            }

            if (_attack_direction.x > 0) {
                _player_sprite_renderer.flipX = false;
            }
            else if (_attack_direction.x < 0) {
                _player_sprite_renderer.flipX = true;
            }
        }
        else if (context.performed && !_is_attacking && _player_grab.HeldEnemy != null) {
            _player_grab.HeldEnemy.Use();
        }
        
    }

    private void AttackMovement() {
        if (_is_attacking) {
            _player_transform.Translate(_attack_direction * Time.deltaTime * _attack_speed);
        }
    }

    public void FinishAttack() {
        _is_attacking = false;
        _player_movement.CanMove = true;
        _player_animator.SetBool("Attacking", false);
    }

    private void OnCollisionEnter(Collision collision) {

        if (_is_attacking) {
            if (collision.gameObject.tag == "Enemy") {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(_current_damage);

                _player_targeter.CheckTargetableEnemies();
            }

            //Apply knockback
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null) {
                Vector3 direction = collision.transform.position - _player_transform.position;
                rb.AddForce(direction.normalized * _knock_back_force, ForceMode.Impulse);
            }
        }
    }
}
