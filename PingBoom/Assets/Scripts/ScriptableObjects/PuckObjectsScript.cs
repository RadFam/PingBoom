using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="PuckContainer", menuName="PingBoomItems/PuckContainer", order=1)]
public class PuckObjectsScript : ScriptableObject 
{
	[SerializeField]
	List<PuckData> allPucks;
	[SerializeField]
	PuckData currData;
	int currIndex;

	public void CreateItem()
	{
		if (allPucks == null)
		{
			allPucks = new List<PuckData>();
		}

		PuckData PD = new PuckData();
		allPucks.Add(PD);
		currData = PD;
		currIndex = allPucks.Count - 1;
	}

	public void RemoveItem()
	{
		if (allPucks == null)
			return;
		if (currData == null)
			return;

		allPucks.Remove(currData);
		if (allPucks.Count > 0)
		{
			currData = allPucks[0];
		}
		else
		{
			CreateItem();
		}
		currIndex = 0;
	}

	public void NextItem()
	{
		if (currIndex < allPucks.Count-1)
		{
			currIndex++;
		}
		else
		{
			currIndex = 0;
		}
		currData = allPucks[currIndex];
	}

	public void PrevItem()
	{
		if (currIndex > 0)
		{
			currIndex--;
		}
		else
		{
			currIndex = allPucks.Count;
		}
		currData = allPucks[currIndex];
	}

	public PuckData GetPuckByName(string findName)
	{
		return allPucks.Find(x => x.Name == findName);
	}

	public PuckData GetPuckByNum(int num)
	{
		if (num >=0 && num < allPucks.Count)
		{
			return allPucks[num];
		}

		return null;
	}
}

[System.Serializable]
public class PuckData
{
	[SerializeField]
	string name;
	[SerializeField]
	string rusName;
	[SerializeField]
	string rusDescription;
	[SerializeField]
	AudioClip strikePuckSound;
	[SerializeField]
	Sprite puckSprite;
	[SerializeField]
	PhysicsMaterial2D puckMaterial;
	[SerializeField]
	float puckFriction;
	[SerializeField]
	float puckStopSpeed;

	public string Name
	{
		get {return name;}
	}
	public string RusName
	{
		get {return rusName;}
	}
	public string RusDescr
	{
		get {return rusDescription;}
	}
	public AudioClip StrikeSound
	{
		get {return strikePuckSound;}
	}
	public Sprite PuckSprite
	{
		get {return puckSprite;}
	}
	public PhysicsMaterial2D PuckMat
	{
		get {return puckMaterial;}
	}
	public float Friction
	{
		get {return puckFriction;}
	}

	public float StopSpeed
	{
		get {return puckStopSpeed;}
	}
}
