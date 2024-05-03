using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public GrapplingGun grappling;



     void Update()
    {
        if (grappling.IsGrappling())
        {
            transform.LookAt(grappling.GetGrapplePoint());
        }
        else
        {
            transform.localRotation = Quaternion.Euler(10, 0, 0);
        }
      


    }

}
