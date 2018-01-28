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
        int cntTextLines = textLines.Length;
        int lastTextLineIndex = cntTextLines - 1;
        for(int i = 0; i < cntTextLines; i++)
        {
            string textLine = textLines[i];
            InteractiveMessage.PromptBuilder builder = new InteractiveMessage.PromptBuilder().Write(textLine);
            if (i < lastTextLineIndex)
            {
                builder.NewlinePrompt(true);
            }
            builder.Wait(0.5f);
            yield return dummyInteractor.RunInteraction(message, builder.End());
        }
        yield return continueInteractor.RunInteraction(message, new InteractiveMessage.PromptBuilder().End());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
