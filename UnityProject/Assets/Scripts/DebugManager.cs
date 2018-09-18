using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    private GUISkin guiSkin;

    private bool toggleDebugUI = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            this.toggleDebugUI = !this.toggleDebugUI;
        }
    }

    private void OnGUI()
    {
        if (!this.toggleDebugUI)
        {
            return;
        }

        // Bullet count
        GUI.BeginGroup(new Rect(Screen.width - 200f, 70f, 200f, 300f));
        {
            GUILayout.Label("Bullets: " + BulletsFactory.Debug_BulletCount, this.guiSkin.label);
            GUILayout.Label("Available enemy 1: " + EnemyFactory.GetCount("Prefabs/Enemy_01"), this.guiSkin.label);
            GUILayout.Label("Available enemy 2: " + EnemyFactory.GetCount("Prefabs/Enemy_02"), this.guiSkin.label);
        }
        GUI.EndGroup();
    }
#endif
}
