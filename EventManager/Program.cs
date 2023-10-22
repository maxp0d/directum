// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using System.Text;

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
        struct Eventstruct
        {
            public string Description { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        static List<Event> events = new List<Event>();
        
        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("-------------------------");
            Console.WriteLine("| Менеджер мероприятий  |");
            Console.WriteLine("|     Главное меню      |");
            Console.WriteLine("------------------------- \n\n");
            Console.WriteLine("[1] Запланировать новую встречу");
            Console.WriteLine("[2] Вывести все предстоящие встречи");
            Console.WriteLine("[3] Вывести расписание на заданный день (Изменить/Удалить/Сохранить)");
            Console.WriteLine("[4] Выход \n");
        }

        static void ShowBackToMainMenu()
        {
            Console.WriteLine("\nДля возврата в главное меню нажмите любую клавишу");
            Console.ReadKey();
            Console.Clear();
        }
        static void ShowEventInfo(Event e)
        {
            Console.WriteLine("[" + e.Description + "]\n");
            Console.WriteLine("Начало: " + e.StartDate);
            Console.WriteLine("Окончание: " + e.EndDate);
            if (e.IsNotifierEnabled)
            {
                Console.WriteLine("Напоминание о встрече: " + (e.StartDate - e.NotifierTime));
            }
            Console.WriteLine("------------------------------\n");
        }

        static void ShowFutureEvents(List<Event> list)
        {
            Console.Clear();
            bool noevents = true;
         
            if (events.Count > 0)
            {
                foreach (Event e in list)
                {
                    if (e.StartDate > DateTime.Now)
                    {
                        ShowEventInfo(e);
                        noevents = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("База встреч пуста");
            }

            if (noevents)
            {
                Console.WriteLine("У вас нет предстоящих встреч");
            }
        }

        static void EditEvent(List<Event> list, int index)
        {
            Event newEvent = list.ElementAt(index);
            list.RemoveAt(index);
            DateTime newDate;

            Console.Clear();
            Console.WriteLine("Редактирование встречи [" + newEvent.Description + "]\n");
            Console.WriteLine("[1] Изменить описание встречи");
            Console.WriteLine("[2] Изменить дату и время начала встречи");
            Console.WriteLine("[3] Изменить дату и время окончания встречи");
            Console.WriteLine("[4] Изменить состояние/время напоминания");
            Console.WriteLine("[5] Вернуться в главное меню");

            switch (GetCommand())
            {
                case 1:
                    Console.WriteLine("\nВведите новое название встречи");
                    newEvent.Description = Console.ReadLine();
                    break;

                case 2:
                    Console.Write ("\nТекущая дата начала встречи: " + newEvent.StartDate + "\n");
                    GetEventDateTime("\nВведите новую дату начала встречи", out newDate);
                    newEvent.StartDate = newDate;
                    
                    Console.WriteLine("\nДата начала встречи изменена");
                    break;

                case 3:
                    bool isValidEndDate = false;
                    do
                    {
                        Console.Write("\nТекущая дата окончания встречи: " + newEvent.EndDate + "\n");
                        GetEventDateTime("\nВведите новую дату окончания встречи", out newDate);

                        if (newEvent.StartDate < newDate)
                        {
                            isValidEndDate = true;
                            newEvent.EndDate = newDate;
                            Console.WriteLine("\nДата окончания встречи изменена");
                        }
                        else
                        {
                            Console.WriteLine("\nНовая дата окончания встречи не может быть раньше её начала");
                        }
                    } while (!isValidEndDate);
                    break;
                case 4:
                    if (newEvent.IsNotifierEnabled)
                    {
                        Console.WriteLine("Дата текущего напоминания:" + (newEvent.StartDate - newEvent.NotifierTime) + "\n");
                        Console.WriteLine("[1] Изменить время напоминания");
                        Console.WriteLine("[2] Отключить напоминание");
                        bool validinput = false;
                        do
                        {
                            switch (GetCommand())
                            {
                                case 1:
                                    newEvent.NotifierTime = GetTime();
                                    Console.WriteLine("\nВремя напоминания изменено");
                                    validinput = true;
                                    break;
                                case 2:
                                    newEvent.DeactivateNotifier();
                                    Console.WriteLine("\nНапоминание отключено");
                                    validinput = true;
                                    break;
                                default:
                                    Console.WriteLine("\nКоманда не найдена");
                                    break;
                            }

                        } while (!validinput);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Напоминание не установлено. Установить?\n");
                        Console.WriteLine("[1] Да");
                        Console.WriteLine("[2] Нет");
                        bool validinput = false;
                        do
                        {
                            switch (GetCommand())
                            {
                                case 1:
                                    newEvent.NotifierTime = GetTime();
                                    Console.WriteLine("\nНапоминание установлено");
                                    validinput = true;
                                    break;
                                case 2:
                                    Console.WriteLine("\nИзменения не внесены");
                                    validinput = true;
                                    break;
                                default:
                                    Console.WriteLine("\nКоманда не найдена");
                                    break;
                            }

                        } while (!validinput);
                    }
                    break;
                default:
                    break;
            }
            list.Add(newEvent);           
        }

        static void ShowEventsByDate(DateOnly date, List<Event> list)
        {
            StringBuilder sb = new StringBuilder();
            int eventsCounter = 0;
            int[] indexes = new int[list.Count + 1];

            Console.Clear();
            
            if (events.Count > 0)
            {
                foreach (Event e in list)
                {
                    if (DateOnly.FromDateTime(e.StartDate) == date)
                    {
                        eventsCounter++;
                        Console.WriteLine("------------------------------");
                        Console.Write("Встреча #" + eventsCounter.ToString() + ": ");
                        indexes[eventsCounter] = list.IndexOf(e);
                        ShowEventInfo(e);
                        Console.WriteLine();

                        sb.Append(e.InfoString); 
                    }
                }
            }
            else
            {
                Console.WriteLine("База встреч пуста");
            }

            if (eventsCounter > 0)
            {
                Console.WriteLine("[1] Сохранить список в файл");
                Console.WriteLine("[2] Редактировать/удалить встречу");
                Console.WriteLine("[3] Вернуться в главное меню");

                switch (GetCommand())
                {
                    case 1:
                        SaveEventsToFile(date, sb.ToString());
                        break;
                    case 2:
                        
                        bool validcommand = false;
                        while (!validcommand)
                        {
                            Console.WriteLine("\nВведите номер встречи для редактирования/удаления");
                            int command = GetCommand();

                            if (command > 0 && command <= eventsCounter)
                            { 
                                validcommand = true;

                                Console.Clear();
                                Console.WriteLine("Выбрана встреча [" + list.ElementAt(indexes[command]).Description + "]\n");
                                Console.WriteLine("[1] Редактировать");
                                Console.WriteLine("[2] Удалить");
                                Console.WriteLine("[3] Вернуться в главное меню");

                                switch (GetCommand())
                                {
                                    case 1:

                                        EditEvent(events, indexes[command]);
                                        break;

                                    case 2:
                                        list.RemoveAt(indexes[command]);
                                        Console.WriteLine("Встреча удалена");
                                        break;

                                    case 3:
                                    default:
                                        break;
                                }

                            }
                            else
                            {
                                Console.WriteLine("\nНет встречи с таким номером");
                            }
                        } 
                        break;
                    case 3:
                    default:
                        break;
                }


            }
            else
            {
                Console.WriteLine("У вас не запланировано встреч на этот день");
            }
        }

        static void SaveEventsToFile(DateOnly date, string eventsinfo)
        {
            string datestring = date.ToString();
            string path = "Список встреч на " + datestring + ".txt";

            try
            {
                StreamWriter sw = new StreamWriter(path + ".txt");
                sw.WriteLine("Список встреч на " + datestring + "\n");
                sw.WriteLine(eventsinfo);
                sw.Close();
                Console.WriteLine("\nСписок встреч на " + datestring + " успешно сохранён");
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка записи в файл: "  + e.Message);
            }

        }

        static int GetCommand()
        {
            Console.Write("\nКоманда: ");
            var input = Console.ReadLine();

            if (!int.TryParse(input, out int command))
            {
                command = -2;
            }
            return command;
        }

        static DateTime GetDate(bool isdateonly)
        {
            DateTime date = new DateTime();

            bool datevalid = false;

            if (isdateonly)
            {
                Console.WriteLine("Введите дату в формате ДД.ММ.ГГГГ и нажмите Enter\n");
            }
            else
            {
                Console.WriteLine("в формате ДД.ММ.ГГГГ ЧЧ:ММ и нажмите Enter\n");
            }

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

        static TimeSpan GetTime()
        {
            TimeSpan time = new TimeSpan();
            bool timevalid = false;
            Console.WriteLine("Введите время, за которое нужно напомнить о встрече в формате ЧЧ:ММ:СС");
            do
            {
                if (TimeSpan.TryParse(Console.ReadLine(), out time))
                {
                    timevalid = true;
                }
                else
                {
                    Console.WriteLine("\nНеверный формат ввода. Попробуйте ещё раз\n");
                }
            } while (!timevalid);

            return time;
        }

        static void GetEventDateTime(string Message, out DateTime EventDateTime)
        {
            bool datevalid = false;
            string description;

            do
            {
                Console.WriteLine(Message);

                EventDateTime = GetDate(false);

                if (IsDateInFuture(EventDateTime))
                {
                    if (IsDateAvailable(EventDateTime, events, out description))
                    {
                        datevalid = true;
                    }
                    else
                    {
                        Console.WriteLine("На эту дату уже запланирована встреча [" + description +"]");
                    }

                }
                else
                {
                    Console.WriteLine("Дата планируемой встречи не может быть раньше текущей даты");
                }

            } while (!datevalid);
        }

        static bool IsDateInFuture(DateTime date)
        {
            return date > DateTime.Now;
        }

        static bool IsDateAvailable(DateTime date, List<Event> list, out string description)
        {
            foreach (Event e in list) 
            {
                if (date.IsInRange(e.StartDate,e.EndDate))
                {
                    description = e.Description; 
                    return false;
                }    
                    
            }
            description = "";
            return true;
        }

        static void AddEvent()
        {
            Event newEvent;
            bool isValidEndDate = false;

            Console.Clear();
            Console.WriteLine("Введите краткое описание встречи и нажмите клавишу Enter\n");

            string description = Console.ReadLine();

            if (description.Length == 0)
            {
                description = "Встреча без описания";
            }

            GetEventDateTime("\nВведите дату и время начала встречи", out DateTime startEventDate);

            do
            {
                GetEventDateTime("\nВведите дату и время окончания встречи", out DateTime endEventDate);

                if (startEventDate < endEventDate) 
                {
                    isValidEndDate = true;
                    Console.Clear();
                    Console.WriteLine("Установить напоминание о встрече?\n");
                    Console.WriteLine("[1] Да");
                    Console.WriteLine("[2] Нет");
                    bool validinput = false;
                    do
                    {
                        switch(GetCommand())
                        {
                            case 1:
                                newEvent = new Event(description, startEventDate, endEventDate, GetTime());
                                events.Add(newEvent);
                                validinput = true;
                                break;
                            case 2:
                                newEvent = new Event(description, startEventDate, endEventDate);
                                events.Add(newEvent);
                                validinput = true;
                                break;
                            default:
                                Console.WriteLine("Команда не найдена");
                                break;
                        }

                    } while (!validinput);
                    
                    Console.WriteLine("\nВстреча [" + description + "] запланирована!");
                }
                else
                {
                    Console.WriteLine("\nДата окончания встречи не может быть раньше её начала");
                }
            } while (!isValidEndDate);
        }

        static void Main()
        {
            int menuScreen = 0;
           
            do
            {
                switch (menuScreen)
                {
                    case -1:
                        ShowBackToMainMenu();
                        menuScreen = 0;
                        break;
                    case 0:
                        ShowMainMenu();
                        menuScreen = GetCommand();
                        break;

                    case 1:
                        AddEvent();
                        menuScreen = -1;
                        break;

                    case 2:
                        ShowFutureEvents(events);
                        menuScreen = -1;
                        break;
                    case 3:
                        ShowEventsByDate(DateOnly.FromDateTime(GetDate(true)), events);
                        menuScreen = -1;
                        break;
                    default:
                        Console.WriteLine("\nКоманда не найдена");
                        menuScreen = -1;
                        break;
                }
            } while (menuScreen != 4);
        }
    }
}