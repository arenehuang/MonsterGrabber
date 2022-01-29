//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] private float _current_hp = 10f;
    [SerializeField] private float _max_hp = 10f;

    [SerializeField] private HealthBarController _player_healthbar_controller;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        CheckHealth();
    }

    public void DoDamage(float damage) {
        _current_hp -= damage;
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        _player_healthbar_controller.SetHealthSlider(_current_hp);
    }

    private void CheckHealth() {
        if (_current_hp <= 0) {
            Debug.Log("You died");
        }
    }
}
