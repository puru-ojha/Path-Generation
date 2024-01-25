using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonSpawner : MonoBehaviour
{
    
    //This script will take an index for hexagonPrefabs and take location and direction from other scripts then it will generate the 
    //expected hexagon at their appropriate place.
    public GameObject[] hexagonPrefabs;
    int hexagonIndex;
    Vector3 hexagonPosition;
    Vector3 hexagonDirection;
    RayCheckerTwo raychecker;
    public int randomIndex;
    public Queue<GameObject> hexagonQueue = new Queue<GameObject>();
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        raychecker = gameObject.GetComponent<RayCheckerTwo>();

    }

    public void SpawnHexagon()
    {
        count = count + 1;
        hexagonIndex = raychecker.indexChosen;
        hexagonPosition = raychecker.playerPosition;
        hexagonDirection = raychecker.playerDirection;
        GameObject currentHexagon = Instantiate(hexagonPrefabs[hexagonIndex], hexagonPosition, Quaternion.Euler(hexagonDirection));
        hexagonQueue.Enqueue(currentHexagon);
        DestroyHexagon();
    }
    void DestroyHexagon()
    {
        if (count >2)
        {
            //Debug.Log("One hexagon should go ");
            Destroy(hexagonQueue.Dequeue());
            count --;
        }
    }
    

}
