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
        turret.transform.position = new Vector3(overlayTile.transform.position.x,
                                                overlayTile.transform.position.y + 0.0001f,
                                                overlayTile.transform.position.z);
        turret.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        turret.GetComponent<TurretBehavior>().mountLocation = overlayTile;
        overlayTile.isBlocked = true;
        overlayTile.turret = turret.GetComponent<TurretBehavior>();
        GameManager.Instance.currency -= turret.GetComponent<TurretBehavior>().price;
    }
}
