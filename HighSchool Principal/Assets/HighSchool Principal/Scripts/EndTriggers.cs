using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggers : MonoBehaviour
{
	public Animator anim1,anim2,anim3;
	public string LevelToLoad;
	GameManager man;
	
	
	void Awake()
	{
		man = FindObjectOfType<GameManager>();
	}
	
	
	
	public void LoadNewScene()
	{
		SceneManager.LoadScene(LevelToLoad);
	}
	
	
	public void GiveMoney()
	{
		man.GiveScore();
	}
	
	public void EOD()
	{
		anim1.SetTrigger("eod");
	}
	
	public void MONEY()
	{
		anim2.SetTrigger("money");
	}
	
	public void COLLECTMONEY()
	{
		anim3.SetTrigger("collectmoney");
	}
}
