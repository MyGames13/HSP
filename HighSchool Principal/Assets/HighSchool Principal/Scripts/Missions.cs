using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
	public GameObject[] files;
	public bool adder,done;
	GameManager gm;
	
	
	void Awake()
	{
		gm = FindObjectOfType<GameManager>();
	}
	
	void Start()
	{
		files = gm.files;
	}
	
	public void StartMission()
	{
		files[gm.currentAnim].SetActive(true);
		//gm.currentAnim++;
	}
	
	public void GMOVE()
	{
		if(adder && !gm.patroling)
		{
			gm.Move = true;
		}
	}
	
	public void happy()
	{
		if(!done)
		{
			
			if(adder)
			{
				gm.Npcs[gm.currentAnim].GetComponent<MoveNpcs>().win = true;
				gm.currentAnim++;
			}
			done = true;
		}
		//gm.Npcs[gm.currentAnim].GetComponent<MoveNpcs>().ReadyToMove = true;
		//gm.Move = true;
	}
	
	public void sad()
	{
		if(!done)
		{
		
			if(adder)
			{
				gm.Npcs[gm.currentAnim].GetComponent<MoveNpcs>().fail = true;
				gm.currentAnim++;
			}
			done = true;
		}
		
	}
	
}
