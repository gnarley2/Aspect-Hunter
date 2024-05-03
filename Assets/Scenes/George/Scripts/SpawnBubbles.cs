using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpawnBubbles : MonoBehaviour
{
    [SerializeField] private int _bubblesAmount = 10;
    [SerializeField] private float _startAngle;
    [SerializeField] private float _endAngle;

    private Vector2 bubbleMoveDirection;

    float angle;
    public float time = 0f;
    public float repeatRate = 0f;

    float elapsedTime = 0f;
    float maxTimeInSeconds = 4f;

    void Update()
    {
        if (BossPhase.phase == BossPhase.Phase.Start)
        {
            
            if (IsInvoking("SpawnInCirclePattern"))
            {
                CancelInvoke("SpawnInCirclePattern");
                //StopCoroutine(SpawnInCirclePattern());
            }

            if (!IsInvoking("SpawnInArcPattern"))
            {
                InvokeRepeating("SpawnInArcPattern", time, repeatRate);
            }
            //StartCoroutine(SpawnInArcPattern());
        }

        if (BossPhase.phase == BossPhase.Phase.End)
        {
            if (IsInvoking("SpawnInArcPattern"))
            {
                CancelInvoke("SpawnInArcPattern");
                //StopCoroutine(SpawnInCirclePattern());
            }

            if (!IsInvoking("SpawnInCirclePattern"))
            {
                InvokeRepeating("SpawnInCirclePattern", time, repeatRate);
            }
            //StartCoroutine(SpawnInCirclePattern());
        }
        
        if(!IsInvoking("PrintPhase"))
        {
            StartCoroutine("PrintPhase");
        }

        float currentTime = elapsedTime >= maxTimeInSeconds ? elapsedTime = 0 : elapsedTime += Time.deltaTime;
        Debug.Log($"Current time: {currentTime}");
    }

    IEnumerator PrintPhase()
    {
        Debug.Log($"{BossPhase.phase} phase");
        yield return new WaitForSeconds(maxTimeInSeconds);
    }

    //IEnumerator SpawnInArcPattern()
    //{
    //    //startAngle = 90f;
    //    //endAngle = 270f;

    //    //float angleStep = (endAngle - startAngle) / bubblesAmount;
    //    //angle = startAngle;

    //    //for (int i = 0; i < bubblesAmount + 1; i++)
    //    //{
    //    //    Spawn();

    //    //    angle += angleStep;
    //    //}

    //    Spawn(90f, 270f);
    //    Debug.Log("SpawnInArcPattern");
    //    yield return new WaitForSeconds(1f);
    //}
    //IEnumerator SpawnInCirclePattern()
    //{
    //    //startAngle = 0f;
    //    //endAngle = 360f;

    //    //float angleStep = (endAngle - startAngle) / bubblesAmount;
    //    //angle = startAngle;

    //    //for (int i = 0; i < bubblesAmount + 1; i++)
    //    //{
    //    //    Spawn();

    //    //    angle += angleStep;
    //    //}

    //    Spawn(0f, 360f);
    //    Debug.Log("SpawnInCirclePattern");
    //    yield return new WaitForSeconds(1f);
    //}

    private void SpawnInArcPattern()
    {
        //startAngle = 90f;
        //endAngle = 270f;

        //float angleStep = (endAngle - startAngle) / bubblesAmount;
        //angle = startAngle;

        //for (int i = 0; i < bubblesAmount + 1; i++)
        //{
        //    Spawn();

        //    angle += angleStep;
        //}

        Spawn(90f, 270f);
        Debug.Log("SpawnInArcPattern");
    }
    private void SpawnInCirclePattern()
    {
        //startAngle = 0f;
        //endAngle = 360f;

        //float angleStep = (endAngle - startAngle) / bubblesAmount;
        //angle = startAngle;

        //for (int i = 0; i < bubblesAmount + 1; i++)
        //{
        //    Spawn();

        //    angle += angleStep;
        //}

        Spawn(0f, 360f);
        Debug.Log("SpawnInCirclePattern");
    }

    private void Spawn(float startAngle, float endAngle)
    {
        _startAngle = startAngle;
        _endAngle = endAngle;

        float angleStep = (endAngle - startAngle) / _bubblesAmount;
        angle = startAngle;

        for (int i = 0; i < _bubblesAmount + 1; i++)
        {
            //Spawn();

            float bubbleDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bubbleDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bubbleMoveVector = new Vector3(bubbleDirX, bubbleDirY, 0f);
            Vector2 bubbleDir = (bubbleMoveVector - transform.position).normalized;

            GameObject bubble = BubblePool.bubblePoolInstance.GetBubble();
            bubble.transform.position = transform.position;
            bubble.transform.rotation = transform.rotation;
            bubble.SetActive(true);
            bubble.GetComponent<Bubble>().SetMoveDirection(bubbleDir);

            angle += angleStep;
        }
    }
}
