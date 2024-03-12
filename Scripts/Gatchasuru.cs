using System.Collections;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Net;
using UnityEngine.UI;

public partial class Gacha : MonoBehaviour
{
    [SerializeField] GameObject prefabTemplate;
    [SerializeField] GameObject imageScroll;
    public List<GameObject> resultPanels;
    public class chara {
        public string name;
        public int rare;
        public Sprite image;
    }

    private string makeRealityStar(int num)
    {
        string star = "";
        for (int i = 1; i <= num; i++) { star += "★"; }
        return star;
    }

    public void DoPlay(int count, bool assurance = false)
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        StartCoroutine(turnGacha(count, assurance));
    }

    private IEnumerator turnGacha(int count, bool assurance)
    {
        if (resultPanels != null)
        {
            foreach (GameObject respan in resultPanels)
            {
                Destroy(respan);
            }
        }
        var param = "?num=" + count;
        var coroutine = Communication(API_URL + param);
        yield return StartCoroutine(coroutine);
        var res = coroutine.Current.ToString();

        Data jsonData = JsonUtility.FromJson<Data>(res);

        yield return StartCoroutine(OneOneShow(jsonData));

        Result_OnePanel.SetActive(false);

        GatchaResults_Panel.SetActive(true);
        jsonData.items = jsonData.items.OrderByDescending(x => x.reality).ToArray();
        // ガチャの最終結果表示
        foreach(var item in jsonData.items)
        {
            chara newChara = new chara();
            yield return StartCoroutine(GetTexture(item.imageLINK, newChara));
            newChara.name = item.name;
            newChara.rare = item.reality;
            // テンプレートを生成
            var template = Instantiate(prefabTemplate, imageScroll.transform);

            // 名前のセット
            template.transform.GetChild(1).GetComponent<Text>().text = newChara.name;

            // レアリティのセット
            template.transform.GetChild(2).GetComponent<Text>().text = makeRealityStar(newChara.rare);

            // テクスチャのセット
            template.transform.GetChild(0).GetComponent<Image>().sprite = newChara.image;

            resultPanels.Add(template);
        }
    }

    private IEnumerator OneOneShow(Data jsonData)
    {
        Animator _animator = Result_OnePanel.GetComponent<Animator>();
        skipBOOL = false;
        // ガチャを一個一個表示する
        foreach (var item in jsonData.items)
        {
            chara newChara = new chara();
            yield return StartCoroutine(GetTexture(item.imageLINK, newChara));
            newChara.name = item.name;
            newChara.rare = item.reality;
            Result_OnePanel.transform.GetChild(1).GetComponent<Text>().text = newChara.name;
            Result_OnePanel.transform.GetChild(2).GetComponent<Text>().text = makeRealityStar(newChara.rare);
            Result_OnePanel.transform.GetChild(0).GetComponent<Image>().sprite = newChara.image;
            Result_OnePanel.SetActive(true);
            _animator.Play("animation", 0, 0);
            if(!skipBOOL)
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // 最後のアイテムの場合は待機しない
            if (item == jsonData.items.Last())
            {
                break;
            }
        }
    }

    private IEnumerator Communication(string url)
    {
        using (var req = UnityWebRequest.Get(url))
        {
            yield return req.SendWebRequest();
            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    print("通信完了");
                    yield return req.downloadHandler.text;
                    break;
                default:
                    print("エラー");
                    print(req.error);
                    break;
            }
        }
    }

    private IEnumerator GetTexture(string url, chara template)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageLoadERROR:" + www.error);
        }
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            template.image = sprite;
        }
    }
}
