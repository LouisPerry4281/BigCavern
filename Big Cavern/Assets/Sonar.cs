using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject enemyDot;
    [SerializeField] GameObject wallDot;

    [SerializeField] LayerMask enemyLayerMask;
    [SerializeField] LayerMask wallLayerMask;

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
        currentDir.Normalize();

        for (int i = 0; i < sonarAccuracy; i++)
        {
            RaycastHit2D hit;

            //If enemy hit
            if (hit = Physics2D.Raycast(transform.position, currentDir, sonarLength, enemyLayerMask))
            {
                Instantiate(enemyDot, hit.point, Quaternion.identity);
            }

            //If wall hit
            else if (hit = Physics2D.Raycast(transform.position, currentDir, sonarLength, wallLayerMask))
            {
                Instantiate(wallDot, hit.point, Quaternion.identity);
            }

            currentDir = Quaternion.Euler(0, 0, innerAngles * i) * directionToCursor;
            currentDir.Normalize();
        }
    }
}
