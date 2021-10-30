﻿using System;

namespace Quoridor.Controller
{
    public static class CustomConsole
    {
        public static string ReadLine()
        {
            Console.Write("-> ");
            return Console.ReadLine();
        }
        public static void WriteLine(string line)
        {
            Console.WriteLine("<- " + line);
        }
    }
}