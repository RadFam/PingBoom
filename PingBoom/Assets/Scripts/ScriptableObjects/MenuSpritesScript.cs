using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuSprites", menuName = "PingBoomItems/MenuSprites", order = 4)]
public class MenuSpritesScript : ScriptableObject 
{
	public Sprite blueChkBox;	
	public Sprite redChkBox;
	public Sprite yellowChkBox;
	public Sprite greenChkBox;
	public Sprite greyBox;

	public List<Sprite> starResults;
}
