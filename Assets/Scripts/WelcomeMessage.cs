using System.Collections;
using UnityEngine;
using WindRose.Behaviours.UI;
using GabTab.Behaviours;
using GabTab.Behaviours.Interactors;

[RequireComponent(typeof(MapInteractiveInterface))]
public class WelcomeMessage : MonoBehaviour {

    [SerializeField]
    [TextArea]
    private string text;

	// Use this for initialization
	IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<InteractiveInterface>().RunInteraction(WelcomeRoutine);
	}

    IEnumerator WelcomeRoutine(InteractorsManager manager, InteractiveMessage message)
    {
        NullInteractor dummyInteractor = (NullInteractor)manager["null"];
        ButtonsInteractor continueInteractor = (ButtonsInteractor)manager["continue"];
        string[] textLines = text.Split('\n');
        foreach(string textLine in textLines)
        {
            yield return dummyInteractor.RunInteraction(message, new InteractiveMessage.PromptBuilder().Write(textLine).NewlinePrompt(false).Wait(0.5f).End());
        }
        yield return continueInteractor.RunInteraction(message, new InteractiveMessage.PromptBuilder().End());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
