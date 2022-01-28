using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Jewel jewelPrefab;
    [SerializeField] Dust dustPrefab;
    public int whatToSpawn; // 0->Jewel; 1->Dust; 2->Both

    // Start is called before the first frame update
    void Start()
    {
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnItem()
    {
        whatToSpawn = Random.Range(0, 3);
        if (whatToSpawn == 0)
        {
            Jewel jewel = Instantiate(jewelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
        else if (whatToSpawn == 1)
        {
            Dust dust = Instantiate(dustPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
        else
        {
            Jewel jewel = Instantiate(jewelPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
            Dust dust = Instantiate(dustPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, transform);
        }
    }
}
