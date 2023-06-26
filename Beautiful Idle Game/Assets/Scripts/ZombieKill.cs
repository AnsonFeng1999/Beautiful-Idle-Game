using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieKill : MonoBehaviour
{
    // Start is called before the first frame update
    public ZombieController controller;

    void Kill()
    {
        controller.KillZombie();
    }
}
