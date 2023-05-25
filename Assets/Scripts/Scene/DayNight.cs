using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
- Creator:    Bert0h
- Script:     Day And Night 2D System
- Unity:      2021 LTS Version
*/

public enum DayCycles //Enum con los ciclos de dia
{
    Sunrise = 0,
    Day = 1,
    Sunset = 2,
    Night = 3,
    Midnight = 4
}

public class DayNight : MonoBehaviour
{
    [Header("Controllers")]

    [Tooltip("This is a current cycle time, you can change for private float but we keep public only for debug")]
    public float cycleCurrentTime = 0; // current cycle time

    [Tooltip("This is a cycle max time in seconds, if current time reach this value we change the state of the day and night cyles")]
    public float cycleMaxTime = 60; // duration of cycle

    [Tooltip("Enum with multiple day cycles to change over time, you can add more types and modify whatever you want to fits on your project")]
    public DayCycles dayCycle = DayCycles.Sunrise; // default cycle
    public bool DayCyclesBool = false;

    [Header("Controllers")]
    private Color32 Sunrise;
    private Color32 Day;
    private Color32 Sunset;
    private Color32 Night;
    private Color32 Midnight;


    public List<GameObject> spriteColors;


    void Start()
    {
        Sunrise = new Color32(255, 251, 182, 15);
        Day = new Color32(255, 253, 223, 15);
        Sunset = new Color32(255, 157, 145, 15);
        Night = new Color32(144, 54, 255, 15);
        Midnight = new Color32(27, 0, 255, 15);

        if (DayCyclesBool)
        {
            dayCycle = DayCycles.Sunrise; // start with sunrise state
            foreach (GameObject sprite in spriteColors)
            {
                sprite.GetComponent<SpriteRenderer>().color = Sunrise;
            }
        }
        else
        {
            foreach (GameObject sprite in spriteColors)
            {
                sprite.GetComponent<SpriteRenderer>().color = Midnight;
            }
        }



    }

    void Update()
    {
        if (DayCyclesBool)
        {
            // Update cycle time
            cycleCurrentTime += Time.deltaTime;

            // Check if cycle time reach cycle duration time
            if (cycleCurrentTime >= cycleMaxTime)
            {
                cycleCurrentTime = 0; // back to 0 (restarting cycle time)
                dayCycle++; // change cycle state
            }

            // If reach final state we back to sunrise (Enum id 0)
            if (dayCycle > DayCycles.Midnight)
                dayCycle = 0;

            // percent it's an value between current and max time to make a color lerp smooth
            float percent = cycleCurrentTime / cycleMaxTime;

            // Sunrise state (you can do a lot of stuff based on every cycle state, like enable animals only in sunrise )
            if (dayCycle == DayCycles.Sunrise)
            {
                foreach (GameObject sprite in spriteColors)
                {
                    sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Sunrise, Day, percent);
                }

            }

            // Mid Day state
            if (dayCycle == DayCycles.Day)
                foreach (GameObject sprite in spriteColors)
                    sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Day, Sunset, percent);

            // Sunset state
            if (dayCycle == DayCycles.Sunset)
                foreach (GameObject sprite in spriteColors)
                    sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Sunset, Night, percent);

            // Night state
            if (dayCycle == DayCycles.Night)
            {

                foreach (GameObject sprite in spriteColors)
                    sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Night, Midnight, percent);
            }

            // Midnight state
            if (dayCycle == DayCycles.Midnight)
                foreach (GameObject sprite in spriteColors)
                    sprite.GetComponent<SpriteRenderer>().color = Color.Lerp(Midnight, Day, percent);
        }
    }


}
