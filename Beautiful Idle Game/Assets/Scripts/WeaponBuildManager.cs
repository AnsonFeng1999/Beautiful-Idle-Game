using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuildManager : MonoBehaviour
{
    public GameObject[] weaponPrefabs;
    
    public void MountWeapons(int towerIndex, OverlayTile overlayTile)
    {
        if (weaponPrefabs[towerIndex].GetComponent<TurretBehavior>().price > GameManager.Instance.currency)
        {
            Debug.Log("Can't Afford.");
            return;
        }

        GameObject turret = Instantiate(weaponPrefabs[towerIndex]);
        turret.GetComponent<TurretBehavior>().WeaponBuild(overlayTile);
        GameManager.Instance.currency -= turret.GetComponent<TurretBehavior>().price;
    }

    public void RemoveWeapons(OverlayTile overlayTile)
    {
        if (overlayTile.turret)
        {
            GameObject turret = overlayTile.turret.gameObject;
            turret.GetComponent<TurretBehavior>().WeaponRemove(overlayTile);
            GameManager.Instance.currency += turret.GetComponent<TurretBehavior>().price;
            Destroy(turret);
        }
    }
}
