using UnityEngine;
using System.Collections.Generic;
// ガチャを実行するスクリプトだよ
public partial class Gacha : MonoBehaviour
{
    public void Gacharuyo() // ガチャを実行する関数の呼び出し
    {
        int required = isTenContinuous ? 10 : 0;
        if (!isTenContinuous)
        {
            int parsedValue;
            if (int.TryParse(SliderField.value.ToString(), out parsedValue))
            {
                required = parsedValue;
            }
            else
            {
                // エラー処理を行うか、デフォルト値を設定するなどの対応が必要です
                Debug.LogError("SliderFieldの値が整数ではありません。");
            }
        }

        DoPlay(required, isTenContinuous);
        offPickCheckPanel();
    }
}
