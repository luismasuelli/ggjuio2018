using UnityEngine;
using WindRose.Types;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlyingParticle : MonoBehaviour {
    private Rigidbody2D rigidBody2D;
    public Direction? Movement = Direction.DOWN;
    public ParticleSystem particleTrail;

    private const float SPEED = 8f;

    private void MakeTrailFollowMe()
    {
        if (particleTrail != null)
        {
            particleTrail.transform.position = new Vector3(transform.position.x, transform.position.y, particleTrail.transform.position.z);
            particleTrail.transform.rotation = transform.rotation;
        }
    }
	// Use this for initialization
	void Start () {
        rigidBody2D = GetComponent<Rigidbody2D>();
        MakeTrailFollowMe();
    }
	
	// Update is called once per frame
	void Update () {
        MakeTrailFollowMe();
        Vector2 speed;
		switch(Movement)
        {
            case Direction.DOWN:
                speed = new Vector2(0, -SPEED);
                break;
            case Direction.UP:
                speed = new Vector2(0, SPEED);
                break;
            case Direction.LEFT:
                speed = new Vector2(-SPEED, 0);
                break;
            case Direction.RIGHT:
                speed = new Vector2(SPEED, 0);
                break;
            default:
                speed = Vector2.zero;
                break;
        }
        rigidBody2D.MovePosition((Vector2)transform.position + speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Mirror mirror = collision.gameObject.GetComponentInParent<Mirror>();
        Capsule capsule = collision.gameObject.GetComponent<Capsule>();
        CapsulesHandler handler = GetComponentInParent<CapsulesHandler>();

        if (mirror != null)
        {
            Debug.Log("Mirror looking at: " + mirror.orientation);
            Debug.Log("Movement of particle: " + Movement);
            switch (mirror.orientation)
            {
                case Mirror.Orientation.LEFT_DOWN:
                    switch(Movement)
                    {
                        case Direction.UP:
                            handler.Crashed();
                            break;
                        case Direction.RIGHT:
                            handler.Crashed();
                            break;
                        case Direction.LEFT:
                            Movement = Direction.UP;
                            break;
                        case Direction.DOWN:
                            Movement = Direction.RIGHT;
                            break;
                    }
                    break;
                case Mirror.Orientation.LEFT_UP:
                    switch (Movement)
                    {
                        case Direction.UP:
                            Movement = Direction.RIGHT;
                            break;
                        case Direction.RIGHT:
                            handler.Crashed();
                            break;
                        case Direction.LEFT:
                            Movement = Direction.DOWN;
                            break;
                        case Direction.DOWN:
                            handler.Crashed();
                            break;
                    }
                    break;
                case Mirror.Orientation.RIGHT_UP:
                    switch (Movement)
                    {
                        case Direction.UP:
                            Movement = Direction.LEFT;
                            break;
                        case Direction.RIGHT:
                            Movement = Direction.DOWN;
                            break;
                        case Direction.LEFT:
                            handler.Crashed();
                            break;
                        case Direction.DOWN:
                            handler.Crashed();
                            break;
                    }
                    break;
                case Mirror.Orientation.RIGHT_DOWN:
                    switch (Movement)
                    {
                        case Direction.UP:
                            handler.Crashed();
                            break;
                        case Direction.RIGHT:
                            Movement = Direction.UP;
                            break;
                        case Direction.LEFT:
                            handler.Crashed();
                            break;
                        case Direction.DOWN:
                            Movement = Direction.LEFT;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        else if (capsule != null)
        {
            if (capsule == handler.StartingCapsule)
            {
                handler.Crashed();
            }
            else
            {
                handler.Reached();
            }
        }
        else
        {
            handler.Crashed();
        }
    }
}
