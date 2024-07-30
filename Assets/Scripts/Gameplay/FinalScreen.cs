using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _credits;
    [SerializeField] private Material _fadeScreen;
    

    void Start()
    {
        StartCoroutine(Print());
        FadeIn();
    }

    

    private IEnumerator Print() {
        var titleText = "Sleeping Giant";

        yield return new WaitForSeconds(1);

        Speech.instance.PrintText(titleText, _title, 0.2f);

        yield return new WaitForSeconds(4);

        var creditsText = "Game by BananaBoo, Mechfried & Sinmero<br><br><br>Thank you for playing!";

        Speech.instance.PrintText(creditsText, _credits, 0.1f);
    }



    private void FadeIn() {
        GameSystems.instance.ShaderTransition(_fadeScreen, "_Fade", 0, 1);
    }
}
