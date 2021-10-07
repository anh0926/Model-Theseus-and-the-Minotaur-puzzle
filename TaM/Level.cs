using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaM
{
    class Level
    {
        protected string _levelname;
        protected int _levelheight = 0;
        protected int _levelwidth = 0;
        protected bool _top, _right, _bottom, _left;
        public int _minotaurRow, _minotaurCol, _theseusRow, _theseusCol, _exitRow, _exitCol;
        public Square[,] allMySquares;
        public int _movecount = 0;



        public string LevelName
        {
            get => _levelname;
        }

        public int LevelHeight
        {
            get => _levelheight;
        }

        public int LevelWidth
        {
            get => _levelwidth;
        }


        public Level(string aName, int aLevelWidth, int aLevelHeight, string levelData)
        {
            _levelname = aName;
            _levelheight = aLevelHeight;
            _levelwidth = aLevelWidth;
            _minotaurRow = int.Parse(levelData.Substring(0, 2));
            _minotaurCol = int.Parse(levelData.Substring(2, 2));
            _theseusRow = int.Parse(levelData.Substring(5, 2));
            _theseusCol = int.Parse(levelData.Substring(7, 2));
            _exitRow = int.Parse(levelData.Substring(10, 2));
            _exitCol = int.Parse(levelData.Substring(12, 2));
            allMySquares = new Square[_levelwidth, _levelheight];

            //split all data of squares from levelData 
            List<string> squareData = new List<string>();
            for (int i = 15; i < levelData.Length; i += 5)
            {
                squareData.Add(levelData.Substring(i, 4));
            }

            int counter = 0;
            for (int row = 0; row < _levelheight; row++)
            {

                for (int col = 0; col < _levelwidth; col++)
                {
                    //find the value for top, right, bottom, left for Square
                    _top = squareData[counter].Substring(0, 1).Equals("1");
                    _right = squareData[counter].Substring(1, 1).Equals("1");
                    _bottom = squareData[counter].Substring(2, 1).Equals("1");
                    _left = squareData[counter].Substring(3, 1).Equals("1");
                    Square aSquare = new Square(_top, _right, _bottom, _left);
                    allMySquares[col, row] = aSquare;
                    counter++;
                }
            }

            allMySquares[_minotaurCol, _minotaurRow].AddMinotaur();
            allMySquares[_theseusCol, _theseusRow].AddTheseus();
            allMySquares[_exitCol, _exitRow].AddExit();
        }


        //create WhatIsAt method. I will have to use this method for the next method MoveTheseus 
        public Square WhatIsAt(int col, int row)
        {
            Square square = allMySquares[col, row];
            return square;
        }

        public void MoveTheseus(Moves direction)
        {
            Square destinationSquare;
            Square originalSquare = WhatIsAt(_theseusCol, _theseusRow);


            switch (direction)
            {
                case Moves.UP:
                    destinationSquare = WhatIsAt(_theseusCol, _theseusRow - 1);
                    if (!originalSquare.Top) //if theseus square have no Top, he can move up
                    {
                        originalSquare.Theseus = false;
                        destinationSquare.theseus = true;
                        _movecount++;
                    }; break;

                case Moves.DOWN:
                    destinationSquare = WhatIsAt(_theseusCol, _theseusRow + 1);

                    if (!originalSquare.Bottom)
                    {
                        originalSquare.Theseus = false;
                        destinationSquare.theseus = true;
                        _movecount++;
                    }; break;

                case Moves.RIGHT:
                    destinationSquare = WhatIsAt(_theseusCol + 1, _theseusRow);

                    if (!originalSquare.Right)
                    {
                        originalSquare.Theseus = false;
                        destinationSquare.theseus = true;
                        _movecount++;
                    }; break;

                case Moves.LEFT:
                    destinationSquare = WhatIsAt(_theseusCol - 1, _theseusRow);

                    if (!originalSquare.Left)
                    {
                        originalSquare.Theseus = false;
                        destinationSquare.theseus = true;
                        _movecount++;
                    }; break;

                case Moves.PAUSE:
                    originalSquare.Theseus = true;
                    _movecount++;
                    break;
            }
        }

        public void MoveMinotaur()
        {
            Square destinationSquare;
            Square originalSquare = WhatIsAt(_minotaurCol, _minotaurRow);

            if (_theseusCol == _minotaurCol)
            {
                if (_theseusRow < _minotaurRow)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow - 1);
                    if (!originalSquare.Top)
                    {
                        originalSquare.Minotaur = false;
                        destinationSquare.Minotaur = true;
                        _minotaurRow -= 1;
                    }
                }
                else if (_theseusRow > _minotaurRow)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow + 1);
                    if (!originalSquare.Bottom)
                    {
                        originalSquare.Minotaur = false;
                        destinationSquare.Minotaur = true;
                        _minotaurRow += 1;
                    }
                }
            }


            //minotour move right when theseus directly right, and move left when theseus directly left
            if (_theseusRow == _minotaurRow)
            {
                if (_theseusCol > _minotaurCol)
                {
                    destinationSquare = WhatIsAt(_minotaurCol + 1, _minotaurRow);
                    if (!originalSquare.Right)
                    {
                        originalSquare.Minotaur = false;
                        destinationSquare.Minotaur = true;
                        _minotaurCol += 1;
                    }
                }
                else if (_theseusCol < _minotaurCol)
                {
                    destinationSquare = WhatIsAt(_minotaurCol - 1, _minotaurRow);
                    if (!originalSquare.Left)
                    {
                        originalSquare.Minotaur = false;
                        destinationSquare.Minotaur = true;
                        _minotaurCol -= 1;
                    }
                }
            }

            //theseus is left and up,  minotour will move left , 
            //if there is a wall on the left, he will move up instead

            if (_minotaurCol > _theseusCol && _minotaurRow > _theseusRow)
            {
                //destinationSquare = WhatIsAt(_minotaurCol - 1, _minotaurRow);

                if (!originalSquare.Left)
                {
                    destinationSquare = WhatIsAt(_minotaurCol - 1, _minotaurRow);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurCol -= 1;
                }
                else if (originalSquare.Left && !originalSquare.Top)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow + 1);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurRow += 1;
                }

            }

            //theseus is left and down(bottom left),  minotour will move left , 
            //if there is a wall on the left, he will move down

            if (_minotaurCol > _theseusCol && _minotaurRow < _theseusRow)
            {
                //destinationSquare = WhatIsAt(_minotaurCol - 1, _minotaurRow);
                if (!originalSquare.Left)
                {
                    destinationSquare = WhatIsAt(_minotaurCol - 1, _minotaurRow);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurCol -= 1;
                }
                else if (originalSquare.Left && !originalSquare.Bottom)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow + 1);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurRow += 1;
                }

            }

            //theseus is right and up, minotour will move right 2 steps,
            //otherwise he moves up if there is no wall on top  
            if (_minotaurCol < _theseusCol && _minotaurRow > _theseusRow)
            {
                //destinationSquare = WhatIsAt(_minotaurCol + 1, _minotaurRow);
                if (!originalSquare.Right)
                {
                    destinationSquare = WhatIsAt(_minotaurCol + 1, _minotaurRow);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurCol += 1;
                }
                else if (originalSquare.Right && !originalSquare.Top)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow - 1);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurRow -= 1;
                }
            }

            //theseus is right and down inotour will move right 2 steps
            //otherwise he moves down if there is no wall at bottom  

            if (_minotaurCol < _theseusCol && _minotaurRow < _theseusRow)
            {
                //destinationSquare = WhatIsAt(_minotaurCol + 1, _minotaurRow);
                if (!originalSquare.Right)
                {
                    destinationSquare = WhatIsAt(_minotaurCol + 1, _minotaurRow);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurCol += 1;
                }
                else if (originalSquare.Right && !originalSquare.Bottom)
                {
                    destinationSquare = WhatIsAt(_minotaurCol, _minotaurRow + 1);
                    originalSquare.Minotaur = false;
                    destinationSquare.Minotaur = true;
                    _minotaurRow += 1;
                }
            }
        }

        public bool isTheseusDead()
        {
            bool result = false;
            if (_minotaurRow == _theseusRow && _minotaurCol == _theseusCol)
            {
                result = true;

            }
            return result;
        }

        public bool hasTheseusEscaped()
        {
            bool result = false;
            if (_theseusRow == _exitRow && _theseusCol == _exitCol)
            {
                result = true;
            }
            return result;
        }

    }
}


