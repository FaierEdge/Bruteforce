using System.Diagnostics;
//using System.Diagnostics.Metrics;
//using System.Windows.Forms;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Bruteforce
{
	internal class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Variables
			Console.Title = "Bruteforce";
			string Choice;
			string ArchivePath = "";
			string DictionaryPath = "";
			string FoundPassword = "";
			bool PasswordFound = false;
			int CheckedPasswords = 0;

			ShowLabel();
			Console.WriteLine("Program for bruteforcing archives.");
			Console.Write("Supports the ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("*.zip"); // Console.Write("*.zip, *.7z, and *.rar");
            Console.ForegroundColor= ConsoleColor.White;
			Console.WriteLine(" extensions for archives.");
			Console.Write("And the ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("*.txt");
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine(" extension for dictionaries.");
			Console.WriteLine();

			Console.WriteLine("Choose how to specify file paths:");
			Console.WriteLine("1 - Browse files using File Explorer");
			Console.WriteLine("2 - Enter paths manually");
			Console.WriteLine();
			Console.Write("Selection: ");
			Choice = Console.ReadLine();

			ShowLabel();
			if (Choice == "1")
			{
				Console.Write("Enter the path to the archive: ");
				ArchivePath = SelectFiles("Select archive", "Archive files (*.zip;*.7z;*.rar)|*.zip;*.7z;*.rar|All files (*.*)|*.*");
				Console.WriteLine();
				Console.Write("Enter the path to the dictionary: ");
				DictionaryPath = SelectFiles("Select dictionary", "Text files (*.txt)|*.txt|All files (*.*)|*.*");
			}
			else if (Choice == "2")
			{
                Console.Write("Enter the path to the archive: ");
                ArchivePath = Console.ReadLine();
				Console.WriteLine();
				Console.Write("Enter the path to the dictionary: ");
				DictionaryPath = Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Incorrect input!");
				return;
			}
			
			if (!File.Exists(ArchivePath))
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Error. ");
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Archive not found!");
            }
            if (!File.Exists(DictionaryPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Dictionary not found!");
            }
			if (!File.Exists(ArchivePath) || !File.Exists(DictionaryPath)) return;

			Stopwatch Timer = new Stopwatch();
			Timer.Start();

			File.ReadLines(DictionaryPath);
			
			foreach (string CurrentPassword in File.ReadLines(DictionaryPath))
			{
				CheckedPasswords++;
				//checkPass


			}
			//PasswordFound = true;

			if (PasswordFound)
			{
				Timer.Stop();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Password found!");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("Password: ");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(FoundPassword);
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(CheckedPasswords);
				Console.WriteLine(Timer); // решить проблему с таймером чтобы показывалось не более 100 мс
				//Console.WriteLine(CheckedPasswords / Timer);
				//checked:
				//time:
				//Speed:




			}













            // добавить в конце
            //Timer.Stop();
            //Console.WriteLine("время выполнения = " + Timer);


            //Console.WriteLine("sosi hui");
            //Console.ReadKey();
        }

        static void ShowLabel()
		{
			string Label = "===== BRUTEFORCE =====";
			ConsoleColor[] Pattern = [ConsoleColor.Red, ConsoleColor.Yellow, ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Blue];
			Console.Clear();
			for (int i = 0; i < Label.Length; i++)
			{
				Console.ForegroundColor = Pattern[i % Pattern.Length];
				Console.Write(Label[i]);
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine("\n");
		}

		static string SelectFiles(string Title, string Filter)
		{
			OpenFileDialog Dialog = new OpenFileDialog();
			Dialog.Title = Title;
			Dialog.Filter = Filter;
			Dialog.Multiselect = false;
			Dialog.InitialDirectory = Directory.GetCurrentDirectory();
			if (Dialog.ShowDialog() == DialogResult.OK)
			{
				Console.WriteLine(Path.GetFileName(Dialog.FileName));
				return Dialog.FileName;
			}
			Console.WriteLine(Dialog.FileName);
			return "";
		}
	}
}