using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    [SerializeField] public Transform target; // Assign "target" for which the snake's head copies
    [SerializeField] float distanceBetween = .2f;
    [SerializeField] public List<GameObject> bodyParts = new List<GameObject>();
    List<GameObject> snakeBody = new List<GameObject>();

    float countUp = 0;
    // Start is called before the first frame update
    void Start()
    {
        CreateBodyParts();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageSnakeBody();
        SnakeMovement();
    }

    void ManageSnakeBody()
    {
        if (bodyParts.Count > 0)
        {
            CreateBodyParts();
        }

        for (int i = 0; i < snakeBody.Count; i++)
        {
            if (snakeBody[i] == null)
            {
                snakeBody.RemoveAt(i);
                i = i - 1;
            }
        }
        if (snakeBody.Count == 0)
            Destroy(this);
    }

    void SnakeMovement()
    {
            transform.position = target.position;
            transform.rotation = target.rotation;

            if (snakeBody.Count > 1)
        {
                for (int i = 1; i < snakeBody.Count; i++)
                {
                    MarkerManager markM = snakeBody[i - 1].GetComponent<MarkerManager>();
                    snakeBody[i].transform.position = markM.markerList[0].position;
                    snakeBody[i].transform.rotation = markM.markerList[0].rotation;
                    markM.markerList.RemoveAt(0);
                }
            }
    }
    void CreateBodyParts()
    {
        if (snakeBody.Count == 0)
        {
            // This spawns the head of the snake
            GameObject temp1 = Instantiate(bodyParts[0], transform.position, transform.rotation, transform);
            if (!temp1.GetComponent<MarkerManager>())
                temp1.AddComponent<MarkerManager>();
            if (!temp1.GetComponent<Rigidbody2D>())
            {
                //temp1.AddComponent<Rigidbody2D>(); // Possibly pointless
                temp1.AddComponent<CapsuleCollider>(); // Possibly pointless
                //temp1.GetComponent<Rigidbody2D>().gravityScale = 0; // Possibly pointless
            }
            if (!temp1.GetComponent<CapsuleCollider>()) // Possibly pointless
            {
                temp1.AddComponent<CapsuleCollider>(); // Possibly pointless
            }
            temp1.name = temp1.name.Replace("(Clone)", "").Trim(); // Remove "(Clone)" from snake part prefabs as they spawn
            snakeBody.Add(temp1);
            bodyParts.RemoveAt(0);
        }
        MarkerManager markM = snakeBody[snakeBody.Count - 1].GetComponent<MarkerManager>();
        if (countUp == 0)
        {
            markM.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            // This spawns the rest of the snake
            GameObject temp = Instantiate(bodyParts[0], markM.transform.position, markM.transform.rotation, transform);
            if (!temp.GetComponent<MarkerManager>())
                temp.AddComponent<MarkerManager>();
            if (!temp.GetComponent<Rigidbody2D>())
            {
                //temp.AddComponent<Rigidbody2D>(); // Possibly pointless
                temp.AddComponent<CapsuleCollider>(); // Possibly pointless
                //temp.GetComponent<Rigidbody2D>().gravityScale = 0; // Possibly pointless
            }
            if (!temp.GetComponent<CapsuleCollider>()) // Possibly pointless
            {
                temp.AddComponent<CapsuleCollider>(); // Possibly pointless
            }
            temp.name = temp.name.Replace("(Clone)", "").Trim(); // Remove "(Clone)" from snake part prefabs as they spawn
            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }

    public void AddBodyParts(GameObject obj)
    {
        bodyParts.Add(obj);
    }
}