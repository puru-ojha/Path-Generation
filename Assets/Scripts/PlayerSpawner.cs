using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    //This script spawns the player at a random location and random direction in the scene.
    public GameObject player;
    public static PlayerSpawner Instance;
    public Vector3 playerPosition;
    public Vector3 playerDirection;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        //playerPosition = new Vector3(Random.Range(-55f, 55f), 0, Random.Range(-55f, 55f));
        playerPosition = new Vector3(Random.Range(-20f, 20f), 0, Random.Range(-20f, 20f));
        playerDirection = new Vector3(0f, Random.Range(0, 359), 0f);
        Instantiate(player, playerPosition, Quaternion.Euler(playerDirection));
        Debug.Log("The player is generated at " + playerPosition + " With the rotation of " + playerDirection + " Along Y axis ");
    }
    
}
