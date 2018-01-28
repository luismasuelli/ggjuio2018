using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindRose.Types;
using WindRose.Behaviours.UI;
using GabTab.Behaviours;
using GabTab.Behaviours.Interactors;
using UnityEditor.SceneManagement;

[RequireComponent(typeof(Animator))]
public class CapsulesHandler : MonoBehaviour {
    private Animator animator;
    private FlyingParticle flyingParticle;
    public Direction StartingDirection = Direction.DOWN;
    public Capsule StartingCapsule;
    private bool finalReached = false;
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private InteractiveInterface interactiveInterface;

    [SerializeField]
    private AudioSource shooting;
    [SerializeField]
    private AudioSource teleporting;
    [SerializeField]
    private AudioSource crashing;
    [SerializeField]
    private AudioSource happyMonkey;
    [SerializeField]
    private AudioSource bouncing;

    void Start()
    {
        animator = GetComponent<Animator>();
        flyingParticle = GetComponentInChildren<FlyingParticle>(true);
    }

    public void ButtonPressed()
    {
        animator.SetBool("Pressed", true);
        teleporting.Play();
    }

    private void PrepareParticle() {
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
        flyingParticle.particleTrail.Play();
        shooting.Play();
    }

    public void Crashed()
    {
        animator.SetBool("Crashed", true);
        interactiveInterface.RunInteraction(Swear);
        flyingParticle.particleTrail.Stop();
        crashing.Play();
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
        flyingParticle.particleTrail.Stop();
        teleporting.Play();
    }

    public void Landed()
    {
        animator.SetBool("Landed", true);
        interactiveInterface.RunInteraction(Yay);
        happyMonkey.Play();
    }

    private IEnumerator Yay(InteractorsManager manager, InteractiveMessage message)
    {
        NullInteractor dummyInteractor = (NullInteractor)manager["null"];
        ButtonsInteractor continueInteractor = (ButtonsInteractor)manager["continue"];
        yield return continueInteractor.RunInteraction( message, new InteractiveMessage.PromptBuilder().Clear().Write( "Siiiiii funciono!" ).Wait( 0.5f ).End() );
    }
}
