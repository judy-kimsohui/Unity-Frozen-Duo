using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeltingIceController : MonoBehaviour
{

    public int count;
    public TextMeshPro leftCountText;

    private int currentHitCount = 0; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeftCountText();

        if (currentHitCount >= count)
        {
            Destroy(gameObject);
        }
    }
    

    private void UpdateLeftCountText()
    {
        var leftCount = count - currentHitCount;
        leftCountText.text = $"{leftCount}";
    }

    public void AddHit()
    {
        currentHitCount++;
    }

    public void RemoveHit()
    {
        currentHitCount--;
    }
}
