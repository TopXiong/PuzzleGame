using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PuzzleGame.Model
{
    [AttributeUsage(AttributeTargets.All)]
    public class If : ComplexInteraction
    {
        public string target;
    }
}
