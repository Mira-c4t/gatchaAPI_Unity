using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Gacha : MonoBehaviour
{
    public void offPickCheckPanel(bool off = false) // ガチャをしないときのOKボタン
    {
        pickCheck_Panel.SetActive(off);
    }
    public void OneGacha() // 単発ガチャ
    {
        isTenContinuous = false;
        offPickCheckPanel(true);
    }
    public void TenGacha() // 10連ガチャ
    {
        isTenContinuous = true;
        offPickCheckPanel(true);
    }
    public void EndResults()
    {
        GatchaResults_Panel.SetActive(false);
    }
    public void textChanged()
    {
        if(numInputField.text == ""){return;}
        if (float.Parse(numInputField.text) > 100) {
            numInputField.text = "100";
        }
        SliderField.value = float.Parse(numInputField.text);
    }
    public void sliderChanged()
    {
        numInputField.text = SliderField.value.ToString();
    }
    public void Button_SKIP_CLICK()
    {
         skipBOOL = true;
    }
}
