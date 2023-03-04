using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public GameObject selector;
    public float tweenTime;

    private float size;
    private Image butImage;
    public float curSize;
    private TMP_Text text;
    private bool isSmall;

    void Start()
    {
        size = 2.5f;
        butImage = transform.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        curSize = size*1.2f;
        LeanTween.scale(gameObject, new Vector3(curSize, curSize, curSize), tweenTime)
        .setEase(LeanTweenType.easeOutQuad)
        .setIgnoreTimeScale(true);

        LeanTween.value(gameObject, 12.8f, 9f, tweenTime)
        .setEase(LeanTweenType.easeOutQuad)
        .setOnUpdate((float val)=>{butImage.pixelsPerUnitMultiplier = val;})
        .setIgnoreTimeScale(true);

        //selector.transform.position = new Vector3(selector.transform.position.x, transform.position.y, selector.transform.position.z);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        curSize = size;
        LeanTween.value(gameObject, 9f, 12.8f, tweenTime + 0.1f)
        .setEase(LeanTweenType.easeOutQuad)
        .setOnUpdate((float val)=>{butImage.pixelsPerUnitMultiplier = val;})
        .setIgnoreTimeScale(true);
        
        LeanTween.scale(gameObject, new Vector3(curSize, curSize, curSize), tweenTime)
        .setEase(LeanTweenType.easeOutQuad)
        .setIgnoreTimeScale(true);
    }

    // Update is called once per frame
    public void Tween()
    {        
        //LeanTween.scale(this.gameObject, new Vector3(curSize/1.25f, curSize/1.25f, curSize/1.25f), 0.15f).setEase(LeanTweenType.easeInQuad).setIgnoreTimeScale(true);
        //LeanTween.scale(this.gameObject, new Vector3(curSize, curSize, curSize), tweenTime).setEase(LeanTweenType.easeOutBounce).setDelay(0.15f).setIgnoreTimeScale(true);
    }
}
