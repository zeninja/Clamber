using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonBoard : MonoBehaviour
{

    public int rowCount = 20, colCount = 10;
    public GameObject rockPrefab;

    List<GameObject> rocks = new List<GameObject>();

    float gridWidth, gridHeight;
    public Extensions.Property holdScale;

    public float timeBetweenSpawns = .0125f;
    public float timeBetweenRows = .25f;

    void Start()
    {
        SetGridWidthAndHeight();
        StartCoroutine(MakeGrid());
    }

    IEnumerator MakeGrid() {
        for(int y = 0; y < rowCount; y++) {
            float ySpacing    = gridHeight / rowCount;
            float spawnHeight = y * ySpacing;
            StartCoroutine(MakeRow(spawnHeight));

            yield return new WaitForSeconds(timeBetweenRows);
        }
    }

    public float wallCoverage = .15f;

    IEnumerator MakeRow(float height) {
        for (int x = 0; x < colCount; x++)
        {
            float p = Random.Range(0f, 1f);

            if(p < wallCoverage) {
                GameObject rock = Instantiate(rockPrefab);
                rock.transform.position = new Vector2((gridWidth / colCount) * x - ScreenInfo.w / 2 + (gridWidth / colCount) / 2, height);
                rock.transform.localScale = GetRandomizedHoldScale();
                rocks.Add(rock);
            }

            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    Vector2 GetRandomizedHoldScale() {
        return Vector2.one * Extensions.GetSmoothStart4Range(holdScale, Random.Range(0f, 1f));
    }

    void SetGridWidthAndHeight()
    {
        gridWidth  = ScreenInfo.w;
        gridHeight = ScreenInfo.h;
    }
}
