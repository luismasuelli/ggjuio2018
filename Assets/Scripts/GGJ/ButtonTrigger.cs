using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WindRose.Behaviours;

[RequireComponent( typeof( TriggerReceiver ) )]
[RequireComponent( typeof( Sorted ) )]
public class ButtonTrigger : MonoBehaviour {

    public UnityEvent onPressed;
    public Sprite pressedSprite;
    public Sprite unpressedState;
    [SerializeField]
    private bool pressed;
    public enum ButtonType { ONCE, MANY }
    [SerializeField]
    private ButtonType buttonType = ButtonType.MANY;

    private SpriteRenderer spriteRenderer;
    private TriggerReceiver receiver;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        receiver = GetComponent<TriggerReceiver>();
        receiver.AddOnMapTriggerEnterListener(OnMapTriggerEnter);
        receiver.AddOnMapTriggerEnterListener(OnMapTriggerExit);
    }

    private void OnMapTriggerEnter(Positionable other, Positionable me, int x, int y) {
        if (!pressed) {
            pressed = true;
            onPressed.Invoke();
        }
    }

    private void OnMapTriggerExit(Positionable other, Positionable me, int x, int y) {
        if (buttonType == ButtonType.MANY) {
            pressed = false;
        }
    }

    private void Update() {
        spriteRenderer.sprite = pressed ? pressedSprite : unpressedState;
    }
}
