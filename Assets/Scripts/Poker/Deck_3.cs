using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deck_3 : MonoBehaviour
{
	public GameObject mRoot0 = null;
	public GameObject mRoot1 = null;
	public GameObject mRoot2 = null;
	public GameObject mRoot3 = null;

	public GameObject mPrefabObj = null;
	public Button mBackButton = null;

	private List<Card> mCardList = new List<Card>();

	private List<Card> mHandCard1List = new List<Card>();
	private List<Card> mHandCard2List = new List<Card>();
	private List<Card> mHandCard3List = new List<Card>();
	private List<Card> mHandCard4List = new List<Card>();

	void Start () 
	{
		mBackButton.onClick.AddListener(delegate {
			SceneManager.LoadSceneAsync("Boot");
		});

		List<Card> cardList = CardHelper.GetCardShuffleList(false);
		mCardList.Clear();
		mCardList.AddRange(cardList);

		// 玩家轮流发牌;
		int index = 0;
		for (int i = 0; i < 20; i++)
		{
			if (index == 4)
			{
				index = 0;
			}

			Card card = Deal();
			if(index == 0)
				mHandCard1List.Add(card);
			else if(index == 1) 
				mHandCard2List.Add(card);
			else if(index == 2) 
				mHandCard3List.Add(card);
			else if(index == 3) 
				mHandCard4List.Add(card);

			index++;
		}

		// 展示;
		CardHelper.SortCards(mHandCard1List);
		SendPoker(mRoot0, mHandCard1List);
		CardHelper.SortCards(mHandCard2List);
		SendPoker(mRoot1, mHandCard2List);
		CardHelper.SortCards(mHandCard3List);
		SendPoker(mRoot2, mHandCard3List);
		CardHelper.SortCards(mHandCard4List);
		SendPoker(mRoot3, mHandCard4List);
	}

	public Card Deal()
	{
		Card card = mCardList[mCardList.Count - 1];
		mCardList.Remove(card);
		return card;
	}

	public void SendPoker(GameObject rootObj, List<Card> cardList)
	{
		// Demo 显示牌信息;
		for(int i = 0; i < cardList.Count; i++)
		{
			GameObject ruleItemObj = GameObject.Instantiate(mPrefabObj) as GameObject;
			if(ruleItemObj != null)
			{
				ruleItemObj.SetActive(true);

				ruleItemObj.transform.parent = rootObj.transform;
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
