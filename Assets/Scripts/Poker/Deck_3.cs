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

	public Text mText0 = null;
	public Text mText1 = null;
	public Text mText2 = null;
	public Text mText3 = null;

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
			
//		mHandCard1List[0].SetWeight(Weight.Eight);
//		mHandCard1List[1].SetWeight(Weight.Two);
//		mHandCard1List[2].SetWeight(Weight.Eight);
//		mHandCard1List[3].SetWeight(Weight.Five);
//		mHandCard1List[4].SetWeight(Weight.One);

		// 展示;
		CardHelper.SortCards(mHandCard1List);
		SendPoker(mRoot0, mHandCard1List);
		CheckNiu(mHandCard1List, mText0);
		CardHelper.SortCards(mHandCard2List);
		SendPoker(mRoot1, mHandCard2List);
		CheckNiu(mHandCard2List, mText1);
		CardHelper.SortCards(mHandCard3List);
		SendPoker(mRoot2, mHandCard3List);
		CheckNiu(mHandCard3List, mText2);
		CardHelper.SortCards(mHandCard4List);
		SendPoker(mRoot3, mHandCard4List);
		CheckNiu(mHandCard4List, mText3);
	}

	public void CheckNiu(List<Card> cardList, Text text)
	{
		string content = "";
		int iSum_0 = 0;
		int iSum_1 = 0;
		int iSum_2 = 0;
		for(int i = 0; i < cardList.Count; i++)
		{
			int iValue = 0;
			if((int)(cardList[i].CardWeight) < 9)
			{
				iSum_1 += ((int)cardList[i].CardWeight + 1);
				iValue = ((int)cardList[i].CardWeight + 1);
			}
			else
			{
				if((int)(cardList[i].CardWeight) > 9)
				{
					iSum_2++;
				}

				iValue = 10;
			}

			iSum_0 += iValue;
		}

		if (iSum_0 <= 10 && iSum_1 <= 10)
		{
			// 五小牛:五张牌的牌点加起来不超过10，含10;
			Debug.Log("五小牛");
			content = "五小牛";
		}
		else if(iSum_2 == 5)
		{
			// 5张均为花牌jqk;
			Debug.Log("五花牛");
			content = "五花牛";
		}
		else
		{
			int iTemp = iSum_0 % 10;
			if(iTemp == 0)
			{
				Debug.Log("牛牛");
				content = "牛牛";
			}
			else
			{
				int m_one = 0;
				for(int i = 0 ; i < 4 ; i++)
				{
					for(int j = i + 1; j < 5; j++)
					{
						int iValue = (int)cardList[i].CardWeight + 1;
						int jValue = (int)cardList[j].CardWeight + 1;
						if(iValue > 10)
							iValue = 0;
						if(jValue > 10)
							jValue = 0;
						// 判断是否存在;
						if(((int)(iValue + jValue) % 10) == iTemp) 
						{
							//正常情况;
							m_one++;
						}
					}
				}

				if (m_one == 0)
				{
					Debug.Log("无牛");
					content = "无牛";

					// 无牛取最大的那张牌比牌;
				} 
				else 
				{
					Debug.Log("有牛，牛" + iTemp);
					content = "有牛，牛" + iTemp;
				}
			}
		}

		text.text = content;
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
