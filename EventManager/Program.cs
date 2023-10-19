// See https://aka.ms/new-console-template for more information
namespace EventManager
{
    public static class DateTimeExtensions
    {
        public static bool IsInRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }

    internal class Program
    {
        struct Event
        {
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        static List<Event> events = new List<Event>();

        static void ShowMainMenu()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("| Менеджер мероприятий  |");
            Console.WriteLine("|     Главное меню      |");
            Console.WriteLine("------------------------- \n\n");
            Console.WriteLine("[1] Запланировать новую встречу");
            Console.WriteLine("[2] Изменить данные о встрече");
            Console.WriteLine("[3] Удалить существующую встречу");
            Console.WriteLine("[4] Вывести расписание встреч");
            Console.WriteLine("[5] Выход \n");
        }

        static int GetCommand()
        {
            Console.WriteLine("\nВведите номер команды и нажмите клавишу Enter \n");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int command))
            {
                command = -2;
            }
            return command;
        }

        static DateTime GetDateTime()
        {
            DateTime date = new DateTime();

            bool datevalid = false;

            Console.WriteLine("в формате ДД.ММ.ГГГГ ЧЧ:ММ:СС и нажмите Enter\n");

            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    datevalid = true;
                }
                else
                {
                    Console.WriteLine("\nНеверный формат ввода. Попробуйте ещё раз\n");
                }
            } while (!datevalid);

            return date;
        }

        static void GetEventDateTime(string Message, out DateTime EventDateTime)
        {
            bool datevalid = false;

            do
            {
                Console.WriteLine(Message);

                EventDateTime = GetDateTime();

                if (DateAvailable(EventDateTime))
                {
                    datevalid = true;
                }
                else
                {
                    Console.WriteLine("Дата и время планируемой встречи не могут быть раньше текущей даты и времени");
                }


            } while (!datevalid);
        }

        static bool DateAvailable(DateTime date)
        {
            return date > DateTime.Now;
        }

        static void AddEvent()
        {
            Console.Clear();
            Console.WriteLine("Введите краткое описание встречи и нажмите клавишу Enter\n");
            string description = Console.ReadLine();

            
            GetEventDateTime("\nВведите дату и время начала встречи", out DateTime startDateTime);
            GetEventDateTime("\nВведите дату и время окончания встречи", out DateTime endDateTime);

            if (startDateTime > endDateTime) { Console.WriteLine("Время окончания встречи не может быть раньше её начала"); }

            events.Add(new Event
            {
                Description = description,
                StartDate = startDateTime,
                EndDate = endDateTime
            });

            Console.WriteLine("\nВстреча " + description + " запланирована!");
        }

        static void ShowAllEvents()
        {
            Console.Clear();

                foreach (Event e in events)
                {
                    if (e.StartDate > DateTime.Now)
                    {
                        Console.WriteLine("-------------------------------------------");
                        Console.WriteLine(e.Description + "\n");
                        Console.WriteLine("Дата и время начала: " + e.StartDate);
                        Console.WriteLine("Дата и время окончания: " + e.EndDate);
                        Console.WriteLine("-------------------------------------------");
                    }
                    else
                    {
                        Console.WriteLine("У вас нет предстоящих встреч");
                    }
            }


        }

        static void BackToMainMenu()
        {
            Console.WriteLine("\nДля возврата в главное меню нажмите любую клавишу");
            Console.ReadLine();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            int currentCommand = 0;

            events.Add(new Event
            {
                Description = "ЛОРОР",
                StartDate = DateTime.Parse("27.10.2023 19:11:02"),
                EndDate = DateTime.Parse("27.10.2023 21:39:45")
            });

            events.Add(new Event
            {
                Description = "дылвдлы",
                StartDate = DateTime.Parse("28.10.2023 19:11:02"),
                EndDate = DateTime.Parse("28.10.2023 21:39:45")
            });


            do
            {
                switch (currentCommand)
                {
                    case -1:
                        BackToMainMenu();
                        currentCommand = 0;
                        break;
                    case 0:
                        ShowMainMenu();
                        currentCommand = GetCommand();
                        break;

                    case 1:
                        AddEvent();
                        currentCommand = 0;
                        break;

                    case 4:
                        ShowAllEvents();
                        currentCommand = -1;
                        break;

                    default:
                        Console.WriteLine("\nКоманда не найдена");
                        currentCommand = -1;
                        break;
                }
            } while (currentCommand != 5);
        }
    }
}