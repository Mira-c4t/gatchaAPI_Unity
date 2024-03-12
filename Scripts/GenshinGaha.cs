using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaItem
{
    public string name;
    public int rarity;
    public float baseRate;
    public float cumulativeRate;
    public bool isntPickup_4;
    public bool isntPickup_5;
}

public class GenshinGacha : MonoBehaviour
{
    public List<GachaItem> items;

    public int maxPulls = 90;
    public int currentPulls = 0;
    public List<GachaItem> results;

    // ガチャの結果を表示するUIテキスト
    public UnityEngine.UI.Text resultText;

    // ★5ピックアップを獲得したかどうかのフラグ
    private bool gotPickup5 = false;

    void Start()
    {
        resultText.text = "引いた回数: 0";
    }

    
    public void PullGacha()
    {
        currentPulls++;
        GachaItem result = GetGachaResult();
        results.Add(result);

        // 結果を表示
        resultText.text = "引いた回数: " + currentPulls + "\n結果: " + result.name;

        if (result.rarity == 5)
        {
            if (Random.value < 0.5f)
            {
                resultText.text += "\n★5 ピックアップ獲得！";
                gotPickup5 = true;
            }
            else
            {
                resultText.text += "\n★5 逃しました...";
                gotPickup5 = false;
            }
        }
        else if (result.rarity == 4)
        {
            if (Random.value < 0.5f)
            {
                // ★4 ピックアップを獲得する処理
                if (gotPickup5)
                {
                    resultText.text += "\n★4 ピックアップ (確定)！";
                }
                else
                {
                    resultText.text += "\n★4 ピックアップ獲得！";
                }
            }
            else
            {
                resultText.text += "\n★4 逃しました...";
            }
        }
    }

    private GachaItem GetGachaResult()
    {
        float randomValue = Random.value;
        foreach (GachaItem item in items)
        {
            if (randomValue <= item.cumulativeRate)
            {
                return item;
            }
        }
        return null;
    }
}
