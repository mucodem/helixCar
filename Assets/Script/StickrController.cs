using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwervingMechanic;
public class StickrController : SwerveMec
{
    [SerializeField] float lerpMultiplier;
    [SerializeField] float clamper;

    void Start()
    {
        LerpMultiplier = lerpMultiplier;
        ClampMaxValue = clamper;
        Pos = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            SwipeSwerveMec(transform);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Timer = true;
        }
    }
}
