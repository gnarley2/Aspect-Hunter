using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanvas : MonoBehaviour
{
   [SerializeField] private Movement movement;

   private Vector2 offset;

   private void Start()
   {
      offset = (Vector2)transform.position - movement.GetPosition();
   }

   private void Update()
   {
      UpdateCanvasPostion();
   }

   private void UpdateCanvasPostion()
   {
      transform.position = offset + movement.GetPosition();
   }
}
