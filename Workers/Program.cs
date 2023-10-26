using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Workers
{
    class Task
    {
        public string Name { get; set; }
        public Person Assigner { get; set; }
        public Person Executor { get; set; }
        public TaskType Type { get; set; }

        public Task(string name, Person assigner, Person executor, TaskType type)
        {
            Name = name;
            Assigner = assigner;
            Executor = executor;
            Type = type;
        }
    }

    class Person
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public Person Boss { get; set; }

        public Person(string name, string department, Person boss)
        {
            Name = name;
            Department = department;
            Boss = boss;
        }

        public bool CanDoTask(Task task)
        {
            switch (task.Type)
            {
                case TaskType.Development:
                    return Department == "Разработка";
                case TaskType.System:
                    return Department == "Системы";
                case TaskType.Boss:
                    return Department == "Руководство";
            }

            return false;
        }
    }
    enum TaskType 
    {
        Development,
        System,
        Boss

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            // Создаем иерархию сотрудников

            // Генеральный директор
            Person semen = new Person("Семен", "Руководство", null);

            // Финансовый директор
            Person rashid = new Person("Рашид", "Руководство", semen);

            // Директор по автоматизации
            Person ilham = new Person("О Ильхам", "Руководство", semen);

            // Бухгалтерия
            Person lukas = new Person("Лукас", "Бухгалтерия", rashid);

            // Отдел информационных технологий
            Person orkadiy = new Person("Оркадий", "Руководство", ilham);
            Person volodya = new Person("Володя", "Руководство", ilham);

            // Системщики
            Person ilshat = new Person("Ильшат", "Системы", orkadiy);
            Person ivanych = new Person("Иванич", "Системы", ilshat);
            Person ilya = new Person("Илья", "Системы", ilshat);
            Person vity = new Person("Витя", "Системы", ilshat);
            Person zhenya = new Person("Женя", "Системы", ilshat);

            // Разработчики
            Person sergey = new Person("Сергей", "Разработка", ilham);
            Person lyasan = new Person("Ляйсан", "Разработка", sergey);
            Person marat = new Person("Марат", "Разработка", lyasan);
            Person dina = new Person("Дина", "Разработка", lyasan);
            Person ildar = new Person("Ильдар", "Разработка", lyasan);
            Person anton = new Person("Антон", "Разработка", lyasan);

            // Создаем список задач

            List<Task> tasks = new List<Task>();
            tasks.Add(new Task("Написать код", semen, marat, TaskType.Development));
            tasks.Add(new Task("Наладить сеть", orkadiy, ilya, TaskType.System));
            tasks.Add(new Task("Подготовить отчет", rashid, lukas, TaskType.Boss));

            // Распределяем задачи по сотрудникам

            foreach (Task task in tasks)
            {
                // Проверяем, может ли сотрудник выполнить задачу

                if (task.Executor.CanDoTask(task))
                {
                    // Даем задачу сотруднику

                    Console.WriteLine("От " + task.Assigner.Name + " дается задача " + task.Name + " сотруднику " + task.Executor.Name);
                }
                else
                {
                    // Отклоняем задачу

                    Console.WriteLine("От " + task.Assigner.Name + " дается задача " + task.Name + " сотруднику " + task.Executor.Name + ", но он ее не берет");
                }
            }
            Console.ReadKey();
        }
    }
}

