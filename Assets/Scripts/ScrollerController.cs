using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using EnhancedUI.EnhancedScroller;
using UnityEngine.Networking;

public class ScrollerController : MonoBehaviour, IEnhancedScrollerDelegate
{
    [SerializeField] private UserCellView userPanelPrefab;
    [SerializeField] private EnhancedScroller myScroller;

    private readonly List<UniTask> _downloadImagesTasks = new List<UniTask>();

    private List<UserData> _usersData = new List<UserData>();
    //private readonly Dictionary<int, Sprite> _downloadedImagesByIndexDictionary = new Dictionary<int, Sprite>();

    public void CreateUserList(UserData[] usersData)
    {
        _usersData = usersData.ToList();

        myScroller.Delegate = this;
        myScroller.ReloadData();
    }

    public int GetNumberOfCells(EnhancedScroller scroller)
    {
        return _usersData.Count;
    }

    public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
    {
        return 100;
    }

    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int
        dataIndex, int cellIndex)
    {
        var cellView = (UserCellView) scroller.GetCellView(userPanelPrefab);
        UniTask.Create(() => cellView.DownloadImage(_usersData[dataIndex].AvatarUrl));
        cellView.NameText.text = _usersData[dataIndex].Username;
        cellView.ScoreText.text = _usersData[dataIndex].Points.ToString();
        return cellView;
    }
}