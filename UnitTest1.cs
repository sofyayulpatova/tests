using System.Transactions;
using Xunit;
using TicTacToe.Models;
namespace TicTacToeTests
{
    public class UnitTest1
    {
        [Fact]
        public void NextPlayerIsO_True()
        {
            
            Game TicTacToe = new Game(); // initialize the gameboard empty
            TicTacToe.CurrentPlayer = "X";

            TicTacToe.NowPlays();


            Assert.Equal("O", TicTacToe.CurrentPlayer);
        }
        [Fact]
        public void NextPlayerIsX_True()
        {

            Game TicTacToe = new Game(); // initialize the gameboard empty
            TicTacToe.CurrentPlayer = "O";

            TicTacToe.NowPlays();


            Assert.Equal("X", TicTacToe.CurrentPlayer);
        }

        [Fact]
        public void IsBoardFull_True()
        {
            Game TicTacToe = new Game(); // initialize the gameboard empty
                                         // board Initialization
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "O", "X" },
                new List<string> { "X", "X", "O" },
                new List<string> { "O", "X", "O" }
            };

            var result = TicTacToe.BoardFull();

            Assert.True(result);
        }

        [Fact]
        public void IsBoardFull_False()
        {
            Game TicTacToe = new Game(); // initialize the gameboard empty
                                         // board Initialization
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "", "", "" },
                new List<string> { "", "", "" },
                new List<string> { "", "", "" }
            };

            var result = TicTacToe.BoardFull();

            Assert.False(result);
        }

        [Fact]
        public void WinnerExistsHorizontal_True()
        {
           
            var TicTacToe = new Game();
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "X", "X" },
                new List<string> { "O", "O", "" },
                new List<string> { "", "", "" }
            };
            TicTacToe.CurrentPlayer = "X";

            
            var result = TicTacToe.WinnerExists();

            
            Assert.True(result);
        }

        public void WinnerExistsVertical_True()
        {
            var TicTacToe = new Game();
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "O", "" },
                new List<string> { "X", "O", "" },
                new List<string> { "X", "", "" }
            };
            TicTacToe.CurrentPlayer = "X";


            var result = TicTacToe.WinnerExists();


            Assert.True(result);
        }
        public void WinnerExistsDiagonal_True()
        {
            var TicTacToe = new Game();
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "O", "" },
                new List<string> { "O", "X", "" },
                new List<string> { "", "", "X" }
            };
            TicTacToe.CurrentPlayer = "X";


            var result = TicTacToe.WinnerExists();


            Assert.True(result);
        }

        public void WinnerExists_False()
        {
            var TicTacToe = new Game();
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "O", "" },
                new List<string> { "X", "O", "" },
                new List<string> { "O", "X", "" }
            };
            TicTacToe.CurrentPlayer = "X";


            var result = TicTacToe.WinnerExists();


            Assert.False(result);
        }

        [Fact]
        public void MakeMove_MoveValid_PlayerSwitched()
        {
            
            var TicTacToe = new Game();
            TicTacToe.CurrentPlayer = "X";
            TicTacToe.GameState = States.Process;
            TicTacToe.Board[0][0] = ""; 

            
            var result = TicTacToe.MakeMove(0, 0);

            
            Assert.True(result);
            Assert.Equal("O", TicTacToe.CurrentPlayer);  //  player to switch from X to O
            Assert.Equal("X", TicTacToe.Board[0][0]);  // Cell = X
        }

        [Fact]
        public void MakeMove_MoveInvalid_PlayerNotSwitched()
        {
            
            var TicTacToe = new Game();
            TicTacToe.CurrentPlayer = "X";
            TicTacToe.GameState = States.Win;  
            TicTacToe.Board[0][0] = "O"; 

            
            var result = TicTacToe.MakeMove(0, 0);

            
            Assert.False(result);  // Move should fail
            Assert.Equal("X", TicTacToe.CurrentPlayer);  // Player should be the same
            Assert.Equal("O", TicTacToe.Board[0][0]);  // Cell value shouldnt be changed 
        }




        [Theory]
        [InlineData("X", new string[] { "X", "X", "" }, 0, 2, States.Win)] // Winning move for X
        [InlineData("O", new string[] { "O", "O", "" }, 0, 2, States.Win)] // Winning move for O
        public void MakeMove_WinningMoves(string player, string[] preMoveRow, int row, int col, States expectedState)
        {
            
            var TicTacToe = new Game();
            TicTacToe.CurrentPlayer = player;
            TicTacToe.Board[row] = new List<string>(preMoveRow);

            
            var result = TicTacToe.MakeMove(row, col);

            
            Assert.True(result);
            Assert.Equal(expectedState, TicTacToe.GameState);
        }


        [Fact]
        public void MakeMove_DrawingMove_SetsGameStateToDraw()
        {
            
            var TicTacToe = new Game();
            TicTacToe.CurrentPlayer = "X";
            // almpost full board without a winner
            TicTacToe.Board = new List<List<string>>
            {
                new List<string> { "X", "O", "X" },
                new List<string> { "X", "O", "O" },
                new List<string> { "O", "X", "" }  
            };

            
            var result = TicTacToe.MakeMove(2, 2);

            
            Assert.True(result);
            Assert.Equal(States.Draw, TicTacToe.GameState);
        }


    }
}