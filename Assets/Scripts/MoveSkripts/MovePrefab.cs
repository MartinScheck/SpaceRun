using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public Key keyCS;

    public GameObject fuelCan;
    public GameObject lazer;
    public GameObject gunPiece;
    public GameObject crate;

    public GameObject key;
    public GameObject gate;

    public GameObject slime;

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

    public GameObject deactivator;

    public GameObject prefabToMove, sensorToActivate, secretPrefab;
    private float deltaX, offsetX;


    private bool prefabIsMoved;

    private float updateInterval = 0.5f;
    private float lastUpdateTime;


    // Start is called before the first frame update
    void Start()
    {
        prefabIsMoved = false;
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
        
        if (collision.gameObject.CompareTag("Hero"))
        {
            setLevelObjects();
            setBackGround();



            if (!prefabIsMoved)
            {
                setSecretPrefabIfGateIsNotActive();
            }


           

            gameObject.SetActive(false);
            prefabToMove.transform.position = new Vector3(
            prefabToMove.transform.position.x + deltaX,
            prefabToMove.transform.position.y,
            prefabToMove.transform.position.z);




            float minRange = prefabToMove.transform.position.x;
            float maxRange = prefabToMove.transform.position.x + 6f;

            float gunRange = Random.Range(minRange, maxRange);

            float lazerRange = Random.Range(minRange, maxRange);

            Vector3 fuelCanPosition = new Vector3(Random.Range(minRange, maxRange), fuelCan.transform.position.y, fuelCan.transform.position.z);
            fuelCan.transform.position = fuelCanPosition;

            Vector3 lazerPosition = new Vector3(lazerRange, lazer.transform.position.y, lazer.transform.position.z);
            lazer.transform.position = lazerPosition;

            float crateRange = Random.Range(-6f, -7f);
            if (Random.Range(0, 2) == 0)
            {
                crateRange = Random.Range(6f, 7f);
            }

            float slimeRange = Random.Range(minRange, maxRange);
            Vector3 slimePosition = new Vector3(slimeRange, slime.transform.position.y, slime.transform.position.z);
            slime.transform.position = slimePosition;

            Vector3 cratePosition = new Vector3(lazerRange + crateRange, crate.transform.position.y, crate.transform.position.z);
            crate.transform.position = cratePosition;

            gunPiece.transform.position = new Vector3(gunRange, gunPiece.transform.position.y, gunPiece.transform.position.z);


            sensorToActivate.SetActive(true);
        }
    }


    private void setSecretPrefabIfGateIsNotActive()
    {
        if (key.activeSelf || keyCS.getKeyCollected())
        {

            if (!gate.activeSelf && keyCS.getKeyCollected())
            {

               
                prefabIsMoved = true;
                deactivator.SetActive(true);

                deactivator.transform.position = new Vector3(
                gate.transform.position.x - deltaX + 20f,
                gate.transform.position.y,
                gate.transform.position.z);

                gameObject.SetActive(false);

                
                secretPrefab.SetActive(true);
                secretPrefab.transform.position = new Vector3(
                gate.transform.position.x + 4.4025f,
                prefabToMove.transform.position.y,
                prefabToMove.transform.position.z);
            }
        }
    }

    private void setLevelObjects()
    {

        fuelCan.SetActive(random() && random());

        lazer.SetActive(random());

        crate.SetActive(random() && random());

        slime.SetActive(random() && random());

        gunPiece.SetActive(random());
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