using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour 
{
	public Button mButton1 = null;

	void Start () 
	{
		mButton1.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("1");
		});
	}
}
