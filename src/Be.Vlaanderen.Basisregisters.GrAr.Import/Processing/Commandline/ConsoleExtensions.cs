namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.CommandLine
{
    using System;

    public static class ConsoleExtensions
    {
        public static void WaitFor(ConsoleKey key)
        {
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
            } while (input.Key != key);
        }
    }
}
