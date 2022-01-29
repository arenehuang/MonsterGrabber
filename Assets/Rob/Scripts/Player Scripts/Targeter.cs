//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Targeter : MonoBehaviour {

    public List<Enemy> targetable_enemies;
    public Enemy targeted_enemy;
    public Enemy enemy_to_attack;
    public Transform probe_transform;
    [SerializeField] private Vector2 _raw_movement;

    private void Awake() {

    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        CheckTargetableEnemies();
    }

    private void CheckTargetableEnemies() {
        if (targetable_enemies.Count > 1 && targeted_enemy != null) {
            foreach (Enemy e in targetable_enemies) {

                if (e != targeted_enemy) {
                    float others_distance = Vector3.Distance(probe_transform.position, e.transform.position);
                    float current_target_distance = Vector3.Distance(probe_transform.position, targeted_enemy.transform.position);

                    if (others_distance < current_target_distance) {
                        targeted_enemy = e;
                    }
                }
            }
        }
    }

    public void SetMovement(InputAction.CallbackContext context) {
        _raw_movement = context.ReadValue<Vector2>();
        probe_transform.localPosition = new Vector3(_raw_movement.x, 0, _raw_movement.y);
        //Debug.Log("Raw Movement: " + _raw_movement.x + ", " + _raw_movement.y);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            if (targetable_enemies.Count <= 0) {
                targeted_enemy = other.GetComponent<Enemy>();
            }

            targetable_enemies.Add(other.GetComponent<Enemy>());

        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Enemy") {
            if (targeted_enemy == other.GetComponent<Enemy>()) {
                targeted_enemy = null;
            }

            targetable_enemies.Remove(other.GetComponent<Enemy>());
        }
    }
}
