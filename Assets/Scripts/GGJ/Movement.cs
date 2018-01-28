using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Transform transform;

    private void Start() {
        transform = GetComponent<Transform>();
    }

    void Update () {
        if( Input.GetKeyDown( KeyCode.D) ) {
            //if( Physics.Raycast( transform.position, new Vector3( 1,0,0 ), 1.1f ) ) {
                transform.position += new Vector3( 1, 0, 0 );
            //}
        }
        if( Input.GetKeyDown( KeyCode.A ) ) {
            transform.position += new Vector3( -1, 0, 0 );
        }
        if( Input.GetKeyDown( KeyCode.S ) ) {
            transform.position += new Vector3( 0, -1, 0 );
        }
        if( Input.GetKeyDown( KeyCode.W ) ) {
            transform.position += new Vector3( 0, 1, 0 );
        }
    }
}
