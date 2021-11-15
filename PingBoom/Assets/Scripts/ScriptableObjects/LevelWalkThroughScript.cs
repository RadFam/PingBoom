using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Walkthrough", menuName = "PingBoomItems/Walkthrough", order = 3)]
public class LevelWalkThroughScript : ScriptableObject 
{
	public List<LevelGlobalData> levelWalkthrough;
}

[System.Serializable]
public class LevelGlobalData
{
	public int levelNum;

	// Число перчаток на уровень
	[SerializeField]
	int catchGloves;
	public int CatchGloves
	{
		get {return catchGloves;}
	}

	// Число смен шайб на уровень
	[SerializeField]
	int puckChanges;
	public int PuckChanges
	{
		get {return puckChanges;}
	}

	// Число ударов на уровень
	[SerializeField]
	int puckStrikes;
	public int PuckStrikes
	{
		get {return puckStrikes;}
	}

	// Время на уровень
	[SerializeField]
	float levelTime;
	public float LevelTime
	{
		get {return levelTime;}
	}

	// Минимально необходимое число очков на уровень
	[SerializeField]
	int levelMinScore;
	public int LevelMinScore
	{
		get {return levelMinScore;}
	}

	// Минимально необходимое число звезд на уровень
	[SerializeField]
	int starScore;
	public int StarScore
	{
		get {return starScore;}
	}
}