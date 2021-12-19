using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float startEndPos, moveSpeed;

    Vector3 vectorEndPos, vectorStartPos;


    void Start()
    {
        
    }

    private void OnEnable()
    {
        //if(Random.Range(0,2) == 1)
        //{
        //    startEndPos = startEndPos * -1;
        //}

        vectorStartPos = new Vector3(startEndPos, transform.localPosition.y, 0);

        vectorEndPos = new Vector3(vectorStartPos.x * -1, transform.localPosition.y, 0);

        transform.localPosition = vectorStartPos;

        StartCoroutine(MoveOverSeconds());
    }

    void Update()
    {
        
    }

    public IEnumerator MoveOverSeconds()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.localPosition;
        while (elapsedTime < moveSpeed)
        {
            transform.localPosition = Vector3.Lerp(startingPos, vectorEndPos, (elapsedTime / moveSpeed));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = vectorEndPos * -1;

        Destroy(gameObject);
    }
}
