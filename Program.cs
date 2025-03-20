using System.Security.Cryptography;

int[] keyLocation = new int[3];  //0 = x and column, 1 = y and row, and position value of target coin (0-63, 8x8).
int[] flipPosition = new int[3]; // 0 = x, 1 = y, and position value of the correct coin.
bool[,] board = new bool[8, 8]; // randomized board of heads and tails.
Random rand = new Random();
string target = null; //binary
string current = null; //binary
string change = null; //binary
string boardDisplay = null;
int counter = 0; // counter for heads in specific positions.
bool inMenu = true; // Determines whether to display start menu again
bool hidingKey = true; // Determines whether to keep displaying key hiding board
string customOrRandomize = null;

do
{
    Console.WriteLine("Would you like to make a custom board, or randomize one? (Type custom or randomize)");
    customOrRandomize = Console.ReadLine();
    Console.Clear();
    if (customOrRandomize == "custom")
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = false;
            }
        }
           inMenu = false;
    }
    else if (customOrRandomize == "randomize")
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = (rand.Next(0, 2) == 1) ? true : false;
            }
        }
        inMenu = false;
    }
} while (inMenu);
//Menu

//Reads current board
for (int i = 0; i < 8; i++)
{
for (int j = 1; j < 9; j = j + 2)
{
counter += (board[i, j] == true) ? 1 : 0;
}
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;

for (int i = 0; i < 8; i++)
{
counter += (board[i, 2] == true) ? 1 : 0;
counter += (board[i, 3] == true) ? 1 : 0;
counter += (board[i, 6] == true) ? 1 : 0;
counter += (board[i, 7] == true) ? 1 : 0;
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;

for (int i = 0; i < 8; i++)
{
for (int j = 4; j < 8; j++)
{
counter += (board[i, j] == true) ? 1 : 0;
}
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;

for (int i = 1; i < 9; i = i + 2)
{
for (int j = 0; j < 8; j++)
{
counter += (board[i, j] == true) ? 1 : 0;
}
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;

for (int i = 0; i < 8; i++)
{
counter += (board[2, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
counter += (board[3, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
counter += (board[6, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
counter += (board[7, i] == true) ? 1 : 0;
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;

for (int i = 4; i < 8; i++)
{
for (int j = 0; j < 8; j++)
{
counter += (board[i, j] == true) ? 1 : 0;
}
}
current = (counter % 2 == 1) ? "1" + current : "0" + current;
counter = 0;
//Reads current board

//Hiding Key and changing board
keyLocation[0] = 0;
keyLocation[1] = 0;
do
{
    //Displays board
    Console.WriteLine("Use arrow keys to move, spacebar to flip a tile, and enter hide key under current space / finish!");
    for (int i = 0; i < 8; i++)
    {
        for (int j = 0; j < 8; j++)
        {
            boardDisplay = (board[i, j] == true) ? boardDisplay + " 1 " : boardDisplay + " 0 ";
        }
        boardDisplay = (keyLocation[0] == i) ? boardDisplay + "<" : boardDisplay;
        Console.WriteLine(boardDisplay);
        boardDisplay = null;
    }
    for (int i = 0; i < keyLocation[1]; i++)
    {
        boardDisplay = boardDisplay + "   ";
    }
    boardDisplay = boardDisplay + " ^";
    Console.WriteLine(boardDisplay);
    boardDisplay = null;
    Console.WriteLine("\n");
    //Displays board

    //Reads key input and moves position on board
    ConsoleKey keyInput = Console.ReadKey().Key;
    Console.WriteLine("\n");
    while (keyInput != ConsoleKey.LeftArrow && keyInput != ConsoleKey.RightArrow && keyInput != ConsoleKey.UpArrow && keyInput != ConsoleKey.DownArrow && keyInput != ConsoleKey.Spacebar && keyInput != ConsoleKey.Enter)
    {
        Console.WriteLine("Please press only the arrow keys, spacebar, or enter! c: \n");
        keyInput = Console.ReadKey().Key;
    }
    if (keyInput == ConsoleKey.LeftArrow)
    {
        if (keyLocation[1] > 0)
        {
            keyLocation[1]--;
        }
    }
    else if (keyInput == ConsoleKey.RightArrow)
    {
        if (keyLocation[1] < 7)
        {
            keyLocation[1]++;
        }
    }
    else if (keyInput == ConsoleKey.UpArrow)
    {
        if (keyLocation[0] > 0)
        {
            keyLocation[0]--;
        }
    }
    else if (keyInput == ConsoleKey.DownArrow)
    {
        if (keyLocation[0] < 7)
        {
            keyLocation[0]++;
        }
    }
    else if (keyInput == ConsoleKey.Spacebar)
    {
        board[keyLocation[0], keyLocation[1]] = !board[keyLocation[0], keyLocation[1]];
    }
    else
    {
        hidingKey = false;
    }
    //Reads key input and moves position on board
    Console.Clear();
} while (hidingKey);
//Hiding key and changing board


keyLocation[2] = ((keyLocation[0]) + ((keyLocation[1]) * 8));


//Converts target into a binary
for (int i = 0; i < 6; i++)
{
    if (32 / (Math.Pow(2, i)) <= keyLocation[2])
    {
        target = target + "1";
        keyLocation[2] = keyLocation[2] % (32 / ((int)Math.Pow(2, i)));
    }
    else
    {
        target = target + '0';
    }
}
//Converts target into a binary


for (int i = 0; i < 6; i++)
{
    change = (target[i] == current[i]) ? change + "0" : change + "1";
}

Console.WriteLine("\n");
Console.WriteLine(target + " is target coin.");
Console.WriteLine(current + " is current board.");
Console.WriteLine(change + " is coin to flip.");



for (int i = 0; i < 6; i++)
{
    if (change[i] == '1')
    {
        flipPosition[2] += (32 / ((int)Math.Pow(2, i)));
    }
}

flipPosition[1] = (flipPosition[2] % 8);
flipPosition[0] = ((flipPosition[2] - flipPosition[1]) / 8);

Console.WriteLine("flip the coin in position " + flipPosition[2] + "!");
Console.WriteLine("flip the coin in column " + (flipPosition[1] + 1) + " and row " + (flipPosition[0] + 1) + "!"); // x,y coordinate (except y is going down... x and y cannot be 0)
Console.WriteLine("\n");

for (int i = 0; i < 8; i++)
{
    for (int j = 0; j < 8; j++)
    {
        boardDisplay = (board[i, j] == true) ? boardDisplay + " 1 " : boardDisplay + " 0 ";

        //edited
        if (flipPosition[0] == i && flipPosition[1] == j)
        {
            board[i, j] = !board[i, j];
        }
        //edited
    }
    boardDisplay = (flipPosition[0] == i) ? boardDisplay + "<" : boardDisplay;
    Console.WriteLine(boardDisplay);
    boardDisplay = null;
}
for (int i = 0; i < flipPosition[1]; i++)
{
    boardDisplay = boardDisplay + "   ";
}
boardDisplay = boardDisplay + " ^";
Console.WriteLine(boardDisplay);
boardDisplay = null;
Console.WriteLine("\n");

//edited

string current2 = null;

for (int i = 0; i < 8; i++)
{
    for (int j = 1; j < 9; j = j + 2)
    {
        counter += (board[i, j] == true) ? 1 : 0;
    }
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

for (int i = 0; i < 8; i++)
{
    counter += (board[i, 2] == true) ? 1 : 0;
    counter += (board[i, 3] == true) ? 1 : 0;
    counter += (board[i, 6] == true) ? 1 : 0;
    counter += (board[i, 7] == true) ? 1 : 0;
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

for (int i = 0; i < 8; i++)
{
    for (int j = 4; j < 8; j++)
    {
        counter += (board[i, j] == true) ? 1 : 0;
    }
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

for (int i = 1; i < 9; i = i + 2)
{
    for (int j = 0; j < 8; j++)
    {
        counter += (board[i, j] == true) ? 1 : 0;
    }
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

for (int i = 0; i < 8; i++)
{
    counter += (board[2, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
    counter += (board[3, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
    counter += (board[6, i] == true) ? 1 : 0;
}
for (int i = 0; i < 8; i++)
{
    counter += (board[7, i] == true) ? 1 : 0;
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

for (int i = 4; i < 8; i++)
{
    for (int j = 0; j < 8; j++)
    {
        counter += (board[i, j] == true) ? 1 : 0;
    }
}
current2 = (counter % 2 == 1) ? "1" + current2 : "0" + current2;
counter = 0;

int[] newCoin = new int[3];
for (int i = 0; i < 6; i++)
{
    if (current2[i] == '1')
    {
        newCoin[2] += (32 / ((int)Math.Pow(2, i)));
    }
}

newCoin[1] = (newCoin[2] % 8);
newCoin[0] = ((newCoin[2] - newCoin[1]) / 8);


for (int i = 0; i < 8; i++)
{
    for (int j = 0; j < 8; j++)
    {
        boardDisplay = (board[i, j] == true) ? boardDisplay + " 1 " : boardDisplay + " 0 ";
    }
    boardDisplay = (newCoin[0] == i) ? boardDisplay + "<" : boardDisplay;
    Console.WriteLine(boardDisplay);
    boardDisplay = null;
}
for (int i = 0; i < newCoin[1]; i++)
{
    boardDisplay = boardDisplay + "   ";
}
boardDisplay = boardDisplay + " ^";
Console.WriteLine(boardDisplay);
Console.WriteLine("\n");

Console.ReadLine();


/*
Bool multiarray
Randomize and print out multiarray
Show the spot to change in the multiarray
*/