using System.Collections.Generic;
using UnityEngine;

public class SynthesisManager : MonoBehaviour
{
  private readonly Queue<(WorldObject, Synthesizer)> _pending = new();
  public int MaxPerFrame = 2;

  public void AddToQueue(WorldObject obj, Synthesizer s)
  {
    _pending.Enqueue((obj, s));
  }

  private void Update()
  {
    int processed = 0;

    while (_pending.Count > 0 && processed < MaxPerFrame)
    {
      var (obj, s) = _pending.Dequeue();
      obj.Synthetize(s);
      processed++;
    }
  }

  private void Synthesize(List<WorldObject> objetos)
  {
    // foreach (var obj in objetos)
    //   AddToQueue(obj, Synthesizer.Instance);
  }
}
