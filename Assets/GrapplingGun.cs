using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class GrapplingGun : MonoBehaviour
{

    [SerializeField] private AudioSource GrappleSound;

    //Linerender reference
    private LineRenderer lr;
    //Ditt vi grapplar 
    private Vector3 grapplePoint;
    //Vad vi kan grappla på
    public LayerMask whatIsGrappleable;
    
    public Transform gunTip, Camera, capsule;

    public GameObject Hook;
    //Hur lång man kan sjuta ut grapplen
    private float maxDistance = 250f;

    private SpringJoint joint;

  



    private Rigidbody rb;
   


    private void Start()
    {
        Hook.SetActive(true);
       
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

       
    }

    //Om vi håller det knappen så ska vi börja grappla om inte ska vi sluta grappla 
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    
    //När vi kallar våran startgrapple
    void StartGrapple()
    {


        //Vad som händer när vi träffar en Raycast
        RaycastHit hit;
        if (Physics.Raycast(Camera.position, Camera.forward, out hit, maxDistance))
        {
            //Springjoint ändringa
            grapplePoint = hit.point;
            joint = capsule.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            //Kollar hur långt det är ifrån spelaren och grapple pointen
            float distanceFromPoint = Vector3.Distance(capsule.position, grapplePoint);
            // Hur spingjointen ökas och minksas i längd under startgrapple
            joint.maxDistance = distanceFromPoint * 1f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 1f;
            joint.damper = 5f;
            joint.massScale = 3f;

            lr.positionCount = 2;

            Hook.SetActive(false);

            GrappleSound.Play();
        }



    }

  

   //När vi kallar våran stopgrapple
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
        Hook.SetActive(true);

    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);


    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

   

}
