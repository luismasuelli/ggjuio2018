using System.Collections;
using UnityEngine;
using WindRose.Types;
using WindRose.Behaviours;
using GabTab.Behaviours;
using GabTab.Behaviours.Interactors;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class CapsulesHandler : MonoBehaviour {
    private Animator animator;
    private FlyingParticle flyingParticle;
    public Direction StartingDirection = Direction.DOWN;
    public Capsule StartingCapsule;
    private bool finalReached = false;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private InteractiveInterface interactiveInterface;
    [SerializeField]
    private KeyboardHandled doctor;

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
        doctor.enabled = false;
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
        doctor.oriented.orientation = Direction.DOWN;
        interactiveInterface.RunInteraction(Swear);
        flyingParticle.particleTrail.Stop();
        crashing.Play();
    }

    private IEnumerator Swear(InteractorsManager manager, InteractiveMessage message)
    {
        ButtonsInteractor restartInteractor = (ButtonsInteractor)manager["restart"];
        yield return restartInteractor.RunInteraction(message, new InteractiveMessage.PromptBuilder().Clear().Write("Coooo#$%^&*o!").Wait(0.5f).End());
        if (restartInteractor.Result == "current")
        {
            // reinicia el nivel.
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else
        {
            // reinicia el juego.
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void Reached()
    {
        animator.SetBool("Reached", true);
        flyingParticle.particleTrail.Stop();
        teleporting.Play();
    }

    public void Landed()
    {
        doctor.oriented.orientation = Direction.DOWN;
        animator.SetBool("Landed", true);
        interactiveInterface.RunInteraction(Yay);
        happyMonkey.Play();
    }

    private IEnumerator Yay(InteractorsManager manager, InteractiveMessage message)
    {
        NullInteractor dummyInteractor = (NullInteractor)manager["null"];
        ButtonsInteractor continueInteractor = (ButtonsInteractor)manager["continue"];
        yield return continueInteractor.RunInteraction( message, new InteractiveMessage.PromptBuilder().Clear().Write( "Siiiiii funciono!" ).Wait( 0.5f ).End() );
        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
    }
}
