using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// マウスのボタンを離した時にブロックに対して処理を発動する
/// </summary>
public class ObjectManager : MonoBehaviour
{
    [SerializeField] Transform _crystal = default;
    [SerializeField] BlockController[] _blocks = default;
    
    void Update()
    {

        // マウスのボタンを離した時（ドロップした時）に処理を発動する
        if (Input.GetButtonUp("Fire1"))
        {
            ResetColor(Color.white);
            PaintFurthestBlock(Color.red);
            NumberBlockOrderByDistance();
            PaintHighestBlock(Color.cyan);
            PaintBlocksInRange(3f, Color.black);
            ShowDistance();
        }
    }

    /// <summary>
    /// 全てのブロックに色を着ける
    /// </summary>
    /// <param name="color"></param>
    void ResetColor (Color color)
    {
        Array.ForEach(_blocks, b =>
        {
            // マテリアルの Color を指定された色に変える
            Renderer r = b.GetComponent<Renderer>();

            if (r)
            {
                r.material.color = color;
            }
        });
    }

    /// <summary>
    /// Crystal から最も遠いブロックに色を着ける
    /// </summary>
    /// <param name="color"></param>
    void PaintFurthestBlock (Color color)
    {
        _blocks.OrderBy(x => Vector3.Distance(x.transform.position, _crystal.position)).LastOrDefault().GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Crystal から近い順に Block のラベルに番号を振る
    /// </summary>
    void NumberBlockOrderByDistance()
    {
        // 以下の処理はヒントです。これを削除して、summary にある機能を実装してください。
        int i=0;
        var blockArray = _blocks.OrderBy(x => Vector3.Distance(x.transform.position, _crystal.position)).ToArray();
        Array.ForEach(blockArray, b =>{b.Label = i.ToString();i++;});
    }

    /// <summary>
    /// 最も高い位置にあるブロックに色を着ける
    /// </summary>
    /// <param name="color"></param>
    void PaintHighestBlock(Color color)
    {
        _blocks.OrderByDescending(b => b.transform.position.y).FirstOrDefault().GetComponent<Renderer>().material.color=color;
        //var highblock = _blocks.OrderByDescending(x => Vector3.Distance(new Vector3(x.transform.position.x, 0, x.transform.position.z), x.transform.position)).FirstOrDefault();
    }

    /// <summary>
    /// Crystal から distance より近くの距離にあるブロックに色を着ける
    /// </summary>
    /// <param name="distance"></param>
    /// <param name="color"></param>
    void PaintBlocksInRange(float distance, Color color)
    {
        _blocks.Where(x => distance > Vector3.Distance(_crystal.position, x.transform.position)).ToList().ForEach(b => b.GetComponent<Renderer>().material.color = color);

       // var blockarray = _blocks.Where(x => distance > Vector3.Distance(_crystal.position, x.transform.position)).ToArray();
       //_blocks.OrderByDescending(x => distance >= Vector3.Distance(_crystal.position, x.transform.position)).ToList().ForEach(b => b.GetComponent<Renderer>().material.color = color);
    }

    /// <summary>
    /// ラベルに Crystal からの距離（小数点第一位まで）を表示する
    /// </summary>
    void ShowDistance()
    {

        Array.ForEach(_blocks, b => 
        {float distance = (Vector3.Distance(_crystal.position, b.transform.position)); b.Label =distance.ToString("F1"); });
    }
}
