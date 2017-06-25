using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Mode
{
    CLOCKWISE,
    ANTI_CLOCKWISE
}
public class CicularUIMotion : MonoBehaviour {

    [SerializeField]
    private Vector2 CenterPos;
    [SerializeField,Range(0.0f,360.0f)]
    private float OffsetAngle;
    [SerializeField,Range(0.0f,360.0f)]
    private float DestAngle;

    [SerializeField]
    private float Radius;

    [SerializeField]
    private float Speed;
    [SerializeField]
    private RectTransform target;

    [SerializeField]
    private bool Loop;

    [SerializeField]
    private bool UseCurve;
    [SerializeField]
    private AnimationCurve Curve;
    
    [SerializeField]
    private uint MoveCount;

    // Use this for initialization
    void Start () {
        MoveCount = 0;
        StartCoroutine(this.CircularMove(this.CenterPos,this.Radius));
    }

    private IEnumerator CircularMove(Vector2 center, float radius)
    {
        float startTime = Time.time;

        float rad = 0;

        float angle = 0.0f;

        Vector2 dest = Vector2.zero;
        for (; angle < this.DestAngle;)
        {

            if (this.UseCurve)
            {
                angle = Mathf.Clamp(angle + this.Speed * this.Curve.Evaluate((angle / DestAngle)), 0.0f, this.DestAngle);
            }
            else if (!this.UseCurve)
            {
                angle = Mathf.Clamp(angle + this.Speed, 0.0f, DestAngle);
            }
            
            rad = (angle + this.OffsetAngle) * Mathf.Deg2Rad;

            dest = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius + center;
            this.target.anchoredPosition = dest;

            yield return null;
        }
        
        MoveCount++;

        if (Loop)
            StartCoroutine(this.CircularMove(this.CenterPos, this.Radius));
    }
}
