using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class FlowScoresPool : MonoBehaviour
    {

       [Header("FlowScores")]
	   public List<TextScoreFlowEffect> freeScores = new List<TextScoreFlowEffect>();
	   public List<TextScoreFlowEffect> busyScore = new List<TextScoreFlowEffect>();
    }
}