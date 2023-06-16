using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public GameObject ZombiePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        if (MapManager.Instance == null)
        {
            Debug.LogError("Error: Map Manager Null");
            return;
        }
        var mapKey = MapManager.Instance.map;
        
        // randomly spawn Zombies
        
        foreach (KeyValuePair<Vector2Int,GameObject> pair in mapKey)
        {
            // randomly spawn a zombie for each tile with
            if (Random.value <= 0.05f)
            {
                var zombie = Instantiate(ZombiePrefab);
                var controller = zombie.GetComponent<ZombieController>();
                var tile = pair.Value.GetComponent<OverlayTile>();
                controller.PositionCharacter(tile, true);
            }
        }
    }
}
