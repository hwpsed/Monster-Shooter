using System.Collections.Generic;
using UnityEngine;

public class ListContent : MonoBehaviour
{
	public GameObject prefab; // This is our prefab object that will be exposed in the inspector
	public List<WeaponPrefabController> listWeaponPrefab = new List<WeaponPrefabController>();
	public GameController gameController;
	public void Start()
	{
		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	public void Populate(List<Gun> listCanon, Gun currentGun)
	{
		GameObject tempPref; // Create GameObject instance
		WeaponPrefabController prefScript;

		foreach (Gun canon in listCanon)
		{
			// Create new instances of our prefab until we've created as many as we specified
			tempPref = Instantiate(prefab, transform);
			prefScript = tempPref.GetComponent<WeaponPrefabController>();
			prefScript.Initialize(canon, this);

			if (canon.Equals(currentGun))
			{
				prefScript.setEquipAvailable(false); // This gun's already been equipped
			}

			listWeaponPrefab.Add(prefScript);
		}
	}

	private void changeEquippedStatusOn()
	{
		foreach (WeaponPrefabController weapon in listWeaponPrefab)
		{
			weapon.setEquipAvailable(true);
		}
	}

	public void changeCanon(WeaponPrefabController weapon)
	{
		changeEquippedStatusOn();
		weapon.setEquipAvailable(false);
		gameController.changeCanon(weapon.canon);
	}
}