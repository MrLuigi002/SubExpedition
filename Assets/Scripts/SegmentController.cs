using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    [SerializeField] private GameObject segmentCollider;

    private void OnEnable()
    {
        segmentCollider.SetActive(true);
    }

    //private void Start()
    //{
    //    StartCoroutine(DisableOffscreen());
    //}

    //IEnumerator DisableOffscreen()
    //{
    //    if(Camera.main.transform.position.z > transform.position.z)
    //    {
    //        gameObject.SetActive(false);
    //    }

    //    yield return new WaitForSeconds(1f);

    //    StartCoroutine(DisableOffscreen());
    //}

    private void Update()
    {
        if (Camera.main.transform.position.z - 30 > transform.position.z)
        {
            gameObject.SetActive(false);
        }
    }
}
