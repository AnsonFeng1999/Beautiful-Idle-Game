using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnContainer : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject ZombieContainer;
    public GameObject[] ZombiePrefab;
    public int maxZombies;
    [SerializeField] private int zombieIndex;   // default to 0
    private int counting;
    // Start is called before the first frame update
    private void Start()
    {
        counting = 0;
    }
    void Update()
    {
        if (MapManager.Instance == null)
        {
            Debug.LogError("Error: Map Manager Null");
            return;
        }
        var map = MapManager.Instance;
       
        // randomly spawn Zombies
        if (GameManager.Instance.AllZombies.Count < maxZombies && spawnPoints.Length > 0)
        {
            // randomly choose a spawnpoint
            int range = Random.Range(0, spawnPoints.Length);
            Debug.Log("rand range " + range);
            Transform randT = spawnPoints[range];
            
            if (counting == maxZombies + 10)
            {
                counting = 0;
                zombieIndex = 1;    // Should be ++ instead of a fixed number, now for testing only
            }
            counting++;

            var zombie = Instantiate(ZombiePrefab[zombieIndex], ZombieContainer.transform);
            var controller = zombie.GetComponent<ZombieController>();
            OverlayTile tile = map.FindNearestTile(randT.position);
            controller.PositionCharacter(tile, true);
            GameManager.Instance.AllZombies.Add(zombie);
            Debug.Log("Spawning zombies at " + zombie.transform.position);
        }
    }
}
