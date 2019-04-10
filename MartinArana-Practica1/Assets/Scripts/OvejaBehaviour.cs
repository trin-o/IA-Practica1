using System.Collections;
using UnityEngine;
using AI;

public class OvejaBehaviour : BaseAgent
{
    Manager04 m;

    [Header("Sheep Specifics")]
    bool caught = false;
    public float randomUpdateCD = 1;
    SpriteRenderer spr;
    Color col = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        m = Manager04.m;
        spr = GetComponent<SpriteRenderer>();
        col.a=0;
        spr.color = col;
        StartCoroutine(Appear());

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Vector2.zero) < m.GoalRadius)
        {
            caught = true;
            StartCoroutine(Disappear());
        }
        if (Vector2.Distance(transform.position, Vector2.zero) > m.WorldRadius)
        {
            StartCoroutine(Disappear());
        }

        if (!caught)
        {
            addFlee(m.Pastor.position, visionRange, 2);
            addFlee(Vector2.zero, 3, .7f);
            addRandom(Vector2.zero, randomUpdateCD, m.WorldRadius, .5f);

            calculateMovement();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, .2f);
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    IEnumerator Disappear()
    {
        while (col.a > 0)
        {
            col.a -= Time.deltaTime * .5f;
            spr.color = col;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator Appear()
    {
        while (col.a < 1)
        {
            col.a += Time.deltaTime;
            spr.color = col;
            yield return null;
        }
        yield return null;
    }
}
