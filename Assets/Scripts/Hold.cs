using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : MonoBehaviour
{

    SpriteRenderer sp;
    public Color holdColor;
    public Color flashColor;

    public float lifetime = .5f;
    public float delayBeforeReactivation = .25f;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    public void GetGrabbed()
    {
        sp.color = flashColor;
        StartCoroutine(ProcessHold());
    }

	public float elapsedPct;

    public IEnumerator ProcessHold()
    {
        float t1 = 0;
        float d1 = lifetime;

        while (t1 < d1)
        {
            elapsedPct = Mathf.Clamp01(t1 / d1);
			sp.color = Color.Lerp(sp.color, holdColor, elapsedPct);
            t1 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        float t2 = 0;
        float d2 = delayBeforeReactivation;

        while (t2 < d2)
        {
            float p = Mathf.Clamp01(t2 / d2);
            t2 += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }




    public bool canHold;
}
