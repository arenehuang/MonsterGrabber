//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {

    [SerializeField] private float _current_speed = 3f;
    [SerializeField] private Vector2 _raw_movement;
    [SerializeField] private bool _can_move = true;
    [SerializeField] private Animator _player_animator;
    [SerializeField] private SpriteRenderer _player_sprite_renderer;
    [SerializeField] private Grab _player_grab;

    public bool CanMove {
        get { return _can_move; }
        set { _can_move = value; }
    }

    // Update is called once per frame
    void FixedUpdate() {
        Walk();
    }

    private void Walk() {
        if (_can_move && _raw_movement.magnitude > 0) {
            _player_animator.SetBool("Walking", true);
            float lr_movement = _raw_movement.x;
            float fb_movement = _raw_movement.y;

            //Flip sprites as needed
            if (lr_movement > 0) {
                _player_sprite_renderer.flipX = false;

                if (_player_grab.HeldEnemy != null) {
                    _player_grab.HeldEnemy.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else if (lr_movement < 0) {
                _player_sprite_renderer.flipX = true;

                if (_player_grab.HeldEnemy != null) {
                    _player_grab.HeldEnemy.GetComponent<SpriteRenderer>().flipX = true;
                }
            }

            //Final movement
            Vector3 total_movment = new Vector3(lr_movement, 0, fb_movement);
            total_movment.Normalize();

            this.transform.Translate(total_movment * _current_speed * Time.deltaTime);
        }
        else {
            _player_animator.SetBool("Walking", false);
        }
    }

    public void OnMove(InputAction.CallbackContext context ) {
        _raw_movement = context.ReadValue<Vector2>();
    }
}
