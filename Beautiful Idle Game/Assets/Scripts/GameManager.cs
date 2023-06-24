using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    public GameObject ZombieContainer;
    public GameObject ZombiePrefab;
    public float currency;

    private void Awake()
    {
        instance = this;
    }

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
        
        foreach (KeyValuePair<Vector2Int,OverlayTile> pair in mapKey)
        {
            // randomly spawn a zombie for each tile with
            if (Random.value <= 0.05f)
            {
                var zombie = Instantiate(ZombiePrefab, ZombieContainer.transform);
                var controller = zombie.GetComponent<ZombieController>();
                var tile = pair.Value.GetComponent<OverlayTile>();
                controller.PositionCharacter(tile, true);
            }
        }
    }
}
