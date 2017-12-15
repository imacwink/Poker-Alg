using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card 
{
	public Weight CardWeight { get; private set; }
	public Suits CardSuits { get; private set; }

	public Card(Weight weight, Suits suits)
	{
		this.CardWeight = weight;
		this.CardSuits = suits;
	}

	public void SetWeight(Weight weight)
	{
		this.CardWeight = weight;
	}

	public bool Equals(Card other)
	{
		return this.CardWeight == other.CardWeight && this.CardSuits == other.CardSuits;
	}

	public string GetName()
	{
		return this.CardSuits == Suits.None ? this.CardWeight.ToString() : (this.CardSuits.ToString() + this.CardWeight.ToString());
	}
}