using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
	GameManager gm;
	
	
    void Start()
    {
	    gm = FindObjectOfType<GameManager>();
    }

	public void File()
	{
		gm.Move = true;
		gm.npcReady = true;
	}
	
	public void Patrolling(string npc)
	{
		this.GetComponent<Animator>().SetTrigger(npc);
	}
	
	public void notPatroling()
	{
		gm.patroling = false;
	}
	
	public void move()
	{
		gm.currentAnim++;
		gm.Move = true;
	}
	
	
}
