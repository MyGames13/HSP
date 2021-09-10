using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public bool Chinese;
	public Animator CameraAnim;
	public GameObject startButton,startConvoButton,convoNpc,FirstMission,SecondMission,ThirdMission;
	public GameObject[] Npcs,files;
	public Animator[] anim;
	public bool[] checkAnim;
	public int currentAnim = 0;
	public bool npcReady,Move,GameWon, convoNpcReady;
	public ParticleSystem[] won,fail;
	public float SliderAddValue = 0.3f;
	public Slider levelSlider;
	public ParticleSystem[] wonGame;
	public int totalLevelScoreCheck;
	public Animator congrats;
	public ParticleSystem finalConfetti;
	public float timeTOCONGRAT;
	public string[] finalRemarks;
	public string[] amountGotten;
	public int[] finalScores;
	public Text moneyText;
	public Text remarksText;
	public Text AmountGotten;
	public string failedText = "Failed!";
	public string failedamount = "0";
	public int failedScore = 0;
	public Animator principal;
	public bool principalAnimate;
	public GameObject claimObject,nextButton;
	public bool claimed;
	public Text totalAmountAvailable;
	public Animator fader,patrol,claimAnimation;
	public int AddedScore = 50;
	public bool convoStart, patrolScene,patroling;
	
	public Animator BellAnim;
	
	
	public void StartGame()
	{
		
	    if(!patrolScene)
	    {
	    	CameraAnim.SetBool("GameStart", true);
		    startButton.SetActive(false);
		    if(FirstMission != null)
		    {
			    FirstMission.SetActive(true);
		    }
		    Move = true;
		    npcReady = true;
	    }
	    else if(patrolScene)
	    {
	    	startButton.SetActive(false);
	    	patrol.SetTrigger("p1");
	    }
    }
    
    
	public void StartConversationLevel()
	{
		CameraAnim.SetBool("GameStart", true);
		FirstMission.SetActive(false);
		SecondMission.SetActive(true);
		convoNpcReady = true;
		startConvoButton.SetActive(false);
	}
	
	void ConvoNpcAvailable()
	{
		if(!convoStart)
		{
			convoNpc.SetActive(true);
			convoStart = true;
		}
	}

	void NPCAvailable()
	{
		foreach(GameObject n in Npcs)
		{
			n.SetActive(true);
		}
		
		if(Move)
		{
			if(currentAnim < anim.Length)
			{
				Npcs[currentAnim].GetComponent<MoveNpcs>().ReadyToMove = true;
				anim[currentAnim].SetTrigger("GiveFile");
				//currentAnim ++;
			}
			else
			{
				GameWon = true;
				BellAnim.SetTrigger("BELL");
				Handheld.Vibrate();
				
				if(totalLevelScoreCheck == 3)
				{
					print("You Scored the highest");
					remarksText.text = finalRemarks[0];
					moneyText.text = finalScores[0].ToString();
					AmountGotten.text = amountGotten[0];
				}
				else if(totalLevelScoreCheck == 2)
				{
					print("You Scored the 2nd highest");
					remarksText.text = finalRemarks[1];
					moneyText.text = finalScores[1].ToString();
					AmountGotten.text = amountGotten[1];
				}
				else if(totalLevelScoreCheck == 1)
				{
					print("You Scored Last");
					remarksText.text = finalRemarks[2];
					moneyText.text = finalScores[2].ToString();
					AmountGotten.text = amountGotten[2];
				}
				else if(totalLevelScoreCheck <= 0)
				{
					print("You failed");
					remarksText.text = failedText;
					moneyText.text = failedScore.ToString();
					AmountGotten.text = failedamount;
				}
				
				foreach(ParticleSystem p in wonGame)
				{
					p.Play();
				}
				Invoke("GameWin", 1.5f);
				Move = false;
			}
			Move = false;
		}
	}
	
	void GameWin()
	{
		if(!patrolScene)
		{
			CameraAnim.SetBool("GameStart", false);
		}
		else if(patrolScene)
		{
			patrol.SetTrigger("won");
		}
	}
	
	void Start()
	{
		nextButton.SetActive(false);
		PlayerPrefs.SetInt("Save", SceneManager.GetActiveScene().buildIndex);
	}
	
    void Update()
	{
		totalAmountAvailable.text = PlayerPrefs.GetInt("Amt").ToString();
		
	    if(npcReady)
	    {
	    	Invoke("NPCAvailable", .4f);
	    }
	    
		
			if(convoNpcReady)
			{
				Invoke("ConvoNpcAvailable", .4f);
			}
	    
	    if(GameWon)
	    {
	    	Invoke("Congratulations", timeTOCONGRAT);
	    }
	}
    
	public void GiveScore()
	{
		if(totalLevelScoreCheck == 3)
		{
			PlayerPrefs.SetInt("Amt", PlayerPrefs.GetInt("Amt") + finalScores[0]);
		}
		else if(totalLevelScoreCheck == 2)
		{
			PlayerPrefs.SetInt("Amt", PlayerPrefs.GetInt("Amt") + finalScores[1]);
		}
		else if(totalLevelScoreCheck == 1)
		{
			PlayerPrefs.SetInt("Amt", PlayerPrefs.GetInt("Amt") + finalScores[2]);
		}
		else if(totalLevelScoreCheck <= 0)
		{
			PlayerPrefs.SetInt("Amt", PlayerPrefs.GetInt("Amt") + failedScore);
		}
		claimed = true;
		nextButton.SetActive(true);
	}
    
	public void ClaimMoney()
	{
		claimAnimation.SetTrigger("CLAIMMONEY");
		Destroy(claimObject);
	}
	
	public void MONEYCLAIMED()
	{
		fader.SetTrigger("out");
		Destroy(nextButton);
	}
    
	public void Accept()
	{
		//anim[currentAnim].SetTrigger("HAPPY");
		files[currentAnim].SetActive(false);
		levelSlider.value += SliderAddValue;
		if(patrolScene)
		{
			patroling = true;
		}
		foreach(Animator a in Npcs[currentAnim].GetComponentsInChildren<Animator>())
		{
			a.SetTrigger("HAPPY");
		}
		if(checkAnim[currentAnim])
		{
			print("You Got It");
			totalLevelScoreCheck++;
			foreach(ParticleSystem p in won)
			{
				p.Play();
			}
		}
		else if(!checkAnim[currentAnim])
		{
			print("You Failed");
			foreach(ParticleSystem p in fail)
			{
				p.Play();
			}
		}
	}
	
	public void Decline()
	{
		//anim[currentAnim].SetTrigger("SAD");
		files[currentAnim].SetActive(false);
		levelSlider.value += SliderAddValue;
		if(patrolScene)
		{
			patroling = true;
		}
		foreach(Animator a in Npcs[currentAnim].GetComponentsInChildren<Animator>())
		{
			a.SetTrigger("SAD");
		}
		if(!checkAnim[currentAnim])
		{
			print("You Got It");
			totalLevelScoreCheck++;
			foreach(ParticleSystem p in won)
			{
				p.Play();
			}
		}
		else if(checkAnim[currentAnim])
		{
			print("You Failed");
			foreach(ParticleSystem p in fail)
			{
				p.Play();
			}
		}
	}
	
	public void Congratulations()
	{
		congrats.SetTrigger("congrats");
		finalConfetti.Play();
		principalAnimate = true;
		if(totalLevelScoreCheck == 3)
		{
			if(principalAnimate)
			{
				principal.SetTrigger("Happy");
				principalAnimate = false;
			}
		}
		else if(totalLevelScoreCheck <= 0)
		{
			finalConfetti.Stop();
			if(principalAnimate)
			{
				principal.SetTrigger("Sad");
				principalAnimate = false;
			}
		}
	}
}
