using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private static int _round;
    private void Awake()
    {
        if (_instance == null){
            _instance = this;
            DontDestroyOnLoad(this);
            _round = 1;
            OrganManager.OnTransplantSuccessfulEvent += (c) => _round++;
        }
        else
            Destroy(this);
    }

    public static int GetOrganAmount()
    {
        if (_round > 4)
            return 3;
        return _round > 2 ? 2 : 1;
    }
}
