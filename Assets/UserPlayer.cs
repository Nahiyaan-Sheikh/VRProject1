using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 用户管理 挂载在头盔上
/// </summary>
public class UserPlayer : MonoBehaviour
{
    /// <summary>
    ///头
    /// </summary>
    public GameObject head;
    /// <summary>
    /// 左右手
    /// </summary>
    public GameObject l, r;
    public static UserPlayer initialize;
    private void Awake()
    {
        initialize = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
