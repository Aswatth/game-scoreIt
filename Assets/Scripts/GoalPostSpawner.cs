using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPostSpawner : MonoBehaviour
{
    [SerializeField] GameObject goalPostObj;

    [SerializeField] List<Transform> topSpawnPositions;
    [SerializeField] List<Transform> bottomSpawnPositions;

    GameObject spawnedPost = null;
    bool spawnInTopSide = true;

    public static GoalPostSpawner Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        SpawnGoalPost();
    }
    public virtual GameObject SpawnGoalPost()
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
        if(spawnedPost == null)
        {
            spawnedPost = Instantiate(goalPostObj, spawnTransform.position, spawnTransform.rotation);
        }
        //Re-position existing post
        else
        {
            spawnedPost.transform.position = spawnTransform.position;
            spawnedPost.transform.rotation = spawnTransform.rotation;
        }

        spawnInTopSide = !spawnInTopSide;

        return spawnedPost;
    }
}
