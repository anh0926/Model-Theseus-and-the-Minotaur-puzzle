using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaM;


namespace TaM
{
    public class Game
    {
        private List<Level> AllLevel = new List<Level>();
        private Level _currentlevel;


        public int LevelCount { get => AllLevel.Count(); }
        public int LevelHeight { get => _currentlevel != null ? _currentlevel.LevelHeight : 0; }
        public int LevelWidth { get => _currentlevel != null ? _currentlevel.LevelWidth : 0; }
        public string CurrentLevelName { get => _currentlevel != null ? _currentlevel.LevelName : "No levels loaded"; }
        

        public List<string> LevelNames()
        {
            List<string> result = new List<string>();
            foreach (Level aLevel in AllLevel)
            {
                result.Add(aLevel.LevelName);
            }
            return result;
        }

        public void AddLevel(string name, int width, int height, string data)
        {
            Level aNewLevel = new Level(name,  width, height, data);
            AllLevel.Add(aNewLevel);
            _currentlevel = aNewLevel;
        }

        public void SetLevel(string targetLevelName)
        {
            foreach (Level aLevel in AllLevel)
            {
                if (aLevel.LevelName == targetLevelName)
                {
                    _currentlevel = aLevel;
                    break;

                }

            }
        }

        //public int MinotaurRow { get => _currentlevel.minotaur.Row; }
        //public int MinotaurCol { get => _currentlevel.minotaur.Col; }
        //public int TheseusRow { get => _currentlevel.theseus.Row; }
        //public int TheseusCol { get => ; }


        // WhatIsAt indicates what block (row and col) Minotaur, Thesus , Exist is. In that row, col is a square 
        //which include walls (top, right, bottom, left )
        //public Square WhatIsAt (int row, int col)
        //{
        //    return _currentlevel.WhatIsAt(row, col, MinotaurRow, MinotaurCol, TheseusRow, TheseusCol);
        //}

        public Square WhatIsAt(int col, int row)
        {
            Square square = this._currentlevel.allMySquares[ col, row];
            return square;
        }

        public void MoveTheseus (Moves direction)
        {
            this._currentlevel.MoveTheseus(direction);
        }

        public int MoveCount { get => _currentlevel._movecount; }

        public void MoveMinotaur()
        {        
            this._currentlevel.MoveMinotaur();
        }
       public bool HasMinotaurWon { get =>  _currentlevel.isTheseusDead();    }
       public bool HasTheseusWon { get => _currentlevel.hasTheseusEscaped();  }

    }

}