using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Mode
{
    LINEAR,
    CIRCULAR
}
public class UIMotion : MonoBehaviour {

    [SerializeField]
    private Vector2[] Points;
    [SerializeField]
    private float duration;
    [SerializeField]
    private RectTransform target;

    [SerializeField]
    private bool Loop;

    [SerializeField]
    private bool UseCurve;
    [SerializeField]
    private AnimationCurve Curve;

	// Use this for initialization
	void Start () {
        if (target == null)
            target = this.GetComponent<RectTransform>();
        StartCoroutine(this.Move());

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator Move()
    {
        for(int i = 0; i < this.Points.Length;i++)
            yield return StartCoroutine(this.MoveCoro(this.duration, this.Points[i]));
        if (Loop)
        {
            StartCoroutine(this.Move());
        }
    }

    private IEnumerator MoveCoro(float duration, Vector2 destPos)
    {
        Vector2 startPos = this.target.anchoredPosition;
        float startTime = Time.time;
        if (this.UseCurve)
        {
            for (; (Time.time - startTime) < duration;)
            {
                this.target.anchoredPosition = Vector2.Lerp(startPos, destPos, this.Curve.Evaluate((Time.time - startTime) / duration));

                yield return null;
            }
        }
        else if (!this.UseCurve)
        {
            for (; (Time.time - startTime) < duration;)
            {
                this.target.anchoredPosition = Vector2.Lerp(startPos, destPos, (Time.time - startTime) / duration);

                yield return null;
            }
        }
    }
}
