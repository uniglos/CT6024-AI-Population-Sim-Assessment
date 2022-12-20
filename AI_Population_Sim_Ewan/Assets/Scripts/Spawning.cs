using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    private float spawnHeight;
    private float spawnWidth;
    private float spawnLength;

    public GameObject FoodPrefab;
    public GameObject WaterPrefab;

    public GameObject PreyPrefab;
    public GameObject PredatorPrefab;

    public int maxFoodNumber;

    public int startPrey;
    public int startPredators;
    int PreyNum = 0;
    int PredatorNum = 0;

    public List<GameObject> preyList;
    public List<GameObject> predatorList;

    public bool ShouldFoodSpawn = true;
    public bool ShouldWaterSpawn = true;

    public List<GameObject> foodEntities;
    public List<GameObject> waterEntities;

    private bool isFoodMax = false;

    [SerializeField]
    private LayerMask Obstacles;

    // Start is called before the first frame update
    void Start()
    {
        spawnHeight = gameObject.GetComponent<BoxCollider>().bounds.size.y;
        spawnWidth = gameObject.GetComponent<BoxCollider>().bounds.size.x;
        spawnLength = gameObject.GetComponent<BoxCollider>().bounds.size.z;

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(foodEntities.Count);
        foodEntities.Clear();
        waterEntities.Clear();

        if ( PreyNum <= startPrey)
        {
            GameObject prefabInstance = Instantiate(PreyPrefab);

            //foodEntities.Add(prefabInstance.gameObject);
            //prefabInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prefabInstance.transform.position = new Vector3(gameObject.GetComponent<BoxCollider>().bounds.center.x + Random.Range(-(spawnWidth / 2), spawnWidth / 2),
                                                            0.0f,
                                                            gameObject.GetComponent<BoxCollider>().bounds.center.z + Random.Range(-(spawnLength / 2), (spawnLength / 2)));
            PreyNum += 1;
            preyList.Add(prefabInstance);
        }
        if (PredatorNum <= startPredators)
        {
            GameObject prefabInstance = Instantiate(PredatorPrefab);

            //foodEntities.Add(prefabInstance.gameObject);
            //prefabInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prefabInstance.transform.position = new Vector3(gameObject.GetComponent<BoxCollider>().bounds.center.x + Random.Range(-(spawnWidth / 2), spawnWidth / 2),
                                                            0.0f,
                                                            gameObject.GetComponent<BoxCollider>().bounds.center.z + Random.Range(-(spawnLength / 2), (spawnLength / 2)));
            PredatorNum += 1;
            predatorList.Add(prefabInstance);
        }

        if (foodEntities.Count >= maxFoodNumber)
        {
            ShouldFoodSpawn = false;

        }
        if (foodEntities.Count < maxFoodNumber && ShouldFoodSpawn == false)
        {
            ShouldFoodSpawn = true;
        }

        if (waterEntities.Count >= maxFoodNumber)
        {
            ShouldWaterSpawn = false;

        }
        if (waterEntities.Count < maxFoodNumber && ShouldWaterSpawn == false)
        {
            ShouldWaterSpawn = true;
        }

        foreach (Transform Food in this.transform)
        {
            Food.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            if (Food.transform.position.y < -10)
            {
                foodEntities.Remove(Food.gameObject);
                Destroy(Food.gameObject);

                waterEntities.Remove(Food.gameObject);
                Destroy(Food.gameObject);
            }
            if (Food.gameObject.name == "Food(Clone)")
            {
                foodEntities.Add(Food.gameObject);
            }
            if (Food.gameObject.name == "Water(Clone)")
            {
                waterEntities.Add(Food.gameObject);
            }
        }

        if (foodEntities.Count <= maxFoodNumber && ShouldFoodSpawn == true)
        {
            GameObject prefabInstance = Instantiate(FoodPrefab, transform);

            //foodEntities.Add(prefabInstance.gameObject);
            prefabInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prefabInstance.transform.position = new Vector3(gameObject.GetComponent<BoxCollider>().bounds.center.x + Random.Range(-(spawnWidth / 2), spawnWidth / 2),
                                                            0.0f,
                                                            gameObject.GetComponent<BoxCollider>().bounds.center.z + Random.Range(-(spawnLength / 2), (spawnLength / 2)));
        }
        if (waterEntities.Count <= maxFoodNumber && ShouldWaterSpawn == true)
        {
            GameObject prefabInstance = Instantiate(WaterPrefab, transform);

            //foodEntities.Add(prefabInstance.gameObject);
            prefabInstance.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prefabInstance.transform.position = new Vector3(gameObject.GetComponent<BoxCollider>().bounds.center.x + Random.Range(-(spawnWidth / 2), spawnWidth / 2),
                                                            0.0f,
                                                            gameObject.GetComponent<BoxCollider>().bounds.center.z + Random.Range(-(spawnLength / 2), (spawnLength / 2)));
        }

    }
}
