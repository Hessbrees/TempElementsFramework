using System;
using TempElementsLib;
using TempElementsLib.Interfaces;

namespace TempElementsConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TempFile text = new TempFile("E:\\Test_srodsemestralny");
            text.AddText("abc");
            
        }
    }
}
