using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    private static bool gamePaused = false;

    public static bool GetGamePaused()
    {
        return gamePaused;
    }

    public static void SetGamePaused(bool arg_gamePaused)
    {
        gamePaused = arg_gamePaused;
    }
}
