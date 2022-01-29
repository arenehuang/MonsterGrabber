//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour {

    [SerializeField] private Targeter _player_targeter;
    [SerializeField] private Enemy _held_enemy;
    [SerializeField] private Transform _hold_point;

    public Enemy HeldEnemy { 
        get { return _held_enemy; }
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void OnGrab(InputAction.CallbackContext context) {
        if (_player_targeter.targeted_enemy != null && _held_enemy == null) {
            if (_player_targeter.targeted_enemy.PickUpAble) {
                _held_enemy = _player_targeter.targeted_enemy;
                _held_enemy.transform.parent = _hold_point;
                _held_enemy.GetComponent<Collider>().enabled = false;
                //_held_enemy.GetComponent<Rigidbody>().
            }
        }
        else if (_held_enemy != null) {
            //Drop enemy
            _held_enemy.transform.parent = null;
            _held_enemy.GetComponent<Collider>().enabled = true;

            _held_enemy = null;
        }
    }
}
