using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGame.Model
{
    [AttributeUsage(AttributeTargets.All)]
    public class Branch : ComplexInteraction
    {
        public List<string> PIC;
        public List<string> name;

        public override string ToString()
        {
            return PIC.ToArray() + "----" + name.ToArray();
        }
    }
}
