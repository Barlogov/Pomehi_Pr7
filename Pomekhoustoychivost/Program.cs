﻿class Pomekhoustoychivost
{
    static int[] _e = new []{1,2,4,8,3,6,12,11,5,10,7,14,15,13,9, 0};
// Test    
    // static int[,] _delimoe = new int[,]
    // {
    //     // Ex^14 -> Ex^6
    //     // E^
    //     {6, 3, 6, 0, 0, 0, 5, 4, 9},
    //     // x^
    //     {14, 13, 12, 11, 10, 9, 8, 7, 6}
    // };
    //     
    // static int[,] _delitel = new int[,]
    // {
    //     // Ex^6 -> Ex^0
    //     // E^
    //     {0, 10, 14, 4, 6, 9, 6},
    //     // x^
    //     {6, 5, 4, 3, 2, 1, 0}
    // };
// My var from pr 6
    // static int[,] _delimoe = new int[,]
    // {
    //     // Ex^14 -> Ex^6
    //     // E^
    //     {1, 15, 4, 8, 9, 3, 1},
    //     // x^
    //     {6, 0, 4, 3, 2, 1, 0}
    // };
    //     
    // static int[,] _delitel = new int[,]
    // {
    //     // Ex^6 -> Ex^0
    //     // E^
    //     {3, 7, 8, 2, 14},
    //     // x^
    //     {5, 4, 3, 2, 1}
    // };
    
// My var from pr 7
    static int[,] _delimoe = new int[,]
    {
        // Ex^14 -> Ex^6
        // E^
        {14, 13, 13, 2, 13, 12, 13, 6, 13},
        // x^
        {14, 13, 12, 11, 10, 9, 8, 7, 6}
    };
        
    static int[,] _delitel = new int[,]
    {
        // Ex^6 -> Ex^0
        // E^
        {0, 10, 14, 4, 6, 9, 6},
        // x^
        {6, 5, 4, 3, 2, 1, 0}
    };
    
    static void Main()
    {
        // int [0, E]
        // int [1, X]
        // GetLength(0) == кол-во строк (сверху вниз)
        // GetLength(1) == кол-во столбцов (слева на право)
        
        int[,] ostatocOtDelenia = new int[2, _delitel[1,0]+1];
        int[,] otvet = new int[2, _delimoe[1,0]-_delitel[1,0]+1];

        for (int i = 0; i < _delimoe.GetLength(1); i++)
        {
            Console.Write("E^{0,2} x^{1,2}", _delimoe[0,i].ToString(), _delimoe[1,i].ToString());
            if (i < _delimoe.GetLength(1) - 1)
            {
                Console.Write(" + ");
            }
        }
        Console.Write(" | / | ");
        for (int i = 0; i < _delitel.GetLength(1); i++)
        {
            Console.Write("E^{0,2} x^{1,2}", _delitel[0,i].ToString(), _delitel[1,i].ToString());
            if (i < _delitel.GetLength(1) - 1)
            {
                Console.Write(" + ");
            }
        }
        // --Вывод начала деления

        otvet = DivEMnogochlen(_delimoe, _delitel, otvet, 0,ref ostatocOtDelenia);

        Console.WriteLine();
        Console.WriteLine("Otvet: f(x) = ");
        
        for (int i = 0; i < otvet.GetLength(1); i++)
        {
            Console.Write("E^{0,2} x^{1,2}", otvet[0,i].ToString(), otvet[1,i].ToString());
            if (i < otvet.GetLength(1) - 1)
            {
                Console.Write(" + ");
            }
        }
        
        Console.WriteLine();
        Console.WriteLine("Ostatoc Ot Delenia: r(x) = ");
        
        for (int i = 0; i < ostatocOtDelenia.GetLength(1); i++)
        {
            
            Console.Write("E^{0,2} x^{1,2}", ostatocOtDelenia[0,i].ToString(), ostatocOtDelenia[1,i].ToString());
            if (i < ostatocOtDelenia.GetLength(1) - 1)
            {
                Console.Write(" + ");
            }
        }
        
        Console.ReadKey();
        Console.ReadKey();
        Console.ReadKey();
        
    }
    public static int[,] DivEMnogochlen(int[,] delimoe, int[,] delitel, int[,] otvet, int iteration, ref int[,] ostatocOtDelenia)
        {
            int[,] promejutoc = new int[2, delitel.GetLength(1)];
            int[,] novoeDelimoe = new int[2, delitel.GetLength(1)];

            

                if (delimoe[1,0]>=delitel[1,0])
                {
                    // Запись частного
                    int raznE= delimoe[0, 0] - delitel[0, 0];
                    otvet[0, iteration] = raznE;
                    
                    int raznX = delimoe[1, 0] - delitel[1, 0];
                    otvet[1, iteration] = raznX;
                    
                    // Запись промежутка для вычетания 
                    for (int i = 0; i < delitel.GetLength(1); i++)
                    {
                        // E
                        promejutoc[0, i] = (raznE + delitel[0, i]) % 15;
                        // X
                        promejutoc[1, i] = (raznX + delitel[1, i]);
                    }

                    // Вычитание
                    for (int i = 1; i < promejutoc.GetLength(1); i++)
                    {
                        //Можно добавить исключение, если есть пропуск в степени Х
                        
                        // E
                        novoeDelimoe[0, i-1] = NumberE(_e[delimoe[0, i]] ^ _e[promejutoc[0, i]]);
                        // X
                        novoeDelimoe[1, i-1] = promejutoc[1, i];
                    }
                    
                    // Не умеет скидывать 2 числа одновременно (в случае, если степень перескакивает через один (6 -> 4))
                    // Скинуть значение с _делимого, если оно есть
                    if (delitel.GetLength(1) + iteration < _delimoe.GetLength(1))
                    {
                        // E
                        novoeDelimoe[0, novoeDelimoe.GetLength(1) - 1] = _delimoe[0, delitel.GetLength(1) + iteration];
                        // X
                        novoeDelimoe[1, novoeDelimoe.GetLength(1) - 1] = _delimoe[1, delitel.GetLength(1) + iteration];
                    }
                    else
                    {
                        // E
                        novoeDelimoe[0, novoeDelimoe.GetLength(1) - 1] = 15;
                        // X
                        novoeDelimoe[1, novoeDelimoe.GetLength(1) - 1] = 0;
                    }

                    int otstup = 12;
                    // Вывод промежуточных данных
                    Console.WriteLine();
                    for (int i = 0; i < otstup*iteration; i++)
                    {
                        Console.Write(" ");
                    }
                    for (int i = 0; i < promejutoc.GetLength(1); i++)
                    {
                        Console.Write("E^{0,2} x^{1,2}", promejutoc[0,i].ToString(), promejutoc[1,i].ToString());
                        if (i < promejutoc.GetLength(1) - 1)
                        {
                            Console.Write(" + ");
                        }
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < otstup*iteration; i++)
                    {
                        Console.Write(" ");
                    }
                    for (int i = 0; i < promejutoc.GetLength(1); i++)
                    {
                        Console.Write("------------");
                    }
                    Console.WriteLine("");
                    for (int i = 0; i < otstup*(iteration+1); i++)
                    {
                        Console.Write(" ");
                    }
                    for (int i = 0; i < novoeDelimoe.GetLength(1); i++)
                    {
                        Console.Write("E^{0,2} x^{1,2}", novoeDelimoe[0,i].ToString(), novoeDelimoe[1,i].ToString());
                        if (i < novoeDelimoe.GetLength(1) - 1)
                        {
                            Console.Write(" + ");
                        }
                    }
                    
                    iteration++;
                    
                    // Вызов себя
                    otvet = DivEMnogochlen(novoeDelimoe, delitel, otvet, iteration,ref ostatocOtDelenia);
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < delimoe.GetLength(1); j++)
                        {
                            ostatocOtDelenia[i, j] = delimoe[i, j];
                        }
                    }
                }
                
                
                
                
                
                return otvet;
        }
    
        public static int NumberE(int num)
        {
            for (int i = 0; i < _e.Length; i++)
            {
                if (_e[i]==num)
                {
                    return i;
                }
            }

            return 404;
        }
    
}