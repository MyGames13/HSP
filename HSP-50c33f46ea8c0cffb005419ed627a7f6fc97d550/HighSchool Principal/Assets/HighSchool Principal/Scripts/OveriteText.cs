using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OveriteText : MonoBehaviour
{
	
	GameManager GM;
	
	[TextArea(3, 10)]
	public string trnsltn;
	
	
	public bool ChineseText;
	public DialogueManager dm;
	

	void Awake()
	{
		GM = FindObjectOfType<GameManager>();
		ChineseText = GM.Chinese;
		
	    if(ChineseText && trnsltn != "")
	    {
	    	if(dm == null)
	    	{
	    		this.GetComponent<Text>().text = trnsltn;
	    	}
	    	
	    	if(dm != null)
	    	{
	    		dm.firstdt.dialogue.sentences[0] = trnsltn;
	    	}
	    }
	}
    
	void Update()
	{
		if(ChineseText && trnsltn != "")
		{
			if(dm == null)
			{
				this.GetComponent<Text>().text = trnsltn;
			}
	    	
			if(dm != null)
			{
				dm.firstdt.dialogue.sentences[0] = trnsltn;
			}
		}
	}
}
