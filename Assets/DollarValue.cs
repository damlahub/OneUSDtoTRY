using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class DollarValue : MonoBehaviour
{
    private const string API_URL = "https://api.exchangerate-api.com/v4/latest/USD";
    [SerializeField] private TextMeshProUGUI TurkishLira;

    void Update()
    {
        StartCoroutine(GetExchangeRates());
    }

    IEnumerator GetExchangeRates()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(API_URL))
        {
            yield return webRequest.SendWebRequest();

            string responseText = webRequest.downloadHandler.text;
            ExchangeRateData exchangeRateData = JsonUtility.FromJson<ExchangeRateData>(responseText);

            float usdToTRY = exchangeRateData.rates.TRY;
            TurkishLira.text=usdToTRY.ToString();
        }
    }

    [System.Serializable]
    private class ExchangeRateData
    {
        public ExchangeRate rates;
    }

    [System.Serializable]
    private class ExchangeRate
    {
        public float TRY;
    }
}
