using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResLoad 
{
	public static int GetWeightNum(Weight weight)
	{
		int iRetValue = 0;
		switch(weight)
		{
		case Weight.One:
			iRetValue = 1;
			break;
		case Weight.Two:
			iRetValue = 2;
			break;
		case Weight.Three:
			iRetValue = 3;
			break;
		case Weight.Four:
			iRetValue = 4;
			break;
		case Weight.Five:
			iRetValue = 5;
			break;
		case Weight.Six:
			iRetValue = 6;
			break;
		case Weight.Seven:
			iRetValue = 7;
			break;
		case Weight.Eight:
			iRetValue = 8;
			break;
		case Weight.Nine:
			iRetValue = 9;
			break;
		case Weight.Ten:
			iRetValue = 10;
			break;
		case Weight.Jack:
			iRetValue = 11;
			break;
		case Weight.Queen:
			iRetValue = 12;
			break;
		case Weight.King:
			iRetValue = 13;
			break;
		case Weight.LJoker:
			iRetValue = 53;
			break;
		case Weight.SJoker:
			iRetValue = 54;
			break;
		}
		return iRetValue;
	}
		
	public static Sprite GetCardSprite(Suits suit, Weight weight)
	{
		Sprite sprite = null;
		int iNum = GetWeightNum(weight);
		string strPath = "";

		switch(suit)
		{
		case Suits.Club:
			strPath = "Poker/" + iNum + "_blossom";
			break;
		case Suits.Diamond:
			strPath = "Poker/" + iNum + "_square";
			break;
		case Suits.Heart:
			strPath = "Poker/" + iNum + "_red";
			break;
		case Suits.Spade:
			strPath = "Poker/" + iNum + "_black";
			break;
		case Suits.None:
			{
				if(iNum == CardHelper.mTotalCardCount - 1)
					strPath = "Poker/joker_s";
				else if(iNum == CardHelper.mTotalCardCount)
					strPath = "Poker/joker_b";
			}
			break;
		}

		return Resources.Load(strPath, typeof(Sprite)) as Sprite;
	}
}
