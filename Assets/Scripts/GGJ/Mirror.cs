using UnityEngine;
using WindRose.Behaviours;

[RequireComponent(typeof(Sorted))]
public class Mirror : MonoBehaviour {
    private SpriteRenderer renderer;

    public enum Orientation { LEFT_DOWN, LEFT_UP, RIGHT_UP, RIGHT_DOWN }
    public Orientation orientation = Orientation.LEFT_DOWN;

    [SerializeField]
    private Sprite leftDown;
    [SerializeField]
    private Sprite leftUp;
    [SerializeField]
    private Sprite rightUp;
    [SerializeField]
    private Sprite rightDown;

    [SerializeField]
    private PolygonCollider2D leftDownCollider;
    [SerializeField]
    private PolygonCollider2D leftUpCollider;
    [SerializeField]
    private PolygonCollider2D rightUpCollider;
    [SerializeField]
    private PolygonCollider2D rightDownCollider;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
	}

    public void Rotate()
    {
        switch(orientation)
        {
            case Orientation.LEFT_DOWN:
                orientation = Orientation.LEFT_UP;
                break;
            case Orientation.LEFT_UP:
                orientation = Orientation.RIGHT_UP;
                break;
            case Orientation.RIGHT_UP:
                orientation = Orientation.RIGHT_DOWN;
                break;
            case Orientation.RIGHT_DOWN:
                orientation = Orientation.LEFT_DOWN;
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
        leftDownCollider.enabled = false;
        leftUpCollider.enabled = false;
        rightUpCollider.enabled = false;
        rightDownCollider.enabled = false;
        switch (orientation)
        {
            case Orientation.LEFT_DOWN:
                leftDownCollider.enabled = true;
                renderer.sprite = leftDown;
                break;
            case Orientation.LEFT_UP:
                leftUpCollider.enabled = true;
                renderer.sprite = leftUp;
                break;
            case Orientation.RIGHT_UP:
                rightUpCollider.enabled = true;
                renderer.sprite = rightUp;
                break;
            case Orientation.RIGHT_DOWN:
                rightDownCollider.enabled = true;
                renderer.sprite = rightDown;
                break;
        }
    }

}
