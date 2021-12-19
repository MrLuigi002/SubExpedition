using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    public List<GameObject> pooledSegments;
    public GameObject[] segmentToPool;
    public int amountSegmentsToPool;

    [SerializeField] private Transform segmentParent;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledSegments = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountSegmentsToPool; i++)
        {
            tmp = Instantiate(segmentToPool[i], segmentParent);
            tmp.SetActive(false);
            pooledSegments.Add(tmp);
        }
    }

    public GameObject GetPooledSegment()
    {
        for (int i = 0; i < amountSegmentsToPool; i++)
        {
            if (!pooledSegments[i].activeInHierarchy)
            {
                return pooledSegments[i];
            }
        }

        return null;
    }
}

