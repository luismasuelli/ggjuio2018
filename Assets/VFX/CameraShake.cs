using UnityEngine;
using System.Collections;

/*
 * Based on https://gist.github.com/ftvs/5822103
 */

public class CameraShake : MonoBehaviour {

    public Transform camTransform;

    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    public bool isShaking = false;

    void Awake() {
        if( camTransform == null ) {
            camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
        //Shake();
    }

    void Update() {
        if( shakeDuration > 0 ) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            isShaking = false;
        }
    }

    public IEnumerator ShakeRoutine(  ) {
        isShaking = true;
        while( isShaking ) {
            if( shakeDuration > 0 ) {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else {
                shakeDuration = 0f;
                camTransform.localPosition = originalPos;
                isShaking = false;
            }
            yield return null;
        }
    }

    public void Shake() {
        StartCoroutine( ShakeRoutine() );
    }
}