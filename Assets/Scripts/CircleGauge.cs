using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CircleGauge : MonoBehaviour {

    [SerializeField]
    private Image FillArea;
    [SerializeField]
    private Text CountText;
    [SerializeField]
    private float fillSpeed;
    [SerializeField]
    private bool Loop;
    private uint MoveCount;

    // Use this for initialization
    void Start () {
        MoveCount = 0;
        StartCoroutine(this.FillGauge());
    }

    private IEnumerator FillGauge()
    {
        float angle = 0.0f;
        for (;angle < 360.0f;)
        {
            angle = Mathf.Clamp(angle + fillSpeed,0.0f,360.0f) ;
            this.FillArea.fillAmount = angle / 360.0f;
            yield return null;
        }
        
        MoveCount++;

        if (CountText != null)
            CountText.text = MoveCount.ToString();

        if (Loop)
            StartCoroutine(FillGauge());
    }

    private void SetValue(float value,float MaxValue)
    {
        this.FillArea.fillAmount = value / MaxValue;
    }
}
