using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMotions : MonoBehaviour {

    [SerializeField]
    AnimationCurve CurveX;
    [SerializeField]
    AnimationCurve CurveY;
    [SerializeField, Range(1.0f, 1000.0f)]
    private float MoveScaleX;
    [SerializeField, Range(1.0f, 1000.0f)]
    private float MoveScaleY;
    [SerializeField]
    private float duration;

    // 移動させるUI
    [SerializeField]
    RectTransform target;

	// Use this for initialization
	void Start () {
        if (target == null)
            this.target = this.GetComponent<RectTransform>();

        Move();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void Move()
    {
        StartCoroutine(this.MoveCoro());
    }

    
    private IEnumerator MoveCoro()
    {
        float timer = 0.0f;
        float endTimeX = CurveX[CurveX.length - 1].time;
        float endTimeY = CurveY[CurveY.length - 1].time;
        for (;timer < this.duration;)
        {
            timer += Time.deltaTime;

            target.anchoredPosition = new Vector2(CurveX.Evaluate((timer / duration) * endTimeX) *MoveScaleX,
                CurveY.Evaluate((timer / duration) * endTimeY) *MoveScaleY);

            print("timer " + timer);

            yield return null;
        }
    }
}
