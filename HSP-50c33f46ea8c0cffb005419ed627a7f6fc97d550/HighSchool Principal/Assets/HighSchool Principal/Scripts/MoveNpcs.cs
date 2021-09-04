using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNpcs : MonoBehaviour
{
	public Transform startMarker;
	public Transform endMarker;
	
	public Transform winPosition, failPosition;

	// Movement speed in units per second.
	public float speed = 1.0F;
	
	public bool ReadyToMove,win,fail;

	// Time when the movement started.
	private float startTime;

	// Total distance between the markers.
	private float journeyLength;
	GameManager gm;

	void Start()
	{
		gm = FindObjectOfType<GameManager>();
		startMarker = transform;
		// Keep a note of the time the movement started.
		startTime = Time.time;

		// Calculate the journey length.
		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
	}

	// Move to the target end position.
	void Update()
	{
	
		if(ReadyToMove)
		{
			// Distance moved equals elapsed time times speed..
			float distCovered = (Time.time - startTime) * speed;

			// Fraction of journey completed equals current distance divided by total distance.
			float fractionOfJourney = distCovered / journeyLength;

			// Set our position as a fraction of the distance between the markers.
			transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
			startMarker = transform;
			//ReadyToMove = false;
		}
		
		
		if(win && !fail)
		{
			endMarker = winPosition;
			startTime = Time.time;

			// Calculate the journey length.
			journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
			
			if(endMarker.position == startMarker.position)
			{
				//gm.currentAnim++;
				//gm.Move = true;
			}
			win = false;
		}
		
		if(fail && !win)
		{
			endMarker = failPosition;
			startTime = Time.time;

			// Calculate the journey length.
			journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
			
			if(endMarker.position == startMarker.position)
			{
				//gm.currentAnim++;
				//gm.Move = true;
			}
			
			fail = false;
		}
	}
}
