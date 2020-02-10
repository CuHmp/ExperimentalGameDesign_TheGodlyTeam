using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {
    //List of events, randomize input keys list
    // timer 
    public Sprite[] sprites;
    public  Image[] buttons;
    private Scrollbar DrunkOMeter;
    private static Image drunk_screen;

    private static int[] input_keys;
    private float MaxTime = 1000;
    private float timer;
    private int storedDrunkMeter;
    private bool ToggleDrunkMeter = false;


    private static int auto_tilt;
    // Start is called before the first frame update
    void Start() {
        drunk_screen = GameObject.FindGameObjectWithTag("DrunkScreen").GetComponent<Image>();
        DrunkOMeter = GameObject.FindGameObjectWithTag("DrunkMeter").GetComponent<Scrollbar>();
        timer = MaxTime;
        input_keys = new int[4];
        for(int i = 0; i < 4; i++) {
            input_keys[i] = i;
        }
    }

    public static int[] getInput() { 
        return input_keys; 
    }

    public static int getAutoTilt() {
        return auto_tilt;
    }

    public static Image getDrunkScreen() {
        return drunk_screen;
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime * PlayerScript.drunkiness / 2;
        
        if (PlayerScript.drunkiness != storedDrunkMeter && !ToggleDrunkMeter) {
            //drunk_screen.color = new Color(drunk_screen.color.r, drunk_screen.color.g, drunk_screen.color.b, ValueMap(PlayerScript.drunkiness, 0, 500, 0, 1));
            DrunkOMeter.size = ValueMap(PlayerScript.drunkiness, 0, 750, 0, 1);
            storedDrunkMeter = PlayerScript.drunkiness;
        }

        if (timer <= 0) {
            timer = MaxTime;
            int eventID = Random.Range(0,4);
            switch (eventID) {
                case 0: 
                case 1: 
                {
                    reshuffle(input_keys);
                    setUISprites();
                    break;
                }
                case 2: 
                {
                    auto_tilt = (int)(Random.Range(-75,75) * Mathf.Deg2Rad);
                    break;
                }
                case 3: 
                {
                    auto_tilt = (int)(Random.Range(-75, 75) * Mathf.Deg2Rad);
                    reshuffle(input_keys);
                    setUISprites();
                    break;
                }
                case 4: 
                    {
                        ToggleDrunkMeter = !ToggleDrunkMeter;

                        break;
                    }
            }

        }

    }

    float ValueMap(float value, float in_min, float in_max, float out_min, float out_max) {
        return (value - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private void setUISprites() {
        for(int i = 0; i < 4; i++) {
            buttons[i].sprite = sprites[input_keys[i]];
        }
    }

    private void reshuffle(int[] nums) {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < nums.Length; t++) {
            int tmp = nums[t];
            int r = Random.Range(t, nums.Length);
            nums[t] = nums[r];
            nums[r] = tmp;
        }
    }

}
