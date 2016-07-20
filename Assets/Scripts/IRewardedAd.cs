using UnityEngine;
using System.Collections;

public interface IRewardedAd
{
    bool IsReady();
    bool Play(System.Action adFinishedCallback);
}
