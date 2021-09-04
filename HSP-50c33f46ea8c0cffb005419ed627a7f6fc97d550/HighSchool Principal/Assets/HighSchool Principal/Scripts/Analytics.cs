using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using Facebook.Unity;

public class Analytics : MonoBehaviour
{
	void Awake()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			Application.targetFrameRate = 60;
		}
		FB.Init(FBInitCallback);
	}
	void FBInitCallback()
	{
		if(FB.IsInitialized)
		{
			FB.ActivateApp();
		}
	}
	void OnApplicationPause(bool paused)
	{
		if(!paused)
		{
			if(FB.IsInitialized)
			{
				FB.ActivateApp();
			}
		}
	}
	
	void Start()
	{
		GameAnalytics.Initialize(); 
	}
}
