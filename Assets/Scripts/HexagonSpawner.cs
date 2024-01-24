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

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    int GetRandomRayIndex()
    {
        
        int index = raychecker.noHitRayIndex.Count;
        //Debug.Log("The total number of rays not hitting is " + index);
        randomIndex = Random.Range(0, index);
        return raychecker.noHitRayIndex[randomIndex];
    }
    */

    public void SpawnHexagon()
    {
        count = count + 1;
        //Doing the ray check
        //raychecker.CheckForRays();
        hexagonIndex = raychecker.indexChosen;
        //Debug.Log("The hexagonIndex is " + hexagonIndex);
        hexagonPosition = raychecker.playerPosition;
        hexagonDirection = raychecker.playerDirection;
        GameObject currentHexagon = Instantiate(hexagonPrefabs[hexagonIndex], hexagonPosition, Quaternion.Euler(hexagonDirection));
        hexagonQueue.Enqueue(currentHexagon);
        DestroyHexagon();
    }
    void DestroyHexagon()
    {
        //count = count + 1;
        //Debug.Log("The hexagon count is " + count);
        if (count >2)
        {
            Debug.Log("One hexagon should go ");
            Destroy(hexagonQueue.Dequeue());
            count --;
        }
    }
    

}
