using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiameter;
    float ballDiameterWithBuffer;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    private void Awake()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiameter = ballRadius * 2f;
        PlaceAllBalls();
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPostion = headBallPosition.position;
        Vector3 currentPostion = firstInRowPostion;

        void PlaceRedBall(Vector3 postion)
        {
            GameObject ball = Instantiate(ballPrefab, postion, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
        }

        void PlaceBlueBall(Vector3 postion)
        {
            GameObject ball = Instantiate(ballPrefab, postion, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < NumInThisRow; j++)
            {
                if (i == 2 && j == 1)
                {
                    PlaceEightBall(currentPostion);
                }
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPostion);
                    }
                    else
                    {
                        PlaceBlueBall(currentPostion);
                    }
                }
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPostion);
                }
                else
                {
                    PlaceBlueBall(currentPostion);
                }

                currentPostion += new Vector3(1, 0, 0).normalized * ballDiameter;
            }

            firstInRowPostion += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPostion = firstInRowPostion;
            NumInThisRow++;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}