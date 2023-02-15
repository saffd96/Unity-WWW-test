using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string usersUrl = "https://dfu8aq28s73xi.cloudfront.net/testUsers";
    [SerializeField] private ScrollerController scrollerController;
    
    private UserInfosGetter _userInfosGetter;

    private void Awake()
    {
        _userInfosGetter = new UserInfosGetter();
    }

    private void OnEnable()
    {
        _userInfosGetter.OnUsersInitialized += scrollerController.CreateUserList;
    }

    private void OnDisable()
    {
        _userInfosGetter.OnUsersInitialized -= scrollerController.CreateUserList;
    }

    private void Start()
    {
        UniTask.Create(() => _userInfosGetter.GetUsersByUrl(usersUrl));
    }
}