using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MeltingIceController : MonoBehaviour
{
    public int count;
    public TextMeshPro leftCountText;
    public GameObject ice;
    public float meltingSeconds;
    
    private int currentHitCount = 0;
    private bool isMelting = false;
    private int meltingPhase;
    private Renderer iceRenderer;
    private float downwardSpeed;
    private float rendererHeightSpeed;
    
    private static readonly float MeltingPerSecond = 0.016f;
    private static readonly int Height = Shader.PropertyToID("_Height");

    // Start is called before the first frame update
    void Start()
    {
        meltingPhase = Convert.ToInt32(meltingSeconds / MeltingPerSecond);
        iceRenderer = ice.GetComponent<Renderer>();
        downwardSpeed = transform.localScale.y / meltingPhase;
        rendererHeightSpeed = 0.1f / meltingPhase * 4;
        Debug.Log($"{transform.localScale.y}");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMelting) return;
        UpdateLeftCountText();
        if (currentHitCount >= count)
        {
            StartCoroutine(StartMelt());
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

    private IEnumerator StartMelt()
    {
        isMelting = true;
        StartCoroutine(MeltDown());
        yield return null;
    }

    private IEnumerator MeltDown()
    {
        var material = iceRenderer.material;
        var height = material.GetFloat(Height);
        material.SetFloat(Height, height - rendererHeightSpeed);
        gameObject.transform.Translate(Vector3.down * downwardSpeed);
        meltingPhase--;
        if (meltingPhase <= 0) Destroy(gameObject);
        else
        {
            yield return new WaitForSeconds(MeltingPerSecond);
            StartCoroutine(MeltDown());
        }
    }
}