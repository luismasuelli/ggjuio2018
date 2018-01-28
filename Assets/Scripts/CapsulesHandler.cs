using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindRose.Types;
using WindRose.Behaviours.UI;
using GabTab.Behaviours;
using GabTab.Behaviours.Interactors;
using UnityEditor.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MapInteractiveInterface))]
public class CapsulesHandler : MonoBehaviour {
    private Animator animator;
    private InteractiveInterface interactiveInterface;
    public Direction StartingDirection = Direction.DOWN;
    public Capsule StartingCapsule;
    [SerializeField]
    private string sceneName; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ButtonPressed()
    {
        animator.SetBool("Pressed", true);
    }

    private void PrepareParticle() {
        FlyingParticle flyingParticle = GetComponentInChildren<FlyingParticle>(true);

        flyingParticle.Movement = StartingDirection;
        switch( StartingDirection ) {
            case Direction.DOWN:
                flyingParticle.transform.position = (Vector2)StartingCapsule.transform.position + new Vector2( 0.5f, -1.25f );
                break;
            case Direction.UP:
                flyingParticle.transform.position = (Vector2)StartingCapsule.transform.position + new Vector2( 0.5f, 0.25f );
                break;
            case Direction.LEFT:
                flyingParticle.transform.position = (Vector2)StartingCapsule.transform.position + new Vector2( -0.25f, -0.5f );
                break;
            case Direction.RIGHT:
                flyingParticle.transform.position = (Vector2)StartingCapsule.transform.position + new Vector2( 1.25f, -0.5f );
                break;
        }
    }

    public void Launched()
    {
        PrepareParticle();
        animator.SetBool("Launched", true);
    }

    public void Crashed()
    {
        animator.SetBool("Crashed", true);
        interactiveInterface.RunInteraction(Swear);
    }

    private IEnumerator Swear(InteractorsManager manager, InteractiveMessage message)
    {
        // TODO: Lanzamos una mega puteada, la cual usa un interactor de dos botones (reiniciar nivel, reiniciar juego)
        //   para que, entonces, el nivel (o el juego) se reinicien.
        yield break;
    }

    public void Reached()
    {
        animator.SetBool("Reached", true);
    }

    public void Landed()
    {
        animator.SetBool("Landed", true);
    }

    public void Final()
    {
        interactiveInterface.RunInteraction(Yay);
    }

    private IEnumerator Yay(InteractorsManager manager, InteractiveMessage message)
    {
        NullInteractor dummyInteractor = (NullInteractor)manager["null"];
        ButtonsInteractor continueInteractor = (ButtonsInteractor)manager["continue"];
        yield return dummyInteractor.RunInteraction( message, new InteractiveMessage.PromptBuilder().Write( "Siiiiii funciono!" ).Wait( 0.5f ).End() );
    }
}
