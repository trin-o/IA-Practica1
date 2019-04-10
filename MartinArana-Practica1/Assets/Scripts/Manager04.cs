using UnityEngine;

public class Manager04 : MonoBehaviour
{
    public static Manager04 m;
    public Transform Pastor;
    public float WorldRadius = 10;
    public float GoalRadius = 1.5f;
    [Header("Ovejas")]
    public GameObject OvejaPrefab;
    public float spawnCD;

    float spawnTimer;

    void Awake()
    {
        m = this;

        transform.localScale = Vector3.one * WorldRadius * 2;
        transform.GetChild(0).localScale = Vector3.one * GoalRadius * 2;

        spawnTimer = spawnCD;
    }

    void Update()
    {
        transform.localScale = Vector3.one * WorldRadius * 2;
        transform.GetChild(0).localScale = Vector3.one * GoalRadius * 2;

        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnCD)
        {
            Instantiate(OvejaPrefab, Random.insideUnitCircle * WorldRadius, Quaternion.identity);
            spawnTimer = 0;
        }
    }
}
