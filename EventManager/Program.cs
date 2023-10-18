// See https://aka.ms/new-console-template for more information
namespace EventManager
{
    internal class Program
    {
        static void ShowMainMenu()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("| Менеджер мероприятий  |");
            Console.WriteLine("|     Главное меню      |");
            Console.WriteLine("------------------------- \n\n");
            Console.WriteLine("[1] Добавить новую встречу");
            Console.WriteLine("[2] Изменить данные о встрече");
            Console.WriteLine("[3] Удалить существующую встречу");
            Console.WriteLine("[4] Вывести расписание встреч");
            Console.WriteLine("[5] Выход \n");
        }

        static int GetCommand()
        {
            Console.WriteLine("\nВведите номер команды и нажмите Enter \n");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int command))
            {
                command = -1;
            }
            return command;
        }

        static void ShowErrorMessage(string errorMessage)
        {

            Console.WriteLine(errorMessage);
            Console.ReadLine();
            Console.Clear();
        }


        static void Main(string[] args)
        {

            int currentCommand = 0;
            do
            {
                switch (currentCommand)
                {
                    case 0:
                        ShowMainMenu();
                        currentCommand = GetCommand();
                        break;

                    default:
                        ShowErrorMessage("Ошибка при вводе команды. Для продолжения нажмите любую клавишу...");
                        currentCommand = 0;
                        break;
                }
            } while (currentCommand != 5);
        }
    }
}