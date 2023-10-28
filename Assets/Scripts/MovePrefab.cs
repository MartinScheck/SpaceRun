using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public GameObject fuelCan;
    public GameObject lazer;
    public GameObject gun;
    public GameObject crate;
    public GameObject gunPiece;

    public GameObject light_L01;
    public GameObject light_L02;
    public GameObject light_L03;
    public GameObject light_L04;

    public GameObject light01_W01;
    public GameObject light02_W01;
    public GameObject light03_W01;
    public GameObject light04_W01;
    public GameObject light05_W01;
    public GameObject light06_W01;
    public GameObject light07_W01;
    public GameObject light08_W01;

    public GameObject light01_W02;
    public GameObject light02_W02;
    public GameObject light03_W02;
    public GameObject light04_W02;
    public GameObject light05_W02;
    public GameObject light06_W02;
    public GameObject light07_W02;
    public GameObject light08_W02;

    public GameObject windowTopOff;
    public GameObject windowTopBubble;

    public GameObject windowBottomOff;
    public GameObject windowBottomBubble;

    public GameObject prefabToMove, sensorToActivate;
    private float deltaX;

    private float updateInterval = 0.5f;
    private float lastUpdateTime;


    // Start is called before the first frame update
    void Start()
    {
        deltaX = 102.535682f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Time.time - lastUpdateTime >= updateInterval)
        {
            DeactivateOrActivateLight();
            lastUpdateTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        setLevelObjects();
        setBackGround();

        gameObject.SetActive(false);
        prefabToMove.transform.position = new Vector3(
            prefabToMove.transform.position.x + deltaX,
            prefabToMove.transform.position.y,
            prefabToMove.transform.position.z);

        sensorToActivate.SetActive(true);
    }

    private void setLevelObjects()
    {
        fuelCan.SetActive(random());
        lazer.SetActive(random());
        crate.SetActive(random());
        bool laserGunBool = random();
        gun.SetActive(laserGunBool);
        gunPiece.SetActive(laserGunBool);
    }

    private void setBackGround()
    {
        randomWindow();
    }

    private bool random()
    {
        int randomeValue = Random.Range(0, 2);

        if (randomeValue == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void randomWindow()
    {
        int rollValue = GenerateRandomNumber();

        switch (rollValue)
        {
            case 1:
                windowTopOff.SetActive(true);
                windowBottomOff.SetActive(true);
                windowTopBubble.SetActive(false);
                windowBottomBubble.SetActive(false);
                break;

            case 2:
                windowTopOff.SetActive(false);
                windowBottomOff.SetActive(false);
                windowTopBubble.SetActive(false);
                windowBottomBubble.SetActive(false);
                break;

            case 3:
                windowTopOff.SetActive(false);
                windowBottomOff.SetActive(false);
                windowTopBubble.SetActive(true);
                windowBottomBubble.SetActive(true);
                break;

            default:
                // Code, der ausgeführt wird, wenn rollValue nicht mit den obigen cases übereinstimmt
                break;
        }
    }

    private void DeactivateOrActivateLight()
    {

        light_L01.SetActive(random());

        light_L02.SetActive(random());

        light_L03.SetActive(random());

        light_L04.SetActive(random());

        light01_W01.SetActive(random());

        light02_W01.SetActive(random());

        light03_W01.SetActive(random());

        light04_W01.SetActive(random());

        light05_W01.SetActive(random());

        light06_W01.SetActive(random());

        light07_W01.SetActive(random());

        light08_W01.SetActive(random());


        light01_W02.SetActive(random());

        light02_W02.SetActive(random());

        light03_W02.SetActive(random());

        light04_W02.SetActive(random());

        light05_W02.SetActive(random());

        light06_W02.SetActive(random());

        light07_W02.SetActive(random());

        light08_W02.SetActive(random());

    }

    public static int GenerateRandomNumber()
    {
        int randomeValue = Random.Range(0, 5);
        return randomeValue;
    }

}