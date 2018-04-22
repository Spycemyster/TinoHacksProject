using System;

namespace TinoHacksGame
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Super Stress Bros. Version 1.0.0a");
            Console.WriteLine("Written by Spencer Chang, Henry Li, Ryan Niu, Joshua Hong");
            using (var game = new Game1())
                game.Run();
        }
    }
}
