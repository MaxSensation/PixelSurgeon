using Human;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int _round;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _round = 1;
            BodyPartManager.OnWinEvent += c => _round++;
        }
        else
            Destroy(this);
    }

    public static void DeleteThis()
    {
        BodyPartManager.OnWinEvent = null;
        var tmp = Instance;
        Instance = null;
        Destroy(tmp.gameObject);
    }


    public int GetOrganAmount()
    {
        if (_round > 4)
            return 3;
        return _round > 2 ? 2 : 1;
    }
}