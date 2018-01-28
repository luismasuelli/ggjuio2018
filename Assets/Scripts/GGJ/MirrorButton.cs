using UnityEngine;
using WindRose.Behaviours;

[RequireComponent(typeof(ButtonTrigger))]
public class MirrorButton : MonoBehaviour {
    private ButtonTrigger trigger;
    private Mirror targetMirror;

    // Use this for initialization
    void Start () {
        trigger = GetComponent<ButtonTrigger>();
        trigger.onPressed.AddListener(() => {
            targetMirror.Rotate();
        });
	}
}
