using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PuckPrize", menuName="PingBoomItems/PuckPrize", order=2)]
public class PuckPrizeScript : ScriptableObject 
{
	public List<PuckPrizeInfo> puckPrizes;
}

[System.Serializable]
public class PuckPrizeInfo
{
	[SerializeField]
	string puckName;
	public string PuckName
	{
		get {return puckName;}
	}

	[SerializeField]
	int levelFromAchieve;
	public int LevelFromAchieve
	{
		get {return levelFromAchieve;}
	}

	[SerializeField]
	int levelScrore;
	public int LevelScrore
	{
		get {return levelScrore;}
	}

	[SerializeField]
	int wholeScore;
	public int WholeScore
	{
		get {return wholeScore;}
	}

	[SerializeField]
	float strikePart;
	public float StrikePart
	{
		get {return strikePart;}
	}
}