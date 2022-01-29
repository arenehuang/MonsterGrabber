//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {

    [SerializeField] private float _current_speed = 3f;
    [SerializeField] private Vector2 _raw_movement;
    [SerializeField] private bool _can_move = true;

    public bool CanMove {
        get { return _can_move; }
        set { _can_move = value; }
    }

    // Update is called once per frame
    void Update() {
        Walk();
    }

    private void Walk() {
        if (_can_move) {
            float lr_movement = _raw_movement.x;
            float fb_movement = _raw_movement.y;

            Vector3 total_movment = new Vector3(lr_movement, 0, fb_movement);
            total_movment.Normalize();

            this.transform.Translate(total_movment * _current_speed * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context ) {
        _raw_movement = context.ReadValue<Vector2>();
    }
}