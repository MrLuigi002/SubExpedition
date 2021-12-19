using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveInputVector;
    Vector2 positionInputVector;

    [SerializeField] private float moveTime = 0.5f;

    [SerializeField] private int movementLimit = 3;

    public int movementLimitZ = 0;

    int screenHeight, screenWidth;

    public int currentMaxScore;

    bool dead = false;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip[] moveSounds;
    [SerializeField] private AudioClip explodeSound;

    void Start()
    {
        
    }

    void Update()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        if(transform.position.z > 3 && PlayerPrefs.GetInt("HideTutorial") == 0)
        {
            gameManager.HideTutorial();
        }
    }

    public void MoveControllerInput(InputAction.CallbackContext ctx)
    {
        moveInputVector = ctx.ReadValue<Vector2>();

        Move();
        //print(moveInputVector);
    }

    public void MoveTouchInput(string input)
    {
        if(input == "up")
        {
            moveInputVector = new Vector2(0, 1);

            print("arriba");
        }

        if (input == "down")
        {
            moveInputVector = new Vector2(0, -1);

            print("abajo");
        }

        if (input == "left")
        {
            moveInputVector = new Vector2(-1, 0);

            print("izquierda");
        }

        if (input == "right")
        {
            moveInputVector = new Vector2(1, 0);

            print("derecha");
        }

        Move();
    }

    void Move()
    {
        if (!dead)
        {
            StartCoroutine(MoveOverSeconds());
            MakeMoveSound();
        }
    }

    public IEnumerator MoveOverSeconds()
    {
        if(moveInputVector.x == -1 && transform.position.x == movementLimit * -1)
        {
            yield break;
        }

        if (moveInputVector.x == 1 && transform.position.x == movementLimit)
        {
            yield break;
        }

        if(moveInputVector.y == -1 && transform.position.z <= movementLimitZ)
        {
            yield break;
        }

        Vector3 moveVector = new Vector3(moveInputVector.x, 0, moveInputVector.y);

        float elapsedTime = 0;
        Vector3 startingPos = transform.position;

        Vector3 finalVector = startingPos + moveVector;

        finalVector = new Vector3(Mathf.Round(finalVector.x), 0, Mathf.Round(finalVector.z));

        while (elapsedTime < moveTime)
        {
            transform.position = Vector3.Lerp(startingPos, finalVector, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = finalVector;

        DoAnimation();

        CalculateScore();
    }

    public void HitByObstacle()
    {
        dead = true;

        audioSource.PlayOneShot(explodeSound);

        gameManager.GameOver();
    }

    void CalculateScore()
    {
        if(Mathf.RoundToInt(transform.position.z) > currentMaxScore)
        {
            currentMaxScore = Mathf.RoundToInt(transform.position.z);
        }
    }

    void DoAnimation()
    {
        if(moveInputVector.x == -1)
        {
            playerAnimator.SetTrigger("MoveLeft");
        }

        if (moveInputVector.x == 1)
        {
            playerAnimator.SetTrigger("MoveRight");
        }
    }

    void MakeMoveSound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.PlayOneShot(moveSounds[Random.Range(0, moveSounds.Length)]);
    }

    //void TouchValue()
    //{
    //    int screenCenterX = screenWidth / 2;
    //    int screenCenterY = screenHeight / 2;

    //    if(positionInputVector.x > screenCenterX) // Franja derecha
    //    {
    //        if(positionInputVector.y > screenCenterY) // Franja superior
    //        {
    //            if(screenCenterX - positionInputVector.x < screenCenterY - positionInputVector.y)
    //            {
    //                Debug.Log("Move right");
    //            }

    //            else
    //            {
    //                Debug.Log("Move up (right)");
    //            }
    //        }

    //        else if(positionInputVector.y > screenCenterY) // Franja inferior
    //        {

    //        }

    //        //if(screenCenterY - positionInputVector.y < screenCenterX - positionInputVector.x)
    //        //{
    //        //    Debug.Log("Move right");
    //        //}

    //        //else
    //        //{
    //        //    if (positionInputVector.y > screenCenterY)
    //        //    {
    //        //        Debug.Log("Move up (right)");
    //        //    }

    //        //    else if (positionInputVector.y < screenCenterY)
    //        //    {
    //        //        Debug.Log("Move down (right)");
    //        //    }
    //        //}
    //    }

    //    else if (positionInputVector.x < screenCenterX) // Franja izquierda
    //    {
    //        //if (positionInputVector.y - screenCenterY > positionInputVector.x - screenCenterX)
    //        //{
    //        //    Debug.Log("Move left");
    //        //}

    //        //else
    //        //{
    //        //    if (positionInputVector.y > screenCenterY)
    //        //    {
    //        //        Debug.Log("Move up (left)");
    //        //    }

    //        //    else if (positionInputVector.y < screenCenterY)
    //        //    {
    //        //        Debug.Log("Move down (left)");
    //        //    }
    //        //}
    //    }

    //    //if(positionInputVector.y > screenCenterY)
    //    //{
    //    //    if (positionInputVector.y - screenCenterY < positionInputVector.x - screenCenterX)
    //    //    {
    //    //        print("Move up");
    //    //    }
    //    //}

    //    //if(positionInputVector.y < screenCenterY)
    //    //{
    //    //    if (positionInputVector.y - screenCenterY < positionInputVector.x - screenCenterX)
    //    //    {
    //    //        print("Move down");
    //    //    }
    //    //}
    //}
}
