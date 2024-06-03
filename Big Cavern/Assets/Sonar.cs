using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject enemyDot;
    [SerializeField] GameObject wallDot;

    [SerializeField] int sonarAngle; //Total scan angle
    [SerializeField] int sonarAccuracy; //No of rays
    [SerializeField] float sonarLength;

    Vector2 directionToCursor;

    private void Update()
    {
        CalculateDirection();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireSonar();
        }
    }

    private void CalculateDirection()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        directionToCursor = cursorPos - (Vector2)transform.position;
    }

    private void FireSonar()
    {
        float innerAngles = sonarAngle / sonarAccuracy;

        Vector2 currentDir = Quaternion.Euler(0, 0, sonarAngle / 2) * directionToCursor;

        for (int i = 0; i < sonarAccuracy; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDir, sonarLength);
            currentDir = Quaternion.Euler(0, 0, innerAngles) * directionToCursor;

            //If enemy hit
            if (hit.transform.gameObject.layer == 6)
            {
                Instantiate(enemyDot, hit.point, Quaternion.identity);
            }

            //If wall hit
            else if (hit.transform.gameObject.layer == 7)
            {
                Instantiate(wallDot, hit.point, Quaternion.identity);
            }
        }
    }
}
