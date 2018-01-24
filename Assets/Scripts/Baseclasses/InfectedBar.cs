using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfectedBar : MonoBehaviour {

    [SerializeField]
    Image background, bar;

    Slider slider;
    private Camera c;

    [SerializeField]
    GameObject fillArea;

    Coroutine currentFade;

    private readonly float fadePause = 0.015f;
    private readonly float standartOpaqueActiveTime = 1.0f;
    private readonly float fadeFactor = 5.0f;



    // Use this for initialization
    void Start () {
        slider = this.gameObject.GetComponent<Slider>();
        c = Camera.main;
        fillArea.SetActive(false);
    }

    private void Update()
    {
        transform.LookAt(c.transform, Vector3.down);
    }

    public void setValue(float val)
    {
        val = Mathf.Clamp01(val);

        fillArea.SetActive(val != 0);

        if(slider != null)
            slider.value = val;
    }

    public void Fade(bool fade)
    {
        Fade(fade, standartOpaqueActiveTime);
    }

    public void Fade(bool fade, float waitingTime)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);
        currentFade = StartCoroutine(FadeImage(fade, waitingTime));
    }

    IEnumerator FadeImage(bool fadeToOpaque, float opaqueActiveTime)
    {
        if(!fadeToOpaque)
        {
            if (background.color.a < 1)
            {
                //First fade to opaque; then fade out again
                yield return StartCoroutine(FadeImage(!fadeToOpaque, opaqueActiveTime));
            }
            yield return new WaitForSeconds(opaqueActiveTime); //Stay opaque for some time
        }

        //fade from opaque to transparent
        if (!fadeToOpaque)
        {
            //loop over 1 second backwards
            for (float i = background.color.a; i >= 0; i -= fadeFactor * Time.deltaTime)
            {
                //set color with i as alpha
                background.color = new Color(background.color.r, background.color.g, background.color.b, i);
                bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, i);
                yield return new WaitForSeconds(fadePause);
            }
        }
        //fade from transparent to opaque
        else
        {
            //loop over 1 second
            for (float i = background.color.a; i <= 1; i += fadeFactor * Time.deltaTime)
            {
                //set color with i as alpha
                background.color = new Color(background.color.r, background.color.g, background.color.b, i);
                bar.color = new Color(bar.color.r, bar.color.g, bar.color.b, i);
                yield return new WaitForSeconds(fadePause);
            }
        }
    }

}
