int[] targetPosition = new int[3];  //0 = x and column, 1 = y and row, and position value of target coin (0-63, 8x8).
int[] flipPosition = new int[3]; // 0 = x, 1 = y, and position value of the correct coin.
bool[,] board = new bool[8, 8]; // randomized board of heads and tails.
Random rand = new Random();
string target = null; //binary
string current = null; //binary
string change = null; //binary
string boardDisplay = null;
int counter = 0; // counter for heads in specific positions.



Console.WriteLine("What column is the target coin in? (1 = left, 8 = right)");
do
{
    targetPosition[0] = Convert.ToInt16(Console.ReadLine());
} while (1 > targetPosition[0] || targetPosition[0] > 8);

Console.WriteLine("What row is the target coin in? (1 = top, 8 = bottom)");
do
{
    targetPosition[1] = Convert.ToInt16(Console.ReadLine());
} while (1 > targetPosition[1] || targetPosition[1] > 8);


targetPosition[2] = ((targetPosition[0] - 1) + ((targetPosition[1] - 1) * 8));



for (int i = 0; i < 6; i++)
{
    if (32 / (Math.Pow(2, i)) <= targetPosition[2])
    {
        target = target + "1";
        targetPosition[2] = targetPosition[2] % (32 / ((int)Math.Pow(2, i)));
    }
    else
    {
        target = target + '0';
    }
}
//Converts target into a binary



for (int i = 0; i < 8; i++)
{
    for (int j = 0; j < 8; j++)
    {
        board[i, j] = (rand.Next(0, 2) == 1) ? true : false;
    }
}
//Randomizes Board



//"Reads" the random board
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
//"Reads" the random board



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
Console.WriteLine("\n");

Console.ReadLine();
