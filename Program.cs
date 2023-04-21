using System;
using System.Drawing;
using System.Text;

namespace ImageToASCII
{
    class Program
    {
        static void Main(string[] args)
        {
            int windowHeight = Console.WindowHeight;
            int developerInfoLine = windowHeight - 2;
            int windowWidth = Console.WindowWidth;
            string dividerLine = new string('-', windowWidth);
            string developerInfo = "Developer: Manish Joshi | Contact: joshimanish7@outlook.com | GitHub: https://github.com/Manish102866 | Version: 1.0.0";
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Green;
            string paddedDeveloperInfo = developerInfo.PadRight(windowWidth);
            Console.SetCursorPosition(0, developerInfoLine);
            Console.WriteLine(dividerLine);
            Console.WriteLine(paddedDeveloperInfo);
            Console.ResetColor();
            //Console.BufferHeight = windowHeight - 2;

            Console.SetCursorPosition(0, 0);
            Console.Write("Enter the path of the image file: ");
            string imagePath = Console.ReadLine();
            Bitmap bitmap = new Bitmap(imagePath);
            char[] asciiChars = { '@', '#', 'S', '%', '?', '*', '+', ';', ':', ',', '.', ' ' };
            double aspectRatio = (double)bitmap.Width / (double)bitmap.Height;
            int width = 80;
            int height = (int)Math.Round(width / aspectRatio);
            Bitmap resizedBitmap = new Bitmap(bitmap, width, height);
            StringBuilder asciiBuilder = new StringBuilder();
            try
            {
                Console.BufferHeight = windowHeight - 2;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.BufferHeight = windowHeight;
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = resizedBitmap.GetPixel(x, y);
                    int brightness = (int)(0.299 * pixelColor.R + 0.587 * pixelColor.G + 0.114 * pixelColor.B);
                    int asciiIndex = (int)Math.Round(brightness / 255.0 * (asciiChars.Length - 1));
                    asciiBuilder.Append(asciiChars[asciiIndex]);
                }
                asciiBuilder.Append(Environment.NewLine);
                if (Console.CursorTop >= windowHeight - 3)
                {
                    Console.MoveBufferArea(0, 1, windowWidth, windowHeight - 3, 0, 0);
                    Console.SetCursorPosition(0, windowHeight - 3);
                }
            }
            Console.WriteLine(asciiBuilder.ToString());
            Console.SetCursorPosition(0, windowHeight - 1);
            Console.WriteLine(dividerLine);
            Console.WriteLine(paddedDeveloperInfo);
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadLine();
        }
    }
}
