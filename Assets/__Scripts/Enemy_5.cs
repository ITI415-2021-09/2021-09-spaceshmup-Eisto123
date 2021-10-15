using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_5 : Enemy
{
    [Header("Set in Inspector: Enemy_5")]
    // # seconds for a full sine wave
    public float waveFrequency = 2;
    // sine wave width in meters
    public float waveWidth = 4;
    public float waveRotY = 45;
    public float sinEccentricity = 0.6f;
    public float lifeTime = 10;

    private Vector3 p0;// The initial pos
    private Vector3 p1;//player pos
    private float birthTime;

    // Use this for initialization
    void Start()
    {
        // Set x0 to the initial x position of Enemy_1
        p0 = pos;
        float widMinRad = bndCheck.camWidth - bndCheck.radius;
        float hgtMinRad = bndCheck.camHeight - bndCheck.radius;
        p0.x = Random.Range(-widMinRad, widMinRad);
        GameObject player = GameObject.FindGameObjectWithTag("Hero");
        p1 = player.transform.position;
        float angle = 0;
        
        //turn angle to player
        if (p1.x < 0)
        {
            angle = Mathf.Atan2(-p1.x, p0.y-p1.y) * Mathf.Rad2Deg - 90f;
            angle *= -1;
            angle -= 90;
        }
        else
        {
            angle = Mathf.Atan2(p1.x, p0.y - p1.y) * Mathf.Rad2Deg - 90f;
            angle += 90;
        }
        this.transform.eulerAngles = new Vector3(0, 0, 180+angle);
        birthTime = Time.time;
    }

    // Override the Move function on Enemy
    public override void Move()
    {

        float u = (Time.time - birthTime) / lifeTime;

        // If u>1, then it has been longer than lifeTime since birthTime
        if (u > 1)
        {
            // This Enemy_2 has finished its life
            Destroy(this.gameObject);
            return;
        }

        // Adjust u by adding a U Curve based on a Sine wave
        u = u +  sinEccentricity * (Mathf.Tan(u * Mathf.PI * 2));

        // Interpolate the two linear interpolation points
        pos = ((1 - u) * p0) + (u * p1);


    }
}
