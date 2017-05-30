using UnityEngine;
using UnityEngine.UI;
using System.Collections;


enum MotionMode
{
    MOVE,
    ROTATE,
    SCALING,
}
public class BasicUIMotion : MonoBehaviour
{
    [SerializeField]
    private MotionMode mode;

    [SerializeField]
    private Vector3[] Points;
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
    void Start()
    {
        if (target == null)
            target = this.GetComponent<RectTransform>();
        StartCoroutine(this.MotionStart());
    }

    private IEnumerator MotionStart()
    {
        for (int i = 0; i < this.Points.Length; i++)
            switch (this.mode)
            {
                case MotionMode.MOVE:
                    yield return StartCoroutine(this.Move(this.duration, this.Points[i]));
                    break;
                case MotionMode.ROTATE:
                    yield return StartCoroutine(this.Rotate(this.duration, this.Points[i]));
                    break;
                case MotionMode.SCALING:
                    yield return StartCoroutine(this.Scaling(this.duration, this.Points[i]));
                    break;
            }
           
        if (Loop)
        {
            StartCoroutine(this.MotionStart());
        }
    }

    private IEnumerator Move(float duration, Vector2 destPos)
    {
        Vector2 startPos = this.target.anchoredPosition;
        float startTime = Time.time;
        if (this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.anchoredPosition = Vector2.Lerp(startPos, destPos, this.Curve.Evaluate((Time.time - startTime) / duration));

                yield return null;
            }
        }
        else if (!this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.anchoredPosition = Vector2.Lerp(startPos, destPos, (Time.time - startTime) / duration);

                yield return null;
            }
        }
        if (Time.time - startTime > duration)
            this.target.anchoredPosition = destPos;
    }

    private IEnumerator Rotate(float duration, Vector3 destAngle)
    {
        Vector3 startAngle = this.target.localScale;
        float startTime = Time.time;
        if (this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.localEulerAngles = Vector3.Lerp(startAngle, destAngle, this.Curve.Evaluate((Time.time - startTime) / duration));

                yield return null;
            }
        }
        else if (!this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.localEulerAngles = Vector3.Lerp(startAngle, destAngle, (Time.time - startTime) / duration);

                yield return null;
            }
        }
        if (Time.time - startTime > duration)
            this.target.localEulerAngles = destAngle;
    }

    private IEnumerator Scaling(float duration, Vector2 destScale)
    {
        Vector2 startScale = this.target.localScale;
        float startTime = Time.time;
        if (this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.localScale = Vector2.Lerp(startScale, destScale, this.Curve.Evaluate((Time.time - startTime) / duration));

                yield return null;
            }
        }
        else if (!this.UseCurve)
        {
            for (; (Time.time - startTime) <= duration;)
            {
                this.target.localScale = Vector2.Lerp(startScale, destScale, (Time.time - startTime) / duration);

                yield return null;
            }
        }
        if (Time.time - startTime > duration)
            this.target.localScale = destScale;
    }
}
