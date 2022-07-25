using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecklessGoalPostSpawner : MonoBehaviour
{
    [SerializeField] GameObject goalPostObj;

    [SerializeField] List<Transform> topSpawnPositions;
    [SerializeField] List<Transform> bottomSpawnPositions;

    [SerializeField] float moveSpeed;

    GameObject spawnedPost = null;
    bool spawnInTopSide = true;

    Vector3 point1, point2;

    public static RecklessGoalPostSpawner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnGoalPost();
    }
    private void Update()
    {
        if(spawnedPost != null)
            spawnedPost.transform.position = Vector3.Lerp(point1, point2, Mathf.PingPong(Time.time * moveSpeed, 1.0f)); //
    }
    public void SpawnGoalPost()
    {
        int randomIndex = -1;
        Transform spawnTransform;

        //Spawn randomly on top and bottom side of the field
        if (spawnInTopSide)
        {
            randomIndex = Random.Range(0, topSpawnPositions.Count);
            spawnTransform = topSpawnPositions[randomIndex];
            //spawnTransform.Rotate(Vector3.up, 180);
        }
        else
        {
            randomIndex = Random.Range(0, bottomSpawnPositions.Count);
            spawnTransform = bottomSpawnPositions[randomIndex];
            //spawnTransform.Rotate(Vector3.up, -180);
        }

        //Spawn first post
        if (spawnedPost == null)
        {
            spawnedPost = Instantiate(goalPostObj, spawnTransform.position, spawnTransform.rotation);
        }
        //Re-position existing post
        else
        {
            spawnedPost.transform.position = spawnTransform.position;
            spawnedPost.transform.rotation = spawnTransform.rotation;
        }

        if (spawnInTopSide)
        {
            if(randomIndex >= 0 && randomIndex<3)
            {
                point1 = topSpawnPositions[0].position;
                point2 = topSpawnPositions[2].position;
            }
            else
            {
                point1 = topSpawnPositions[3].position;
                point2 = topSpawnPositions[5].position;
            }      
        }
        else
        {
            if (randomIndex >= 0 && randomIndex < 3)
            {
                point1 = bottomSpawnPositions[0].position;
                point2 = bottomSpawnPositions[2].position;
            }
            else
            {
                point1 = bottomSpawnPositions[3].position;
                point2 = bottomSpawnPositions[5].position;
            }
        }

        spawnInTopSide = !spawnInTopSide;
    }
}
