using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPooler : MonoBehaviour
{

 

    [System.Serializable]
    public class Pool
    {
        [SerializeField]
        string tag;
        [SerializeField]
        GameObject platformPrefab;
        [SerializeField]
        int size = 10;

        public int ReturnPoolSize()
        {
            return size;
        }
        public GameObject ReturnPlatformPrefab()
        {
            return platformPrefab;
        }
        public string ReturnPlatformTag()
        {
            return tag;
        }
    }


    [SerializeField]
    Transform platformSpawnPosition;

    [SerializeField]
    float spawnRate = 3f;

    [SerializeField]
    List<Pool> pools;

    [SerializeField]
    List<GameObject> platformInstance;

    

    
    int amountOfActivePlatforms = 0;


    void OnEnable()
    {
        platformInstance = new List<GameObject>();
        foreach (Pool pool in pools)
        {
            for (int i = 0; i < pool.ReturnPoolSize(); i++)
            {
                GameObject obj = Instantiate(pool.ReturnPlatformPrefab());
                platformInstance.Add(obj);
                obj.SetActive(false);

            }
        }
        StartCoroutine(SpawnPlatforms());
    }


   
    IEnumerator SpawnPlatforms()
    {
        //iterate through and add up all active platforms
        for (int i = 0; i < platformInstance.Count; i++)
        {
            if (platformInstance[i].activeSelf)
            {
                amountOfActivePlatforms++;
            }

        }
        
        //if the count of platforms is larger than current pool, add to the pool
        if (amountOfActivePlatforms > platformInstance.Count - 1)
        {

            foreach (Pool pool in pools)
            {
                for (int i = 0; i < pool.ReturnPoolSize(); i++)
                {
                    GameObject obj = Instantiate(pool.ReturnPlatformPrefab());
                    platformInstance.Add(obj);
                    obj.SetActive(false);
                }
            }
        }
        //reset counter for iteration pool
        amountOfActivePlatforms = 0;

        int pickRandomPlatform = Random.Range(0, platformInstance.Count);

        if (!platformInstance[pickRandomPlatform].activeSelf)
        {
            platformInstance[pickRandomPlatform].SetActive(true);
            platformInstance[pickRandomPlatform].transform.position = platformSpawnPosition.transform.position;

        }
        else if (platformInstance[pickRandomPlatform].activeSelf)
        {

            Debug.Log("picking new random platform");

            //randomly select a platform until an inactive platform is selected
            while (platformInstance[pickRandomPlatform].activeSelf)
            {
                
                pickRandomPlatform = Random.Range(0, platformInstance.Count);
                
            }
            Debug.Log($"set next platform to {platformInstance[pickRandomPlatform]}");

            platformInstance[pickRandomPlatform].SetActive(true);
            platformInstance[pickRandomPlatform].transform.position = platformSpawnPosition.transform.position;

        }
        else
        {
            Debug.LogError("No Inactive Platforms available");
            yield break;
        }
      
        //TO-DO: take hard coded value out and replace with the modifier
        yield return new WaitForSeconds(spawnRate);

        StartCoroutine(SpawnPlatforms());
    }

    void OnDisable()
    {
        StopCoroutine(SpawnPlatforms());
        foreach(GameObject platform in platformInstance)
        {
            Destroy(platform);
        }
        platformInstance = new List<GameObject>();
    }

}
