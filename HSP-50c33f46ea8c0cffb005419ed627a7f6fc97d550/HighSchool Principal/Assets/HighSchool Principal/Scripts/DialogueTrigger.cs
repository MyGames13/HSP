using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;
	public DialogueManager dm;
	
	public void TriggerDialogue()
	{
		dm.StartDialogue(dialogue);
	}
}
