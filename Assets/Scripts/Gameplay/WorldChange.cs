using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WorldChange : MonoBehaviour
{
    [SerializeField] private Light2D _globalLight2D;
    [SerializeField] private Material _shadowStormMat;
    [SerializeField] private Material _blackAndWhiteMat;


    private static WorldChange _wrcg;
    public static WorldChange instance {get{return _wrcg;} set{return;}}



    private void Start() {
        if(_wrcg != null) {
            Destroy(this);
        }
        _wrcg = this;
    }



    public void DimLight(float value = 0, float time = 10) {
        StartCoroutine(TransitionLight(_globalLight2D, value, time));
    }



    public void FadeInStorm(float value = 1, float time = 10) {
        GameSystems.instance.ShaderTransition(_shadowStormMat, "_Opacity", value, time);
    }



    public void BleedBricks(float value = 0, float time = 10){
        GameSystems.instance.ShaderTransition(_blackAndWhiteMat, "_BlackAndWhite", value, time);
    }



    public void ChangeWalls(float value = 0.45f, float time = 10) {
        GameSystems.instance.ShaderTransition(_blackAndWhiteMat, "_FullTransition", value, time);
    }


    
    private IEnumerator TransitionLight(Light2D light, float value, float time = 1)
    {
        float lightStr = _globalLight2D.intensity;
        float t = 0;
        while (t < 1)
        {
            t += 1 / (time * 20);
            float lerp = Mathf.Lerp(lightStr, value, t);
            light.intensity = lerp;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
