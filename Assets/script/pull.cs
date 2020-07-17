using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pull : MonoBehaviour
{
    Rigidbody2D rigidbody;
    SpringJoint2D joint2D;

    bool track_mouse;

    void Awake() {
        track_mouse = false;
    }

    void Start() {
        rigidbody = GetComponent<Rigidbody2D>();
        joint2D = GetComponent<SpringJoint2D>();
    }

    void LateUpdate() {
        
        if(track_mouse)
            track_mouse_pos();

    }

    private void track_mouse_pos() {

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rigidbody.position = pos;
        rigidbody.isKinematic = true;

    }

    void OnMouseDown() {
        track_mouse = true;
    }

    void OnMouseUp() {
        track_mouse = false;
        rigidbody.isKinematic = false;

        StartCoroutine(wait());
    }

    IEnumerator wait() {

        yield return new WaitForSeconds(0.2f);
        joint2D.enabled = false;

    }


    
}
