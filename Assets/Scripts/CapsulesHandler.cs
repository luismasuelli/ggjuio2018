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

    public void Launched()
    {
        // TODO: Acomodamos la particula para lanzarla
        // TODO: Tenemos en cuenta la direccion de inicio y la capsula de comienzo
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
        // TODO: Lanzamos un mensaje de exito, el cual usa un interactor de continuar, para avanzar al proximo nivel
        //   o para ganar el juego.
        yield break;
    }
}
