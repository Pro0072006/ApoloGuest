using System;
using System.Threading;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NumberGuest.Helpers
{
	internal class Questions
	{
		private readonly List<Question> questions;
		private readonly Player Player;
		private string Answer;

		public Questions(Player player)
		{
			questions = JsonConvert.DeserializeObject<List<Question>>(File.ReadAllText("TextQuestion.json"));
			Player = player;
			Answer = string.Empty;
		}

		public void InitQuestions()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Entrando a las preguntas en: ");

			for (int i = 5; i >= 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(1000);
			}

			Console.Clear();
			Console.ForegroundColor = ConsoleColor.Cyan;

			Console.WriteLine("Holaaa... Bienvenido a las preguntas, una pregunta sera revelada ante ti...");
			Console.WriteLine("Si la contestas correctamente recibiras 10 de dinero");
			Console.WriteLine("De lo contrario sufriras las consecuencias");

			Question question = GetQuestion();

			Console.WriteLine($"\n\n {question.TextQuestion}");

			Console.Write("\nPon tu respuesta: ");
			Answer = Console.ReadLine();
			Answer = Answer.ToUpper();

			while (Answer != "A" && Answer != "B" && Answer != "C" && Answer != "D")
			{
				Console.Write("\nRespuesta invalida, Intente nuevamente: ");
				Answer = Console.ReadLine();
				Answer = Answer.ToUpper();
			}

			if (Convert.ToChar(Answer) != question.Answer)
			{
				Incorrect(question.Answer);
				return;
			}

			Correct();
		}

		private Question GetQuestion()
		{
			Random random = new();

			return questions[random.Next(0, questions.Count)];
			
		}

		private void Correct()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n\nCorrecto!!, Respuesta correcta!!!!!");
			Thread.Sleep(1000);

			Console.ResetColor();
			Console.WriteLine("\nTus monedas ya fueron depositadas en tu billetera");
			Player.MoneyOperation(10, true);
			Console.WriteLine($"Dinero restante: {Player.Money}\n");

			Thread.Sleep(4000);

			Console.Clear();
		}

		private void Incorrect(char question)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n\nLo lamento, respuesta incorrecta😣");
			Thread.Sleep(1000);
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"La respuesta correcta era: {question}");

			Console.ResetColor();
			Thread.Sleep(1500);
			Console.WriteLine("\nComo castigo perderas 5 moneditas de oro, ademas de una espera de 20 segundos");

			Thread.Sleep(800);
			Player.MoneyOperation(5, false);
			Console.WriteLine($"Dinero restante: {Player.Money}\n");

			Console.WriteLine("Tiempo restante:");
			for (int i = 20; i > 0; i--)
			{
				Console.WriteLine(i);
				Thread.Sleep(1000);
			}

			Thread.Sleep(1000);
			Console.Clear();
		}
	}

	public class Question
	{
		public int ID { get; set; }
		public string TextQuestion { get; set; }
		public char Answer { get; set; }
	}
}
