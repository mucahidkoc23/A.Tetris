using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] blocks;
    void Start()
    {
        spawn();
        //InvokeRepeating("spawn", 2, 5);
    }

    // Update is called once per frame
    void Update()
    {   
        
        
    }
   public void spawn()
    {
        int index = Random.Range(0, blocks.Length);
        Instantiate(blocks[index], transform.position,Quaternion.identity);
    }
}
