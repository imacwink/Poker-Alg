using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardHelper
{
	public static int mTotalCardCount = 54; /*扑克牌总数*/
	public static int mDiv = 13;  /*扑克牌四种花色拆分*/

	// 获取权重;
	public static int GetWeight(Card[] cards, CardsType rule)
	{
		int totalWeight = 0;
		if (rule == CardsType.JokerBoom)
		{
			totalWeight = int.MaxValue;
		}
		else if (rule == CardsType.Boom)
		{
			totalWeight = (int)cards[0].CardWeight * (int)cards[1].CardWeight * (int)cards[2].CardWeight * (int)cards[3].CardWeight + (int.MaxValue / 2);
		}
		else if (rule == CardsType.ThreeAndOne || rule == CardsType.ThreeAndTwo)
		{
			for (int i = 0; i < cards.Length; i++)
			{
				if (i < cards.Length - 2)
				{
					if (cards[i].CardWeight == cards[i + 1].CardWeight &&
						cards[i].CardWeight == cards[i + 2].CardWeight)
					{
						totalWeight += (int)cards[i].CardWeight;
						totalWeight *= 3;
						break;
					}
				}
			}
		}
		else
		{
			for (int i = 0; i < cards.Length; i++)
			{
				totalWeight += (int)cards[i].CardWeight;
			}
		}

		return totalWeight;
	}

	// 排序;
	public static void SortCards(List<Card> cards)
	{
		cards.Sort(
			(Card a, Card b) =>
			{
				//先按照权重降序，再按花色升序;
				return -a.CardWeight.CompareTo(b.CardWeight) * 2 + a.CardSuits.CompareTo(b.CardSuits);
			}
		);
	}

	#region 根据Card数据结构洗牌;
	// 返回Card结构的洗牌结果;
	public static List<Card> GetCardShuffleList(bool bJoker = true)
	{
		List<Card> cardList = CreateDeck(bJoker);
		CardShuffle(cardList);
		return cardList;
	}

	// 洗牌;
	public static void CardShuffle(List<Card> cardList)
	{
		System.Random random = new System.Random();
		List<Card> newCards = new List<Card>();
		foreach (var card in cardList)
		{
			newCards.Insert(random.Next(newCards.Count + 1), card);
		}

		cardList.Clear();
		cardList.AddRange(newCards);
	}

	// 创建棋牌;
	private static List<Card> CreateDeck(bool bJoker)
	{
		List<Card> cardList = new List<Card>();

		// 创建普通扑克;
		for (int color = 0; color < 4; color++)
		{
			for (int value = 0; value < CardHelper.mDiv; value++)
			{
				Weight w = (Weight)value;
				Suits s = (Suits)color;
				Card card = new Card(w, s);
				cardList.Add(card);
			}
		}

		if(bJoker)
		{
			// 创建大小王扑克;
			cardList.Add(new Card(Weight.SJoker, Suits.None));
			cardList.Add(new Card(Weight.LJoker, Suits.None));	
		}

		return cardList;
	}
	#endregion

	#region 根据数据码洗牌;
	// 返回洗牌后的数据;
	public static List<Card> GetCodeShuffleList()
	{
		return ConvertCard(CodeShuffle(CardHelper.mTotalCardCount));
	}

	// 根据数据码洗牌;
	public static int[] CodeShuffle(int iPokerCnt)
	{
		// 初始化;
		int[] cardArray = new int[iPokerCnt];

		// 排序;
		for (int i = 0; i < cardArray.Length; i++)
			cardArray[i] = i;

		// 洗牌;
		for (int i = 0; i < cardArray.Length; i++)
		{
			int j = Random.Range(i, cardArray.Length);
			int temp = cardArray[i];
			cardArray[i] = cardArray[j];
			cardArray[j] = temp;
		}

		return cardArray;
	}

	// 将数据转换成Card;
	private static List<Card> ConvertCard(int[] cardArray)
	{
		List<Card> cardList = new List<Card>();
		for(int i = 0; i < cardArray.Length; i++)
		{
			Card card = new Card(GetCardWeight(cardArray[i] + 1), GetCardSuit(cardArray[i] + 1));
			cardList.Add(card);
		}

		return cardList;
	}

	// 根据数据码获取牌的类型;
	public static Suits GetCardSuit(int iValueCode)
	{
		Suits enSuit = Suits.None;
		if(iValueCode >= mDiv * 3 + 1 && iValueCode <= mDiv * 4)
			enSuit = Suits.Club;
		else if(iValueCode >= mDiv * 2 + 1 && iValueCode <= mDiv * 3)
			enSuit = Suits.Diamond;
		else if(iValueCode >= mDiv + 1 && iValueCode <= mDiv * 2)
			enSuit = Suits.Heart;
		else if(iValueCode >= 1 && iValueCode <= mDiv)
			enSuit = Suits.Spade;
		else if(iValueCode >= mTotalCardCount - 1 && iValueCode <= mTotalCardCount)
			enSuit = Suits.None;
		return enSuit;
	}

	public static Weight GetCardWeight(int iValueCode)
	{
		Weight retWeight = Weight.One;
		int iNum = 0;
		if(iValueCode >= (mTotalCardCount - 1))
		{
			iNum = iValueCode;
		}
		else
		{
			iNum = iValueCode % mDiv;
			if(iNum == 0)
				iNum = mDiv;
		}

		switch(iNum)
		{
		case 1:
			retWeight = Weight.One;
			break;
		case 2:
			retWeight = Weight.Two;
			break;
		case 3:
			retWeight = Weight.Three;
			break;
		case 4:
			retWeight = Weight.Four;
			break;
		case 5:
			retWeight = Weight.Five;
			break;
		case 6:
			retWeight = Weight.Six;
			break;
		case 7:
			retWeight = Weight.Seven;
			break;
		case 8:
			retWeight = Weight.Eight;
			break;
		case 9:
			retWeight = Weight.Nine;
			break;
		case 10:
			retWeight = Weight.Ten;
			break;
		case 11:
			retWeight = Weight.Jack;
			break;
		case 12:
			retWeight = Weight.Queen;
			break;
		case 13:
			retWeight = Weight.King;
			break;
		case 53:
			retWeight = Weight.LJoker;
			break;
		case 54:
			retWeight = Weight.SJoker;
			break;
		}

		return retWeight;
	}
	#endregion
}
