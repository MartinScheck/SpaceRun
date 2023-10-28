using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabRandomizer : MonoBehaviour
{
    public GameObject fuelCan;
    public GameObject lazer;
    public GameObject gun;
    public GameObject crate;
    public GameObject gunPiece;

    public GameObject light01;
    public GameObject light02;
    public GameObject light03;
    public GameObject light04;
    public GameObject light05;
    public GameObject light06;
    public GameObject light07;
    public GameObject light08;

    public GameObject windowTopOff;
    public GameObject windowTopBubble;

    public GameObject windowBottomOff;
    public GameObject windowBottomBubble;

    public GameObject sensorToActivate;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        setLevelObjects();
        setBackGround();
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
        randomLights();
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

    private void randomLights()
    {

    }

    public static int GenerateRandomNumber()
    {
        int randomeValue = Random.Range(0, 5);
        return randomeValue;
    }


}
