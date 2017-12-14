using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deck_1 : MonoBehaviour
{
	public GameObject mRoot = null;
	public GameObject mPrefabObj = null;
	public Button mBackButton = null;

	void Start () 
	{
		mBackButton.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("Boot");
		});

		List<Card> cardList = CardHelper.GetCardShuffleList();

		// 测试排序;
		CardHelper.SortCards(cardList);

		// Demo 显示牌信息;
		for(int i = 0; i < cardList.Count; i++)
		{
			GameObject ruleItemObj = GameObject.Instantiate(mPrefabObj) as GameObject;
			if(ruleItemObj != null)
			{
				ruleItemObj.SetActive(true);

				ruleItemObj.transform.parent = mRoot.transform;
				ruleItemObj.transform.localPosition = Vector3.zero;
				ruleItemObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				ruleItemObj.name = i.ToString();

				Image image = ruleItemObj.GetComponent<Image>();
				if(image != null)
				{
					image.sprite = ResLoad.GetCardSprite(cardList[i].CardSuits, cardList[i].CardWeight);
				}
			}
		}
	}
}
