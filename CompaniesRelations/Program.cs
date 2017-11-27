using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace CompaniesRelations
{
    class Program
    {
        /// <summary>
        /// Экземпляр класса, представляющего совокупность компаний и их связей
        /// </summary>
        private static CompaniesAndRelationsPool CompaniesRelations;
        static void Main(string[] args)
        {
            CompaniesRelations = new CompaniesAndRelationsPool();
            CompaniesRelations.Initialize();
            int input = -1;
            while (true)
            {
                do
                {   
                    ShowMenu();
                }
                while (!int.TryParse(Console.ReadLine(), out input));
                Console.Clear();
                try
                {
                    switch (input)
                    {
                        case 1:
                            ShowCompaniesPoolTable();
                            break;
                        case 2:
                            ShowCompaniesTable();
                            break;
                        case 3:
                            ShowRelationsByCompanyTemp();
                            break;
                        case 4:
                            AddNewCompany();
                            break;
                        case 5:
                            AddNewRelation();
                            break;
                        case 6:
                            DeleteCompany();
                            break;
                        case 7:
                            DeleteRelation();
                            break;
                        case 8:
                            SaveInfoToFile();
                            break;
                        case 9:
                            LoadInfoFromFile();
                            break;
                        case 0:
                            Console.WriteLine("Работа программы завершена.");
                            return;
                        default:
                            Console.WriteLine("Команда не распознана. Пожалуйста, введите номер команды, указанный в меню.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Очистка консоли и отображение меню
        /// </summary>
        static void ShowMenu()
        {
            Console.WriteLine("Данная программа предназначена для отображения связей между компаниями, а также их редактирования.");
            Console.WriteLine("Ниже перечислены возможные действия с программой (нажмите Enter после ввода номера действия):");
            Console.WriteLine("1 - показать связи.\n2 - показать компании.\n3 - показать связи для компании (поиск).\n4 - добавить новую компанию.\n5 - добавить новую связь.\n6 - удалить компанию.\n7 - удалить связь.\n8 - сохранить связи и компании в файл.\n9 - загрузить связи и компании в файл.\n0 - выход из программы.");
        }

        /// <summary>
        /// Удалить компанию
        /// </summary>
        private static void DeleteCompany()
        {
            Console.WriteLine("Нажмите Enter, чтобы отобразить список компаний.");
            Console.ReadLine();
            ShowCompaniesTable();
            string companyName = string.Empty;
            bool found = false;
            while (true)
            {
                Console.WriteLine("Введите название компании для удаления:");
                companyName = Console.ReadLine();
                found = false;
                foreach (var comp in CompaniesRelations.Companies)
                {
                    if (comp.Name == companyName)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    var company = CompaniesRelations.Companies.Where(x => x.Name == companyName).FirstOrDefault();
                    CompaniesRelations.Companies.Remove(company);
                    var companyRelations = CompaniesRelations.Relations.Where(x => x.Item1.Name == companyName || x.Item2.Name == companyName).Select(x => x).ToList();
                    for (int i = 0; i < companyRelations.Count; i++)
                    {
                        CompaniesRelations.Relations.Remove(companyRelations[i]);
                    }
                    Console.WriteLine("Компания {0} и все ее связи удалены.\n", company.Name);
                    break;
                }
            }
        }

        /// <summary>
        /// Удалить связь компаний.
        /// </summary>
        private static void DeleteRelation()
        {
            Console.WriteLine("Нажмите Enter, чтобы отобразить список компаний.");
            Console.ReadLine();
            ShowCompaniesTable();
            string companyName = string.Empty;
            bool found = false;
            Company company = null;
            while (true)
            {
                Console.WriteLine("Введите название компании, для которой вы хотите удалить связь:");
                companyName = Console.ReadLine();
                found = false;
                foreach (var comp in CompaniesRelations.Companies)
                {
                    if (comp.Name == companyName)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    company = CompaniesRelations.Companies.Where(x => x.Name == companyName).FirstOrDefault();
                    break;
                }
            }
            Console.WriteLine("Список связей для выбранной компании.");
            ShowRelationsByCompany(company);

            int number = -1;
            while (true)
            {
                Console.WriteLine("Укажите номер связи для удаления.");
                if (int.TryParse(Console.ReadLine(), out number))
                    break;
            }
            var relation = CompaniesRelations.Relations[number];
            CompaniesRelations.Relations.RemoveAt(number);
            Console.WriteLine("Связь {0} компаний {1} и {2} удалена.\n", relation.Item3, relation.Item1.Name, relation.Item2.Name);
        }

        /// <summary>
        /// Вспомогательный метод для отображения связей выбранной компании
        /// </summary>
        /// <param name="companies"></param>
        private static void ShowRelationsByCompanyTemp()
        {
            string input = string.Empty;
            while (true)
            {
                Console.WriteLine("Отобразить список компаний, среди которых можно осуществить поиск? Y/N");
                input = Console.ReadLine().ToLower();
                if (input == "y" || input == "n")
                    break;
            }
            if (input == "y")
            {
                var list = CompaniesRelations.Relations.Select(x => x.Item1).Distinct().ToList();
                Console.WriteLine("Список всех компаний, среди которых можно осуществить поиск:");
                foreach (var c in list)
                    Console.Write(c.ToString() + "\t");
            }
            string companyName = string.Empty;
            bool found = false;
            while (true)
            {
                Console.WriteLine("Введите название компании, для которой следует осуществить вывод связей:");
                companyName = Console.ReadLine();
                found = false;
                foreach (var comp in CompaniesRelations.Companies)
                {
                    if (comp.Name == companyName)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Компания с таким названием не найдена в системе. Выход из меню поиска.");
                    return;
                }
                break;
            }
            var company = CompaniesRelations.Companies.Where(x => x.Name == companyName).FirstOrDefault();
            ShowRelationsByCompany(company);
        }

        /// <summary>
        /// Добавить новую компанию в список компаний
        /// </summary>
        static void AddNewCompany()
        {
            string input = string.Empty;
            while (true)
            {
                Console.WriteLine("Введите название компании, которую вы хотите добавить:");
                input = Console.ReadLine();
                foreach (var c in CompaniesRelations.Companies)
                {
                    if (c.Name == input)
                    {
                        Console.WriteLine("Компания с таким названием уже содержится в списке.");
                        continue;
                    }
                }
                break;
            }
            CompaniesRelations.Companies.Add(new Company(input));
            Console.WriteLine("Компания добавлена.");
            Console.WriteLine();
        }

        /// <summary>
        /// Добавить новую связь
        /// </summary>
        static void AddNewRelation()
        {
            var nameFirst = string.Empty;
            var nameSecond = string.Empty;
            var relation = string.Empty;
            var companyFirst = default(Company);
            var companySecond = default(Company);

            while (true)
            {
                Console.WriteLine("Введите название компании, для которой вы хотите добавить связь:");
                nameFirst = Console.ReadLine();
                bool contains = false;
                foreach (var c in CompaniesRelations.Companies)
                {
                    if (c.Name == nameFirst)
                    {
                        contains = true;
                        companyFirst = c;
                        break;
                    }
                }
                if (!contains)
                {
                    Console.WriteLine("Компании с данным именем не содержится в списке компаний.");
                    Console.WriteLine();
                    continue;
                }
                break;
            }

            while(true)
            { 
                Console.WriteLine("Введите название компании, с которой вы хотите связать первую:");
                nameSecond = Console.ReadLine();
                bool contains = false;
                foreach (var c in CompaniesRelations.Companies)
                {
                    if (c.Name == nameSecond)
                    {
                        contains = true;
                        companySecond = c;
                        break;
                    }
                }
                if (!contains)
                {
                    Console.WriteLine("Компании с данным именем не содержится в списке компаний.");
                    Console.WriteLine();
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine("Опишите связь первой компании со второй:");
                relation = Console.ReadLine();
                foreach (var rel in CompaniesRelations.Relations)
                {
                    // Если есть полное совпадение по названиям компаний и их связи
                    if (rel.Item1 == companyFirst && rel.Item2 == companySecond && rel.Item3 == relation)
                    {
                        Console.WriteLine("Такая связь между выбранными компаниями уже существует.");
                        Console.WriteLine();
                        continue;
                    }
                }
                break;
            }
            CompaniesRelations.Relations.Add(Tuple.Create(companyFirst, companySecond, relation));
            Console.WriteLine("Связь добавлена.");
            Console.WriteLine();
        }

        /// <summary>
        /// Сохранить в файл информацию о передаваемом списке компаний и их связей
        /// </summary>
        /// <param name="fileName">Имя файла, в который происходит сохранение данных</param>
        static void SaveInfoToFile()
        {
            string fileName = string.Empty;
            while (true)
            {
                Console.WriteLine("Введите имя файла БЕЗ расширения, в который вы хотите осуществить сохранение данных:");
                fileName = Console.ReadLine();
                if (fileName.Intersect(Path.GetInvalidFileNameChars()).Any())
                {
                    Console.WriteLine("Введенное имя файла содержит недопустимые символы.");
                    continue;
                }
                break;
            }
            fileName += ".bin";
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                bf.Serialize(fs, CompaniesRelations);
            }
            Console.WriteLine("Файл сохранен.");
            Console.WriteLine();
        }

        /// <summary>
        /// Загрузить из файла информацию о списке компаний и их связях
        /// </summary>
        /// <param name="fileName">Имя файла, из которого читаются данные</param>
        /// <returns>Список компаний и их связей, null в случае ошибки (вообще, будет выброшено исключение)</returns>
        static void LoadInfoFromFile()
        {
            string fileName = string.Empty;
            while (true)
            {
                Console.WriteLine("Введите имя файла БЕЗ расширения, из которого вы хотите осуществить загрузку данных:");
                fileName = Console.ReadLine();
                if (fileName.Intersect(Path.GetInvalidFileNameChars()).Any())
                {
                    Console.WriteLine("Введенное имя файла содержит недопустимые символы.");
                    continue;
                }
                break;
            }
            fileName += ".bin";
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл с именем " + fileName + " не существует/не найден.");
            BinaryFormatter bf = new BinaryFormatter();
            CompaniesAndRelationsPool companies = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                object readData = bf.Deserialize(fs);
                if (readData is CompaniesAndRelationsPool)
                    companies = readData as CompaniesAndRelationsPool;
                else
                    throw new FormatException("В указанном файле " + fileName + " не было объекта, пригодного для десериализации в тип CompaniesPool.");
            }
            CompaniesRelations = companies;
            Console.WriteLine("Данные успешно загружены из файла.");
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод списка компаний в виде таблицы
        /// </summary>
        static void ShowCompaniesTable()
        {
            Console.WriteLine("Список всех компаний:");
            string delimeter = string.Empty;
            for (int i = 0; i < 30; i++)
                delimeter += "_";
            Console.WriteLine("{0,30}", "Компания");
            Console.WriteLine(delimeter);
            foreach (var c in CompaniesRelations.Companies)
            {
                Console.WriteLine("{0,30}", c.ToString());
            }
            Console.WriteLine(delimeter);
            Console.WriteLine();
        }

        /// <summary>
        /// Вывод таблицы компаний и их связей
        /// </summary>
        static void ShowCompaniesPoolTable()
        {
            Console.WriteLine("Список всех компаний и их связей:");
            string delimeter = string.Empty;
#warning Вынести в константы длину столбцов и их суммарную длину
            for (int i = 0; i < 130; i++)
                delimeter += "_";
            Console.WriteLine("{0,3}{1,30}{2,30}{3,50}", "№", "Компания 1", "Компания 2", "Отношение");
            Console.WriteLine(delimeter);
            foreach (var c in CompaniesRelations.Relations)
            {
                Console.WriteLine("{0,3}{1,30}{2,30}{3,50}", c.Item4.ToString(), c.Item1.ToString(), c.Item2.ToString(), c.Item3);
            }
            Console.WriteLine(delimeter);
            Console.WriteLine();
        }

        /// <summary>
        /// Показать связи текущей компании
        /// </summary>
        /// <param name="company">Компания, для которой отображаются связи</param>
        static void ShowRelationsByCompany(Company company)
        {
            var list = CompaniesRelations.Relations.Where(x => x.Item1.Name == company.Name).Select(x => x).Distinct();
            Console.WriteLine("Список всех связей для компании: " + company.ToString());
            string delimeter = string.Empty;
            for (int i = 0; i < 130; i++)
                delimeter += "_";
            Console.WriteLine("{0,3}{1,30}{2,30}{3,50}", "№","Компания 1", "Компания 2", "Отношение");
            Console.WriteLine(delimeter);
            foreach (var c in list)
            {
                Console.WriteLine("{0,3}{1,30}{2,30}{3,50}", c.Item4.ToString(), c.Item1.ToString(), c.Item2.ToString(), c.Item3);
            }
            Console.WriteLine(delimeter);
            Console.WriteLine();
        }
    }
#warning Вынести классы в отдельные файлы

    /// <summary>
    /// Пул список компаний и их связей
    /// </summary>
    [Serializable]
    class CompaniesAndRelationsPool 
    {
        /// <summary>
        /// Список всех компаний
        /// </summary>
        public List<Company> Companies { get; set; }

        /// <summary>
        /// Список связей имеющихся компаний
        /// </summary>
        public Relations Relations { get; set; }

        /// <summary>
        /// Начальная инициализация членов класса
        /// </summary>
        public void Initialize()
        {
            Companies = new List<Company>();
            Relations = new Relations();
        }
    }

    /// <summary>
    /// Список связей компаний
    /// </summary>
    [Serializable]
    class Relations : List<Tuple<Company, Company, string, int>>
    {
        /// <summary>
        /// Счетчик количества связей
        /// </summary>
        public static int Counter { get; private set; }

        static Relations()
        {
            Counter = 0;
        }

        /// <summary>
        /// Сокрытие метода добавления новой связи с лишним параметром - номером. Сделано в целях сохранения имеющейся реализации остального кода.
        /// </summary>
        /// <param name="tuple">Элемент списка: исходная компания, компания, с которой она связана, описание связи, номер связи</param>
        public new void Add(Tuple<Company, Company, string, int> tuple)
        {
            var tupleNew = Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3);
            Add(tupleNew);
        }

        /// <summary>
        /// Перегрузка метода добавления новой связи для компаний
        /// </summary>
        /// <param name="tuple">Элемент списка: исходная компания, компания, с которой она связана, описание связи</param>
        public void Add(Tuple<Company, Company, string> tuple)
        {
            if (tuple == null)
                throw new ArgumentNullException(nameof(tuple));
            if (tuple.Item1.Name == tuple.Item2.Name)
                throw new ArgumentException("Нельзя создать связь компании с самой собой", nameof(tuple));
            if (string.IsNullOrEmpty(tuple.Item3))
                throw new ArgumentNullException(nameof(tuple.Item3));

            foreach (var rel in this)
            {
                if (tuple.Item1.Name == rel.Item1.Name &&
                    tuple.Item2.Name == rel.Item2.Name &&
                    tuple.Item3 == rel.Item3)
                {
                    throw new ArgumentException("Данная связь указанных компаний уже зарегистрирована", nameof(tuple));
                }
            }
            Counter++;
            var tupleNew = Tuple.Create(tuple.Item1, tuple.Item2, tuple.Item3, Counter);
            base.Add(tupleNew);
        }
    }

    /// <summary>
    /// Компания
    /// </summary>
    [Serializable]
    class Company
    {
        private string _name;
        /// <summary>
        /// Название компании
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException(nameof(value));
                _name = value;
            }
        }

        /// <summary>
        /// Получить строковое представление компании - ее название
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="name"></param>
        public Company(string name)
        {
            _name = name;
        }

        public Company()
        {
        }
    }
}