using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using UnityEditor;

// This is a super bare bones example of how to play and display a ink story in Unity.
public class DialogMenu : MonoBehaviour
{
	public static event Action<Story> OnCreateStory;
	private InteractionManagerNPC interactionManagerNPC;
	private TextAsset inkJSONAsset;
	private Action actionAfterDialog=null;

	public void startDialog(InteractionManagerNPC interactionManagerNPC, Story story, Action actionAfterDialog = null)
    {
		this.story = story;
		this.interactionManagerNPC = interactionManagerNPC;
        if (actionAfterDialog != null) { this.actionAfterDialog = actionAfterDialog; }
        if (story == null)//default dialog
        {
			Debug.Log("Couldn't find dialog -> using default");
			TextAsset ta = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/Dialoge/justLookingDialogue.json", typeof(TextAsset));
			this.story = new Story(ta.text);
		}
		RemoveChildren();
		StartStory();
	}
	
	public void startDialog()
    {
		RemoveChildren();
		StartStory();
	}
	// Creates a new Story object with the compiled story which we can then play!
	public void StartStory()
	{
		if (OnCreateStory != null) OnCreateStory(story);
		RefreshView();
	}

	// This is the main function called every time the story changes. It does a few things:
	// Destroys all the old content and choices.
	// Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
	void RefreshView()
	{
		// Remove all the UI on screen
		RemoveChildren();

		// Read all the content until we can't continue any more
		while (story.canContinue)
		{
			// Continue gets the next line of the story
			string text = story.Continue();
			// This removes any white space from the text.
			text = text.Trim();
			// Display the text on screen!
			CreateContentView(text);
		}

		// Display all the choices, if there are any!
		if (story.currentChoices.Count > 0)
		{
			for (int i = 0; i < story.currentChoices.Count; i++)
			{
				Choice choice = story.currentChoices[i];
				Button button = CreateChoiceView(choice.text.Trim());
				// Tell the button what to do when we press it
				button.onClick.AddListener(delegate {
					OnClickChoiceButton(choice);
				});
			}
		}
		// If we've read all the content and there's no choices, the story is finished!
		else
		{
			StartCoroutine(interactionManagerNPC.endDialog((bool)story.variablesState["positiveEnding"], actionAfterDialog));
		}
	}
	// When we click the choice button, tell the story to choose that choice!
	void OnClickChoiceButton(Choice choice)
	{
		story.ChooseChoiceIndex(choice.index);
		RefreshView();
	}

	// Creates a textbox showing the the line of text
	void CreateContentView(string text)
	{
		Text storyText = Instantiate(textPrefab) as Text;
		storyText.text = text;
		storyText.transform.SetParent(panel.transform, false);
	}

	// Creates a button showing the choice text
	Button CreateChoiceView(string text)
	{
		// Creates the button from a prefab
		Button choice = Instantiate(buttonPrefab) as Button;
		choice.transform.SetParent(panel.transform, false);

		// Gets the text from the button prefab
		Text choiceText = choice.GetComponentInChildren<Text>();
		choiceText.text = text;

		// Make the button expand to fit the text
		HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
		layoutGroup.childForceExpandHeight = false;

		return choice;
	}

	// Destroys all the children of this gameobject (all the UI)
	void RemoveChildren()
	{
		int childCount = panel.transform.childCount;
		for (int i = childCount - 1; i >= 0; --i)
		{
			GameObject.Destroy(panel.transform.GetChild(i).gameObject);
		}
	}

	//[SerializeField]
	//public TextAsset inkJSONAsset;
	public Story story;

	[SerializeField]
	private Canvas panel = null;

	// UI Prefabs
	[SerializeField]
	private Text textPrefab = null;
	[SerializeField]
	private Button buttonPrefab = null;
}
