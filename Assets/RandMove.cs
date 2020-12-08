using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandMove : MonoBehaviour
{
    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;
    public List<int> tick = new List<int>();
    public GameObject AIRL;
    private List<List<float>>[,] conn = new List<List<float>>[19,17];
    private int action;
    //private void

    private void Start()
    {
        tick.Add(1);
        tick.Add(0);
        tick.Add(0);
        conn = AIRL.GetComponent<RL>().QSA;
    }

    private void Update()
    {
        if (!hasArrived)
        {
            hasArrived = true;
            GameObject lance = AIRL.GetComponent<RL>().inter;
            Destroy(lance);
            float randX = Random.Range(-.9f, 1f);
            float randY = Random.Range(.7f, 1.4f);
            StartCoroutine(MoveToPoint(new Vector3(randX, randY, 0.7f)));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }
        tick[0] = 0;
        targetPos[0] = (targetPos[0] * 10) +9;
        int temp = (int)targetPos[0];
        tick[1] = temp;
        targetPos[1] = (targetPos[1] * 10) -7;
        temp = (int)targetPos[1];
        tick[2] = temp;
        //Debug.Log("Wiggler");
        //Debug.Log(tick[1]);
        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Center(Clone)")
        {
            if (action != 7)
            {
                GameObject lance = AIRL.GetComponent<RL>().inter;
                Destroy(lance);
                
                int action = AIRL.GetComponent<RL>().act;
                //Debug.Log(action);
                conn[tick[1], tick[2]][action][0] = 1f;

            }
        }
    }
}

