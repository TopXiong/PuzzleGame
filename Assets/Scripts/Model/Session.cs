﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PuzzleGame.Model
{
    [AttributeUsage(AttributeTargets.All)]
    public class Session : Interactive
    {
        public string PIC;
        public string name;
    }
}
