using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuckObjectsScript))]
public class EditorPuckScrObj : Editor 
{
	PuckObjectsScript puckObjectsScript;
	void Awake()
	{
		puckObjectsScript = (PuckObjectsScript)target;
	}

	public override void OnInspectorGUI()
	{
		GUILayout.BeginHorizontal();

		if (GUILayout.Button("New item"))
		{
			puckObjectsScript.CreateItem();
		}

		if (GUILayout.Button("Delete item"))
		{
			puckObjectsScript.RemoveItem();
		}

		if (GUILayout.Button("Item ==>"))
		{
			puckObjectsScript.NextItem();
		}

		if (GUILayout.Button("<== Item"))
		{
			puckObjectsScript.PrevItem();
		}

		GUILayout.EndHorizontal();

		base.OnInspectorGUI();
	}
}
