using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour 
{
	public Button mButton1 = null;
	public Button mButton2 = null;
	public Button mButton3 = null;
	public Button mButton4 = null;

	void Start () 
	{
		mButton1.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("1");
		});

		mButton2.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("2");
		});

		mButton3.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("3");
		});

		mButton4.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("4");
		});
	}
}
