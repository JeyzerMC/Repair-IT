using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Analyzable
{
    void OnAnalyze(RepairObjectAnalyzer analyzer, Interactror interactror);
    void OnAnalyzeFinished();
    void OnAnalyzeCancelled();
}
