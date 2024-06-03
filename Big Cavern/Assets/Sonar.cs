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
    [SerializeField] float waveSpeedMultiplier;
    [SerializeField] float sonarPingFadeMultiplier;

    Vector2 directionToCursor;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        TurnSonar();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireSonar();
        }
    }

    private void TurnSonar()
    {
        Vector3 cursorPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(cursorPos.x - transform.position.x, cursorPos.y - transform.position.y);
        transform.up = direction;
    }

    private void FireSonar()
    {
        float innerAngles = sonarAngle / sonarAccuracy;

        for (int i = 0; i < sonarAccuracy; i++)
        {
            float angle = transform.eulerAngles.y - sonarAngle / 2 + innerAngles * i;
            angle -= transform.eulerAngles.z;

            Vector2 currentDir = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            
            RaycastHit2D hit;

            //If enemy hit
            if (hit = Physics2D.Raycast(transform.position, currentDir, sonarLength, enemyLayerMask))
            {
                float distanceFromTarget = Vector2.Distance(hit.point, (Vector2)transform.position);
                GameObject dotInstance = Instantiate(enemyDot, hit.point, Quaternion.identity);
                dotInstance.GetComponent<SonarPingDot>().InitialiseDot(distanceFromTarget, waveSpeedMultiplier, sonarPingFadeMultiplier);
            }

            //If wall hit
            else if (hit = Physics2D.Raycast(transform.position, currentDir, sonarLength, wallLayerMask))
            {
                float distanceFromTarget = Vector2.Distance(hit.point, (Vector2)transform.position);
                GameObject dotInstance = Instantiate(wallDot, hit.point, Quaternion.identity);
                dotInstance.GetComponent<SonarPingDot>().InitialiseDot(distanceFromTarget, waveSpeedMultiplier, sonarPingFadeMultiplier);
            }
        }
    }
}
