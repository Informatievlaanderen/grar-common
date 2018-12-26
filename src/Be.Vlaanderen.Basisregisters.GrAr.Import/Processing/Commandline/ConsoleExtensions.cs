namespace Be.Vlaanderen.Basisregisters.GrAr.Import.Processing.Commandline
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
