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
        //float directionMultiplier = 1.0f;
        //this.transform.RotateAround( rotatePivot.transform.position, -Vector3.forward, directionMultiplier * 90.0f );
        StartCoroutine( RotateAnimated() );
    }

    public IEnumerator RotateAnimated() {

        float directionMultiplier = 1.0f;
        float rotationDuration = 0.25f;
        float elapsedTime = 0.0f;

        Quaternion initialAngle = this.transform.rotation;
        Vector3 initialPosition = this.transform.position;

        this.transform.RotateAround( rotatePivot.transform.position, -Vector3.forward, directionMultiplier * 90.0f );

        Quaternion finalAngle = this.transform.rotation;
        Vector3 finalPosition = this.transform.position;

        this.transform.rotation = initialAngle;
        this.transform.position = initialPosition;

        float rotationDurationBck = rotationDuration;

        while( elapsedTime <= rotationDurationBck ) {

            this.transform.RotateAround( rotatePivot.transform.position, -Vector3.forward, directionMultiplier * 90.0f * Time.deltaTime / rotationDuration );

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        this.transform.rotation = finalAngle;
        this.transform.position = finalPosition;
    }
}
