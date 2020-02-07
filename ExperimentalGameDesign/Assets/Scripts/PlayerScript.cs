using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerScript : MonoBehaviour {

    public KeyCode tilt_down_key;
    public KeyCode tilt_up_key;
    public KeyCode realse_key;
    public KeyCode drink_key = KeyCode.Space;

    public KeyCode[] move_keys;

    public static int drunkiness = 0;
    public int maxDrunkiness;
    private GameObject bottle;

    private Transform cursor;
    public float speed;
    public float rotate_speed;

    private bool hasReleased = false;
    private int releaseTimer = 10;
    private bool IsHoldingGlass = false;
    private BottlePlug plug;
    private bool ResetBottle = false;
    
    // Start is called before the first frame update
    void Start() {
        cursor = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if(drunkiness >= maxDrunkiness) {
            Debug.LogError("You black out");
        }


        movement();

        if (Input.GetKey(drink_key) && !ResetBottle && plug != null) {
            plug.ResetBottle();
            Debug.Log("Drunkiness: " + drunkiness);
            plug = null;
            bottle = null;
            ResetBottle = true;
            hasReleased = true;
        }

        if (bottle) {
            bottle.transform.position = cursor.position;

            float deg = rotate_speed * Mathf.Deg2Rad;

            if (Input.GetKey(tilt_up_key)) {
                bottle.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f), -deg);
            }
            if (Input.GetKey(tilt_down_key)) {
                bottle.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f), deg);
            }
            if (Input.GetKey(realse_key)) {
                hasReleased = true;
                bottle.GetComponent<Rigidbody>().useGravity = true;
                bottle = null;
            }
        }
        if (hasReleased) {
            releaseTimer--;
            if(releaseTimer < 0 && !IsHoldingGlass) {
                hasReleased = false;
                releaseTimer = 10;
                ResetBottle = false;
            }
        }
    }

    void movement() {
        if (Input.GetKey(move_keys[EventManager.getInput()[0]])) {
            move_cursor(Vector3.right);
        }
        if (Input.GetKey(move_keys[EventManager.getInput()[1]])) {
            move_cursor(Vector3.forward);
        }
        if (Input.GetKey(move_keys[EventManager.getInput()[2]])) {

            move_cursor(Vector3.left);
        }
        if (Input.GetKey(move_keys[EventManager.getInput()[3]])) {
            move_cursor(Vector3.back);
        }
    }

    void move_cursor(Vector3 dir) {
        cursor.Translate(dir * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Bottle" && !hasReleased) {
            bottle = other.gameObject;
            bottle.GetComponent<Rigidbody>().useGravity = false;
            plug = bottle.GetComponent<BottlePlug>();
        }
    }
}
