using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuildManager : MonoBehaviour
{
    [Header("Turret Reference")]
    public GameObject[] weaponPrefabs;
    
    /// <summary>
    /// This method builds the turret on the tile. It instantiates a turret object and links it with the tile.
    /// It also updates the currency UI.
    /// </summary>
    /// <param name="towerIndex">Type of the turret</param>
    /// <param name="overlayTile">Tile the turret will be on</param>
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

    /// <summary>
    /// This method removes and destroys the turret and update currency UI as well
    /// </summary>
    /// <param name="overlayTile">Tile the turret currently on</param>
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