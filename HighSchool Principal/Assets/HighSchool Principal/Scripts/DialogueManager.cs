using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{ 
	
	public Text nameText;
	public Text dialogueText;
	public DialogueTrigger dt,firstdt;
	public GameObject options, continueButton;
	public ParticleSystem[] win,failed,won;
	GameManager gm;
	bool firstdtTriggered;
	
	public bool finalDialogue,needOptions,firstDialogue, TENSED,Happy,Sad;
	
	public Animator anim,anim2,playerAnim;
	public ParticleSystem tensedEmoji, sadEmoji, HappyEmoji;
	
	Queue<string> sentences;
	 
	bool playedPat,playedPat2,playedPat3;
	
	
	void Start()
	{
		sentences = new Queue<string>();
		gm = FindObjectOfType<GameManager>();
		
		win = gm.wonGame;
		won = gm.won;
		failed = gm.fail;
	}
	
	void Update()
	{
		if(!firstdtTriggered && gm.convoNpcReady)
		{
			if(firstDialogue)
			{
				Invoke("TriggerDialogue", 2f);
			}
			firstdtTriggered = true;
		}
	}
	
	public void StartDialogue(Dialogue dialogue)
	{
		//set the animator for starting dialogue here
		if(TENSED)
		{
			if(!playedPat)
			{
				tensedEmoji.Play();
				playedPat = true;
			}
		}
		
		if(Happy)
		{
			if(!playedPat2)
			{
				HappyEmoji.Play();
				playerAnim.SetTrigger("isHappy");
				playedPat = true;
			}
		}
		
		if(Sad)
		{
			if(!playedPat3)
			{
				sadEmoji.Play();
				playerAnim.SetTrigger("isSad");
				playedPat = true;
			}
		}
		
		anim.SetBool("Talking", true);
		
		nameText.text = dialogue.name;
		
		sentences.Clear();
		
		foreach(string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}
		
		DisplayNextSentence();
	}
	
	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}
	
	IEnumerator TypeSentence(string sentence)
	{
		dialogueText.text = "";
		
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}
	
	public void WrongOption()
	{
		foreach(ParticleSystem p in failed)
		{
			p.Play();
		}
	}
	
	public void RightOption()
	{
		foreach(ParticleSystem p in won)
		{
			p.Play();
		}
	}
	
	void TriggerDialogue()
	{
		firstdt.TriggerDialogue();
	}
	
	public void closeDialogue()
	{
		anim.SetBool("Talking", false);
		options.SetActive(false);
	}
	
	public void Continue(bool fail)
	{
		if(finalDialogue)
		{
			anim.SetBool("Talking", false);
			if(options != null)
			{
				options.SetActive(false);
			}
			
			if(fail)
			{
				//no payment and confetti
				//PlayerIsSad
				//	sadEmoji.Play();
				//	playerAnim.SetTrigger("isSad");
				
				foreach(ParticleSystem p in failed)
				{
					p.Play();
				}
				
				gm.SecondMission.SetActive(false);
				Invoke("CameraMove", 1f);
				Invoke("NextTask", 3f);
			}
			else
			{
				//payment and confetti
				//PlayerCelebrates
			
				//HappyEmoji.Play();
				//playerAnim.SetTrigger("isHappy");
			
				foreach(ParticleSystem p in win)
				{
					p.Play();
				}
				
				gm.finalScores[0] += gm.AddedScore;
				gm.finalScores[1] += gm.AddedScore;
				gm.finalScores[2] += gm.AddedScore;
				
				gm.SecondMission.SetActive(false);
				Invoke("CameraMove", 1f);
				Invoke("NextTask", 2f);
			}
		}
	}
	
	void CameraMove()
	{
		gm.convoNpc.SetActive(false);
		gm.CameraAnim.SetBool("GameStart", false);
	}
	
	void NextTask()
	{
		//gm.convoFinish = true;
		gm.startButton.SetActive(true);
		
	}
	
	public void EndDialogue()
	{
		//set the animator for ending dialogue here
		
		if(!finalDialogue && !needOptions)
		{
			anim.SetBool("Talking", false);
			dt.TriggerDialogue();
		}
		else if(needOptions)
		{
			options.SetActive(true);
			continueButton.SetActive(false);
		}
		
	}
}
