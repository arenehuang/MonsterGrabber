//Created by Rob Harwood
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEnemy : Enemy {



    private void Awake() {
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void FixedUpdate() {
        Wander();
        Follow();
    }

    protected override void Wander() {
        if (!_aggroed) {

            _distance_from_waypoint = Vector3.Distance(this.transform.position, _area_waypoint.position);

            if (_distance_from_waypoint > _max_distance_from_waypoint) {
                //Go back to way point

                Vector3 direction = (_area_waypoint.position - this.transform.position);
                transform.Translate(direction * Time.deltaTime * _current_speed);

            }
            else {
                //Randomly wander
            }

        }
    }

    protected override void Follow() {
        if (_aggroed) { 

        }
    }

    public override void Use() {

    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
    }
}
