using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePrefab : MonoBehaviour
{
    public GameObject prefabToMove, sensorToActivate;
    private float deltaX;
    // Start is called before the first frame update
    void Start()
    {
        deltaX = 76.935682f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        prefabToMove.transform.position = new Vector3(
            prefabToMove.transform.position.x + deltaX,
            prefabToMove.transform.position.y,
            prefabToMove.transform.position.z);

        sensorToActivate.SetActive(true);
    }
}