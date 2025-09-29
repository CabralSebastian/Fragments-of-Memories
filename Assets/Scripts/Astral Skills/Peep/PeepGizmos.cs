/*
#if UNITY_EDITOR
  private void OnDrawGizmos()
  {
    // if (!Application.isPlaying || PeepData.IsActive <= 0f) return;

    Vector3 center = PeepData.Center;
    Vector3 dir = PeepData.Direction.normalized;
    float radius = PeepData.Radius;
    float height = 100f; // You can make this configurable if you want

    Vector3 top = center + dir * (height * 0.5f);
    Vector3 bottom = center - dir * (height * 0.5f);

    // Save Gizmo color
    Color oldColor = Gizmos.color;
    Gizmos.color = new Color(0f, 1f, 1f, 0.4f); // Cyan-ish

    // Draw discs (top and bottom)
    UnityEditor.Handles.color = Gizmos.color;
    UnityEditor.Handles.DrawWireDisc(top, dir, radius);
    UnityEditor.Handles.DrawWireDisc(bottom, dir, radius);

    // Draw lines between edges (approximated with 8 segments)
    for (int i = 0; i < 8; i++)
    {
      float angle = i * Mathf.PI * 2f / 8;
      float nextAngle = (i + 1) * Mathf.PI * 2f / 8;

      Vector3 radialOffset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
      Vector3 nextRadialOffset = new Vector3(Mathf.Cos(nextAngle), 0f, Mathf.Sin(nextAngle)) * radius;

      // Rotate radial offsets to align with cylinder direction
      Quaternion rot = Quaternion.LookRotation(dir);
      Vector3 worldOffset = rot * radialOffset;
      Vector3 nextWorldOffset = rot * nextRadialOffset;

      Vector3 pointTop = top + worldOffset;
      Vector3 pointBottom = bottom + worldOffset;

      Gizmos.DrawLine(pointTop, pointBottom);
    }

    Gizmos.color = oldColor;
  }
#endif
*/
