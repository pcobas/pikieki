using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SFXGroup {
	public string groupName;
	public int specificCapAmount;
	
	/// <summary>
	/// Initialize a SFX Group.  'name' is the name of the group, and 'capAmount' is a custom SFX cap for that group.
	/// Use -1 as the cap amount to use the default global cap amount, and use 0 if you don't want the group to use a specific cap amount at all.
	/// The specific cap amount will only be respected when using SoundManager.PlayCappedSFX
	/// </summary>
	public SFXGroup(string name, int capAmount)
	{
		groupName = name;
		specificCapAmount = capAmount;
	}
	
	/// <summary>
	/// Initialize a SFX Group.  'name' is the name of the group
	/// </summary>
	public SFXGroup(string name)
	{
		groupName = name;
		specificCapAmount = 0;
	}
}
