using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RL : MonoBehaviour
{
    public GameObject Cam;
    public GameObject target;
    public GameObject intercept;
    public GameObject inter;
    //private List<float> A = new List<float>();
    //private List<float> B = new List<float>();
    //private List<float> C = new List<float>();
    //private List<float> D = new List<float>();
    //private List<float> E = new List<float>();
    private List<float> move = new List<float>();
    //private List<List<float>> actionSpace = new List<List<float>>();
    //private bool Guardian = false;
    private List<int> Guardian = new List<int>();
    private float exploreRate = 100f;
    public int act;

    //For all the marbles, the Q (S, A) Matrix
    public List<List<float>>[,] QSA = new List<List<float>>[19, 7];






    private void Start()
    {
        //GameObject.FindObjectWithTag("target").GetComponent<RandMove>().tick;
        Guardian = target.GetComponent<RandMove>().tick;

       
        //temp[0] = 1f;
        //Debug.Log(actionSpace[1][0]);
        //QSA[17, 5] = actionSpace;

        setQSpace();
    }

    private void Update()
    {
        if (Guardian[0] == 0)
        {
            //moveCam(B);
            act = Random.Range(0, 5);
            //Debug.Log(act);
            EGA(act);
            Guardian[0] = 1;
        }
        //Debug.Log(QSA[0,0][4][1]);

    }

    private void moveCam(List<float> Choice)
    {
        Cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        //inter = Instantiate(intercept, new Vector3(.0f, .8f, 1f), Quaternion.identity);
        //inter.transform.SetParent(Cam.transform);
        Cam.transform.Rotate(new Vector3(Choice[1], Choice[2], 0f));
        //Destroy(inter);
        

    }

    void EGA(int act)
    {
        if (Random.Range(0.00f, 100.00f) < exploreRate)
        //if (Random.Range(0, 10) > 5)
        {
            exploreRate -= exploreRate * .005f;
            explore(act);
        }
        else
            exploit();
        //count();
        //Debug.Log(exploreRate);

    }

    void setQSpace()
    {
        //actionSpace.initialize_order();
        for (int i = 0; i < 19; i++)
            for (int ii = 0; ii < 7; ii++)
            {
                var aS = new List<List<float>>();

                var temp = new List<float>();
                temp.Add(0f);
                temp.Add(-14f);
                temp.Add(-33f);
                aS.Add(temp);

                temp = new List<float>();
                temp.Add(0f);
                temp.Add(14f);
                temp.Add(33f);
                aS.Add(temp);

                temp = new List<float>();
                temp.Add(0f);
                temp.Add(14f);
                temp.Add(-33f);
                aS.Add(temp);

                temp = new List<float>();
                temp.Add(0f);
                temp.Add(-14f);
                temp.Add(33f);
                aS.Add(temp);

                temp = new List<float>();
                temp.Add(0f);
                temp.Add(0f);
                temp.Add(0f);
                aS.Add(temp);

                QSA[i,ii] = aS;
            }
    }


    void explore(int action)
    {
        Debug.Log("Method: EXPLORE");
        move = QSA[Guardian[1], Guardian[2]][act];
        //inter = Instantiate(intercept, new Vector3(.0f, .8f, 1f), Quaternion.identity);
        if (action == 4)
        {
            //Cam.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //inter.transform.SetParent(Cam.transform);
            inter = Instantiate(intercept, new Vector3(.0f, 1f, 1f), Quaternion.identity);
            inter.transform.localScale = new Vector3(.5f, 1f, 2f);
        }
        else if (action == 2)
        {
            inter = Instantiate(intercept, new Vector3(-.5f, .8f, 1f), Quaternion.identity);
            inter.transform.localScale = new Vector3(.5f, .5f, 2f);
        }
        else if (action == 1)
        {
            inter = Instantiate(intercept, new Vector3(.5f, .8f, 1f), Quaternion.identity);
            inter.transform.localScale = new Vector3(.5f, .5f, 2f);
        }
        else if (action == 0)
        {
            inter = Instantiate(intercept, new Vector3(-.5f, 1.3f, 1f), Quaternion.identity);
            inter.transform.localScale = new Vector3(.5f, .5f, 2f);
        }
        else if (action == 3)
        {
            inter = Instantiate(intercept, new Vector3(.5f, 1.3f, 1f), Quaternion.identity);
            inter.transform.localScale = new Vector3(.5f, .5f, 2f);
        }
        else
        {
            //inter.transform.localScale = new Vector3(.8f, .5f, 2f);
        }
        //inter.transform.SetParent(Cam.transform);
        moveCam(move);
        //Destroy(inter);
    }

    void exploit()
    {
        Debug.Log("Method: EXPLOIT");
        for (int i = 0; i < 5; i++)
        {
            if (QSA[Guardian[1], Guardian[2]][i][0] == 1)
            {
                Debug.Log("MEMORY USED");
                //Debug.Log(Guardian[1]);
                move = QSA[Guardian[1], Guardian[2]][i];
                //Debug.Log(move[0]);
                //Debug.Log(i);

                moveCam(move);
                return;
            }
        }
        Debug.Log("NO MEMORY");
        move = QSA[Guardian[1], Guardian[2]][4];
        moveCam(move);



    }
    void count()
    {
        int ctr = 0;
        for (int i = 0; i < 19; i++)
            for (int ii = 0; ii < 7; ii++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (QSA[i, ii][j][0] == 1)
                    {
                        ctr++;
                    }
                }
            }
        Debug.Log(ctr);
    }
}
