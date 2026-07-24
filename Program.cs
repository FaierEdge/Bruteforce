using System.Diagnostics;
using System.Security.Authentication;
using Ionic.Zip;

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
			int TimeSeconds, TimeMilliseconds, Seconds, Hours, Minutes, Milliseconds;
			double Speed;
            Stopwatch Timer = new Stopwatch();

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
			//Choice = Console.ReadLine();

			ShowLabel();
			/*if (Choice == "1")
			{
				Console.Write("Enter the path to the archive: ");
				ArchivePath = SelectFiles("Select archive", "Archive files (*.zip)|*.zip|All files (*.*)|*.*"); // ArchivePath = SelectFiles("Select archive", "Archive files (*.zip;*.7z;*.rar)|*.zip;*.7z;*.rar|All files (*.*)|*.*");
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
			if (!File.Exists(ArchivePath) || !File.Exists(DictionaryPath)) return;*/
			
			//=============TEMP============
			ArchivePath = "C:\\Users\\Faier_Edge\\Desktop\\blya5.zip";
			DictionaryPath = "C:\\Users\\Faier_Edge\\Desktop\\Numbers.txt";
			//=============================

			Timer.Start();

            try
			{
				using (ZipFile Zip = ZipFile.Read(ArchivePath))
				{
					foreach (string CurrentPassword in File.ReadLines(DictionaryPath))
					{
						CheckedPasswords++;
						try
						{
							Zip.Password = CurrentPassword;
							foreach (ZipEntry Entry in Zip)
							{
								Entry.Extract(Path.GetTempPath(), ExtractExistingFileAction.OverwriteSilently);
								PasswordFound = true;
								FoundPassword = CurrentPassword;
								break;
							}
							if (PasswordFound) break;
						}
						catch {}
					}
				}
			}
			catch (Exception Ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: {Ex.Message}");
				Console.ForegroundColor = ConsoleColor.White;
			}



            ShowLabel();
			Timer.Stop();
			if (PasswordFound)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Password found!\n");
				Console.ForegroundColor = ConsoleColor.White;
				Console.Write("Password: ");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(FoundPassword);
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Password not found!\n");
			}
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"Passwords checked: {CheckedPasswords}");
            Console.Write($"Time: ");
			if (Timer.Elapsed.Hours < 10) Console.Write($"0{Timer.Elapsed.Hours}:");
			if (Timer.Elapsed.Minutes < 10) Console.Write($"0{Timer.Elapsed.Minutes}:");
			if (Timer.Elapsed.Seconds < 10) Console.Write($"0{Timer.Elapsed.Seconds}:");
			Console.WriteLine(Timer.Elapsed.Milliseconds);
			Speed = CheckedPasswords / Timer.Elapsed.TotalSeconds;
			Console.WriteLine($"Speed: {Speed:F2} psw/s");
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