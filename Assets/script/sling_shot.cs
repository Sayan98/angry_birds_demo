using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sling_shot : MonoBehaviour
{
    [SerializeField]
    LineRenderer lr_left, lr_right;

    Rigidbody2D rigidbody, sling_point_rb;
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
        //rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

        sling_band_func( new Vector2(rigidbody.position.x - 0.30f, rigidbody.position.y - 0.30f) );
    }


    IEnumerator getmouse_pos() {

        if(dragable) {
            
            Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Vector2.Distance(mouse_pos, sling_point_rb.position) > 3f)
                rigidbody.position = sling_point_rb.position + ((mouse_pos - sling_point_rb.position).normalized) * 3f;
            else
                rigidbody.position = mouse_pos;

            sling_band_func( new Vector2(rigidbody.position.x - 0.30f, rigidbody.position.y - 0.30f) );

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


    private void sling_band_func(Vector2 target) {

        Vector3[] pos = new Vector3[2];
        
        pos[1] = target;
        
        pos[0] = lr_left.transform.position;
        lr_left.SetPositions(pos);

        pos[0] = lr_right.transform.position;
        lr_right.SetPositions(pos); 

    }


    void OnMouseUp() {
        
        dragable = false;
        rigidbody.isKinematic = false;
        StartCoroutine(shoot_player());

    }


    IEnumerator shoot_player() {

        ///backup
        /*sling_band_func(sling_point_rb);
        yield return new WaitForSeconds(0.2f);
        springJoint.enabled = false;*/


        if(Vector2.Distance(rigidbody.position, sling_point_rb.position) < 0.7f) {

            springJoint.enabled = false;
            sling_band_func(sling_point_rb.position);
            yield return new WaitForEndOfFrame();

        }
        else {

            yield return new WaitForEndOfFrame();
            sling_band_func( new Vector2(rigidbody.position.x - 0.30f, rigidbody.position.y - 0.30f) );
            StartCoroutine(shoot_player());
        
        }

    }
    
    
}
//// line ren sorting order