using System.Collections;
using UnityEngine;
using TMPro;

public class NeonFlicker : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    void OnEnable() 
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        
        if (textComponent != null) 
        {
            StartCoroutine(Flicker());
        }
    }

    IEnumerator Flicker()
    {
        while (true) 
        {
            float randomAlpha = Random.Range(0.1f, 1.0f);
            
            Color newColor = textComponent.color;
            newColor.a = randomAlpha;
            textComponent.color = newColor;

            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }
}