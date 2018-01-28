using UnityEngine;
using WindRose.Types;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FlyingParticle : MonoBehaviour {
    private Rigidbody2D rigidBody2D;
    public Direction? Movement = Direction.DOWN;
    public ParticleSystem particleTrail;
    private bool bouncing;

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
        Debug.Log(speed);
        rigidBody2D.MovePosition((Vector2)transform.position + speed * Time.deltaTime);
    }

    private void Bounce(Direction direction)
    {
        Debug.Log(string.Format("Bouncing from direction {0} to direction {1}", Movement, direction));
        bouncing = true;
        Movement = direction;
    }

    private void HitMirrorBadly(CapsulesHandler handler)
    {
        Debug.Log(string.Format("Crashing with direction {0}", Movement));
        if (!bouncing) handler.Crashed();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Leaving bounce");
        bouncing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Mirror mirror = collision.gameObject.GetComponentInParent<Mirror>();
        Capsule capsule = collision.gameObject.GetComponent<Capsule>();
        CapsulesHandler handler = GetComponentInParent<CapsulesHandler>();

        if (mirror != null)
        {
            switch (mirror.orientation)
            {
                case Mirror.Orientation.LEFT_DOWN:
                    switch(Movement)
                    {
                        case Direction.UP:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.RIGHT:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.LEFT:
                            Bounce(Direction.UP);
                            break;
                        case Direction.DOWN:
                            Bounce(Direction.RIGHT);
                            break;
                    }
                    break;
                case Mirror.Orientation.LEFT_UP:
                    switch (Movement)
                    {
                        case Direction.UP:
                            Bounce(Direction.RIGHT);
                            break;
                        case Direction.RIGHT:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.LEFT:
                            Bounce(Direction.DOWN);
                            break;
                        case Direction.DOWN:
                            HitMirrorBadly(handler);
                            break;
                    }
                    break;
                case Mirror.Orientation.RIGHT_UP:
                    switch (Movement)
                    {
                        case Direction.UP:
                            Bounce(Direction.LEFT);
                            break;
                        case Direction.RIGHT:
                            Bounce(Direction.DOWN);
                            break;
                        case Direction.LEFT:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.DOWN:
                            HitMirrorBadly(handler);
                            break;
                    }
                    break;
                case Mirror.Orientation.RIGHT_DOWN:
                    switch (Movement)
                    {
                        case Direction.UP:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.RIGHT:
                            Bounce(Direction.UP);
                            break;
                        case Direction.LEFT:
                            HitMirrorBadly(handler);
                            break;
                        case Direction.DOWN:
                            Bounce(Direction.LEFT);
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
