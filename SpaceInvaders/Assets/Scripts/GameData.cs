using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Custom
{
    public static class GameData
    {
        public static float GameTimeMultiplier = 1f;

        public static float GameTimeDelta => Time.deltaTime * GameTimeMultiplier;
    }
}