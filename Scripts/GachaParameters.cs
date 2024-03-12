using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 変数をいろいろ格納するスクリプト
public partial class Gacha : MonoBehaviour
{
    List<GatchaData> GatchaList;

    private const string API_URL = "http://api.aomilka.shop/";

    [Header("ガチャを引いても良いのかのパネル")]
    [SerializeField]
    private GameObject pickCheck_Panel;


    [SerializeField]
    [Header("ガチャ結果のパネル")]
    private GameObject GatchaResults_Panel;

    [SerializeField]
    [Header("ガチャ結果1個ずつ確認")]
    private GameObject Result_OnePanel;

    [SerializeField]
    [Header("入力")]
    private InputField numInputField;

    [SerializeField]
    [Header("スライダー")]
    private Slider SliderField;

    private bool skipBOOL;

    private bool isTenContinuous;

    private bool atLeastOneSuccessive;
}
