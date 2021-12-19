using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject playerDestroy;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            playerController.HitByObstacle();

            playerDestroy.SetActive(true);

            gameObject.SetActive(false);
        }
    }
}
