using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaM
{
    public class Square
    {
        protected bool top = false;
        protected bool right = false;
        protected bool bottom = false;
        protected bool left = false;
        protected bool minotaur = false;
        public bool theseus = false;
        protected bool exit = false;

        public Square(bool top, bool right, bool bottom, bool left)
        {
            this.top = top;
            this.right = right;
            this.bottom = bottom;
            this.left = left;

        }
        public bool Top { get => top; }
        public bool Right { get => right; }
        public bool Bottom { get => bottom; }
        public bool Left { get => left; }
        public bool Minotaur { get => minotaur; set => minotaur = value; }
        public bool Theseus { get => theseus; set => theseus = value; }
        public bool Exit { get => exit; }
        public void AddMinotaur()
        {
            minotaur = true;
        }
        public void AddTheseus()
        {
            theseus = true;
        }
        public void AddExit()
        {
            exit = true;
        }
        public void RemoveMinotaur()
        {
            minotaur = false;
        }
        public void RemoveTheseus()
        {
            theseus = false; 
        }
    }


}
