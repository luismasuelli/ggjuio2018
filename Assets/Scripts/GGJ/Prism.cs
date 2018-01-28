using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : MonoBehaviour {

    public GameObject[] angles;
    public GameObject[] walls;
    public float rotationDuration = 0.25f;
    public GameObject rotatePivot;

    // Use this for initialization
    void Start() {
        //StartCoroutine( RotateAnimated() );
        //Rotate();
    }

    public void Rotate() {
        float directionMultiplier = 1.0f;
        this.transform.RotateAround( rotatePivot.transform.position, -Vector3.forward, directionMultiplier * 90.0f );
    }

    public IEnumerator RotateAnimated() {

        float directionMultiplier = 1.0f;
        float elapsedTime = 0.0f;
        while( elapsedTime <= rotationDuration ) {
            //Debug.Log("Rotation forward");
            //this.transform.Rotate( 0, 0, 90.0f );
            this.transform.RotateAround( rotatePivot.transform.position, -Vector3.forward, directionMultiplier * 90.0f );

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
}
