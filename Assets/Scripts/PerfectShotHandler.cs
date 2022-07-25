using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectShotHandler : MonoBehaviour
{
    [SerializeField] bool isPerfectShot = true;
    [SerializeField] ParticleSystem celebration;

    public static PerfectShotHandler Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "GoalPost")
        {
            isPerfectShot = false;
        }
    }

    public void Celebrate()
    {
        celebration.transform.position = transform.position;
        celebration.Play();
    }

    public void ResetPerfectShot()
    {
        isPerfectShot = true;
    }

    public bool IsPerfectShot
    {
        get
        {
            return isPerfectShot;
        }
    }
}
