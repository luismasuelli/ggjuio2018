using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonTrigger : MonoBehaviour {

    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter( Collider collision ) {
        if( collision.tag == "Player" ) {
            onTriggerEnter.Invoke();
        }
    }

    public void ButtonPush() {
        Debug.Log("ButtonPress");
    }
}
