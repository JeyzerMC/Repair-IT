using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class RepairObjectAnalyzer : ObjectContainer
{
    [SerializeField]
    Transform depotSpot;

    [SerializeField]
    public float AnalyzeTime = 2f;

    [SerializeField]
    public Canvas RecipeResult;

    [SerializeField]
    public Font TextFont;

    private float currentTime = 0f;
    private bool AnalyzeFinished = false;
    private bool ContainsObject { get { return containedObjects.Count > 0; } }

    private ProgressBar progressBar;
    private GameObject progressionUI;

    private AudioSource typingSound;


    void Start()
    {
        progressBar = GetComponentInChildren<ProgressBar>();
        progressionUI = transform.Find("ProgressionUI").gameObject;
        progressionUI.SetActive(false);
        typingSound = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!AnalyzeFinished && ContainsObject && currentTime < AnalyzeTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= AnalyzeTime)
            {
                AnalyzeFinished = true;
                currentTime = 0;
                var LastObj = containedObjects[containedObjects.Count - 1];
                LastObj.GetComponent<Analyzable>().OnAnalyzeFinished();
                typingSound.Stop();
                AddFinalText(LastObj);
            }

            progressBar.Progress = currentTime / AnalyzeTime;
            Debug.Log(currentTime + " --> " + currentTime / AnalyzeTime);
        }
    }

    private void AddFinalText(Takable lastObj)
    {
        var text = RecipeResult.gameObject.AddComponent<Text>();
        text.text = "Analyze finished: \n" + GetRecipe(lastObj);

        text.resizeTextForBestFit = true;
        text.verticalOverflow = VerticalWrapMode.Truncate;
        text.horizontalOverflow = HorizontalWrapMode.Wrap;
        text.font = TextFont;
        text.alignByGeometry = true;
        text.material = TextFont.material;
        text.alignment = TextAnchor.LowerCenter;
        text.color = new Color(1, 0, 0.31f);
        Material updatedMaterial = new Material(text.materialForRendering);
        updatedMaterial.SetInt("unity_GUIZTestMode", (int)UnityEngine.Rendering.CompareFunction.Always);
        text.material = updatedMaterial;
    }

    private string GetRecipe(Takable lastObj)
    {
        if(!lastObj.TryGetComponent<ObjectToBeRepairedBehaviour>(out var repairable))
        {
            return "Not repairable!\n";
        }

        string result = "";
        foreach (var req in repairable.requirements.GroupBy(x => x))
        {
            result += req.Count() +"x " + req.Key + "\n";
        }

        return result + "\n";
    }

    protected override bool TryAddObject(Takable gameObject, Interactror interactor)
    {
        if (!gameObject.TryGetComponent<Analyzable>(out var analyzable))
        {
            Debug.LogError("Object is not Analyzable!");
            return false;
        }

        if (containedObjects.Count == 1)
        {
            Debug.Log("There is already an object in the Analyzer!");
            return false;
        }

        progressBar.Progress = 0;
        progressionUI.SetActive(true);
        analyzable.OnAnalyze(this, interactor);
        typingSound.Play();
        gameObject.transform.position = depotSpot.position;
        gameObject.transform.rotation = depotSpot.rotation;

        return true;
    }

    protected override void OnInteractionImpl(Interactror interactror)
    {
        if (currentTime > 0 && !AnalyzeFinished)
        {
            Debug.LogError("CANCELING TIMER!");
            currentTime = 0;
            progressBar.Progress = 0;
            interactror.heldObject.GetComponent<Analyzable>().OnAnalyzeCancelled();
            typingSound.Stop();
        }

        if (!ContainsObject)
        {
            progressionUI.SetActive(false);
            Destroy(RecipeResult.GetComponent<Text>());
        }
        AnalyzeFinished = false;
    }
}
