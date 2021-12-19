using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentColliderController : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            gameManager.ExpandLevel();

            gameObject.SetActive(false);
        }
    }
}
