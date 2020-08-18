using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PuzzleGame.Manager
{
    /// <summary>
    /// 可收集物体为Sprite
    /// </summary>
    public class CollectableObject : BaseClickObject
    {
        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCol;
        private Rigidbody2D rb;

        //手动为物体添加物理材质
        public PhysicsMaterial2D physicsMat;

        public bool isCollectable = false;

        /// <summary>
        /// 为可收集物体添加刚体组件和碰撞器
        /// </summary>
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCol = gameObject.AddComponent<BoxCollider2D>();
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            boxCol.sharedMaterial = physicsMat;
        }

        /// <summary>
        /// 继承自BaseClickObject，当物体可被收集时设置重力
        /// </summary>
        public override void OnClick()
        {
            if (isCollectable == true)
            {
                rb.gravityScale = 1;
            }
        }
    }

}