using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
	public string SceneToLoad;
	
	
	public void SceneLoad()
	{
		if(PlayerPrefs.GetInt("Save") > 0)
		{
			SceneManager.LoadScene(PlayerPrefs.GetInt("Save"));
		}
		else
		{
			SceneManager.LoadScene(SceneToLoad);
		}
	}
}
