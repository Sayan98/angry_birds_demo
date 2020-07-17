using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sling_shot : MonoBehaviour
{
    Rigidbody2D sling_point_rb;
    Rigidbody2D rigidbody;
    SpringJoint2D springJoint;

    bool dragable;

    void Awake() {

        dragable = false;
    
    }

    void Start() {

        rigidbody = GetComponent<Rigidbody2D>();
        sling_point_rb = GameObject.Find("sling_point").GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();

        springJoint.connectedBody = sling_point_rb;

        rigidbody.isKinematic = true;
        rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    IEnumerator getmouse_pos() {

        if(dragable) {
            
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            /*mouse_pos.x = Mathf.Clamp(mouse_pos.x, -14, -8);
            mouse_pos.y = Mathf.Clamp(mouse_pos.y, -2.3f, 3);*/


            if(Vector2.Distance(mouse_pos, sling_point_rb.position) > 3.5f)
                rigidbody.position = sling_point_rb.position + ((mouse_pos - sling_point_rb.position).normalized) * 3.5f;
            else
                rigidbody.position = mouse_pos;

            yield return new WaitForFixedUpdate();
            StartCoroutine(getmouse_pos());

        }
        else
            yield return new WaitForFixedUpdate();

    }

    void OnMouseDown() {

        dragable = true;
        rigidbody.isKinematic = true;

        StartCoroutine(getmouse_pos());

    }

    void OnMouseUp() {
        
        dragable = false;
        rigidbody.isKinematic = false;
        StartCoroutine(shoot_player());

    }

    IEnumerator shoot_player() {

        if(Vector2.Distance(rigidbody.position, sling_point_rb.position) < 0.5f) {

            springJoint.enabled = false;
            yield return new WaitForEndOfFrame();

        }
        else {

            yield return new WaitForEndOfFrame();
            StartCoroutine(shoot_player());
        
        }

    }
    
    
}
