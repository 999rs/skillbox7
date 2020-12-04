using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using static System.Console;
using System.Runtime.Serialization.Formatters.Binary;

namespace NoteBook
{
    /// <summary>
    /// Инкапсулирует логику меню и обращение к методам Книги
    /// </summary>
    public static class Menu
    {
        private static string input = "";
        
        /// <summary>
        /// рисует главное меню
        /// </summary>
        /// <param name="book">Книга к которой будут обращения из меню</param>
        public static void drawMainMenu(NoteBook book)
        {
            do 
            {
                #region draw menu
                Console.Clear();
                WriteLine("MENU:");
                WriteLine("[add]:   Add new Record +");
                WriteLine("[edit]:  Edit Mode and Delete +");
                WriteLine("[sort]:  Sort Mode +");  // todo
                //WriteLine("[3]:  Delete Record +");
                WriteLine("[p]:     Print Book +");
                WriteLine("[e]:     Export book to file +");
                WriteLine("[c]:     Clear book (delete all records) +");
                WriteLine("[i]:     Import from book file (add ALL records with new IDs) +");
                WriteLine("[idate]: Import from book file (add new records in specific Range) "); // todo
                WriteLine("[q]:     Quit program +");
                input = ReadLine();

                Console.Clear();

                #endregion

                // обработка ввода и вызов методов
                switch (input)
                {
                    case "add":
                        {
                            addNewRecord(book);
                            break;
                        }
                    case "p":
                        {
                            printBook(book);
                            break;
                        }
                    case "q":
                        {
                            
                            break;
                        }
                    case "e":
                        {
                            saveBookToFile(book);
                            break;
                        }
                    case "c":
                        {
                            clearNoteBook(book);
                            break;
                        }
                    case "i":
                        {
                            importNoteBook(book);
                            break;
                        }
                    case "edit":
                        {
                            navigate(book);
                            break;
                        }
                    case "sort":
                        {
                            sortMenu(book);
                            break;
                        }
                    case "idate":
                        {
                            importNoteBookInDateRange(book);
                            break;
                        }

                    default:
                        {
                            
                            WriteLine("Недопустивый ввод! Нажмите любую клавишу для продолжения.");
                            ReadKey();
                            
                            break;
                        }



                }


            }
            
            while (input != "q");
        }
        

        /// <summary>
        /// обработка меню для новой записи
        /// </summary>
        /// <param name="book">книга</param>
        public static void addNewRecord(NoteBook book)
        {
            Write("\r\nTitle: ");
            var titleInput = ReadLine();
            
            Write("\r\nText: ");
            var textInput = ReadLine();

            book.addNewRecord(titleInput, textInput);           

            WriteLine("\r\n\r\nЗапись добавлена!\r\nНажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

        /// <summary>
        /// обработка меню для вывода книги
        /// </summary>
        /// <param name="book">книга</param>
        public static void printBook(NoteBook book)
        {
            Console.WriteLine("Содержимое книги:");

            book.printOut();

            //WriteLine($"{"Guid",50}{"Modification date",25}{"Creation Date",25}");


            WriteLine("\r\nНажмите любую клавишу для возврата в меню.");
            ReadKey();

        }

        /// <summary>
        /// обработка меню для экспорта книги в файл
        /// </summary>
        /// <param name="book">книга</param>
        public static void saveBookToFile(NoteBook book)
        {
            string fileName = "";
            bool isFileError = false;

            do
            {
                isFileError = false;
                WriteLine("Введите имя файла: ");
                fileName = ReadLine();
                
                // создаем объект BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    var fs = File.Create(fileName);
                    formatter.Serialize(fs, book);
                    fs.Flush();
                    fs.Close();
                    fs.Dispose();

                }
                catch (Exception ex)
                {
                    isFileError = true;
                    WriteLine($"Ошибка: {ex.Message} \r\n ");
                    WriteLine("Нажмите любую клавишу для повторной попытки");
                    ReadKey();
                }

            } 
            while (isFileError == true);

            WriteLine("\r\n Файл сохранен. \r\n Нажмите любую клавишу для возврата в меню.");
            ReadKey();

        }


        /// <summary>
        /// обработка меню для очистки книга
        /// </summary>
        /// <param name="book">книга</param>
        public static void clearNoteBook(NoteBook book)
        {
            book.clear();
            WriteLine("\r\n Книга очищена. \r\n Нажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

        /// <summary>
        /// обработка меню для импорта отсутствующих записей
        /// </summary>
        /// <param name="book">книга</param>
        public static void importNoteBook(NoteBook book)
        {
            string fileName = "";
            bool isFileError = false;
            NoteBook importedBook;

            do
            {
                isFileError = false;
                WriteLine("Введите имя файла: ");
                fileName = ReadLine();
                int importedCount = 0;
                // создаем объект BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    var fs = File.OpenRead(fileName);
                    importedBook = (NoteBook)formatter.Deserialize(fs);              
                    fs.Close();
                    fs.Dispose();

                }
                catch (Exception ex)
                {
                    isFileError = true;
                    WriteLine($"Ошибка: {ex.Message} \r\n ");
                    WriteLine("Нажмите любую клавишу для повторной попытки");
                    ReadKey();
                    continue;
                }
                
                foreach (var importedRecord in importedBook.Records)
                {
                    
                    if (!book.Records.Exists(x => x.Id == importedRecord.Id)) 
                    {
                        book.Records.Add(importedRecord);
                        importedCount++;

                    }
                    
                }
                WriteLine($"Количество Импортированных записей {importedCount}");

            }
            while (isFileError == true);

            WriteLine("\r\n Импорт завершен. \r\n Нажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

        /// <summary>
        /// обработка меню для импорта отсутствующих записей с датой создания в пределах диапазона
        /// </summary>
        /// <param name="book">кника </param>
        public static void importNoteBookInDateRange(NoteBook book)
        {
            string fileName = "";
            bool isFileError = false;
            NoteBook importedBook;


            string sFrom = "";
            string sThru = "";
            DateTime dFrom;
            DateTime dThru;


            do
            {
                isFileError = false;
                WriteLine("Введите имя файла: ");
                fileName = ReadLine();
                int importedCount = 0;
                int skipCount = 0;
                int inRangeCount = 0;
                
                // создаем объект BinaryFormatter
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    var fs = File.OpenRead(fileName);
                    importedBook = (NoteBook)formatter.Deserialize(fs);
                    //fs.Flush();
                    fs.Close();
                    fs.Dispose();

                }
                catch (Exception ex)
                {
                    isFileError = true;
                    WriteLine($"Ошибка: {ex.Message} \r\n ");
                    WriteLine("Нажмите любую клавишу для повторной попытки");
                    ReadKey();
                    continue;
                }

                do
                {
                    WriteLine($"Введите НАЧАЛЬНУЮ дату диапозона импорта, например {DateTime.Now}");
                    sFrom = ReadLine();
                } while (!DateTime.TryParse(sFrom, out dFrom));

                do
                {
                    WriteLine($"Введите КОНЕЧНУЮ дату диапозона импорта, например {DateTime.Now}");
                    sFrom = ReadLine();
                } while (!DateTime.TryParse(sFrom, out dThru));

                


                foreach (var importedRecord in importedBook.Records)
                {
                    bool isExist = false;
                    bool isInRange = false;


                    if (book.Records.Exists(x => x.Id == importedRecord.Id))
                    {
                    
                        isExist = true;
                        skipCount++;
                    }
                    if ( importedRecord.Cdate >= dFrom && importedRecord.Cdate <= dThru)
                    {
                        isInRange = true;
                        inRangeCount++;
                    }

                    if (isInRange && !isExist)
                    {
                        book.Records.Add(importedRecord);
                        importedCount++;
                    }


                }
                WriteLine($"Количество записей в диапазоне {inRangeCount}, пропущено записей с существующим Id {skipCount}");
                WriteLine($"Импортировано записей {importedCount}.");

            }
            while (isFileError == true);

            WriteLine("\r\n Нажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

        /// <summary>
        /// вспомогательный - для загрузки конкретного файла при старте
        /// </summary>
        /// <param name="book"></param>
        /// <param name="filename"></param>
        public static void importNoteBook(NoteBook book, string filename)
        {

            NoteBook importedBook;

            WriteLine("Введите имя файла: ");

            int importedCount = 0;
            int skipCount = 0;
            // создаем объект BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                var fs = File.OpenRead(filename);
                importedBook = (NoteBook)formatter.Deserialize(fs);
                //fs.Flush();
                fs.Close();
                fs.Dispose();

            }
            catch (Exception ex)
            {

                WriteLine($"Ошибка: {ex.Message} \r\n ");
                WriteLine("Нажмите любую клавишу для выхода");
                ReadKey();
                return;

            }

            foreach (var importedRecord in importedBook.Records)
            {

                if (!book.Records.Exists(x => x.Id == importedRecord.Id))
                {
                    book.Records.Add(importedRecord);
                    importedCount++;

                }
                else
                {
                    skipCount++;
                }

            }

            WriteLine($"Количество Импортированных записей {importedCount}. Пропущено записей {skipCount}");
            WriteLine("\r\n Импорт завершен. \r\n Нажмите любую клавишу для продолжения");

            ReadKey();
        }


        /// <summary>
        /// обработка меню для удаления записи
        /// </summary>
        /// <param name="book">книга</param>
        /// <param name="idx">индекс удаляемой записи</param>
        public static void deleteRecord(NoteBook book, int idx)
        {
            book.Records.RemoveAt(idx);

            WriteLine($"\r\n\r\nRecord №[{idx + 1}] deleted. Press any key to continue. ");
            ReadKey();

        }

        /// <summary>
        /// обработка меню для редактирования записи
        /// </summary>
        /// <param name="book">книга</param>
        /// <param name="idx">индекс редактируемой записи </param>
        public static void editRecord(NoteBook book, int idx)
        {
           
            bool exitFlag = false;
            do
            {
                Console.Clear();
                WriteLine($"{"*============== Instructions =================\r\n",-80}");
                WriteLine($"{" Edit Title:",-20} {"[1]",20}");
                WriteLine($"{" Edit Text:",-20} {" [2]",20}");                
                WriteLine($"{" BACK:",-20} {"[EscKey]",20}");
                WriteLine($"{"\r\n*=============================================\r\n",-80}");

                WriteLine("Выбранная для редактирования запись:\r\n");
                book.Records[idx].printRecord();
                
                var key = ReadKey();
                switch (key.Key)
                {
                    case (ConsoleKey.Escape):
                        {
                            exitFlag = true;
                            //WriteLine("exit edit record");
                            break;
                        }
                    case (ConsoleKey.D1):
                        {                           
                            WriteLine("\r\nEnter new title:\r\n");
                            var rec = book.Records[idx];
                            rec.Title = ReadLine();
                            book.Records[idx] = rec;
                            break;
                        }
                    case (ConsoleKey.D2):
                        {

                            WriteLine("\r\nEnter new text:\r\n");
                            var rec = book.Records[idx];
                            rec.Text = ReadLine();
                            book.Records[idx] = rec;
                            break;
                        }

                    default:
                        { break; }

                }
            }
            while (!exitFlag);
        
        }

        /// <summary>
        /// обработка меню для навигации по записям
        /// </summary>
        /// <param name="book">книга</param>
        public static void navigate(NoteBook book) 
        {
           
            int selectedIndex = 0;
            int itemsCount = book.Records.Count;
            if (itemsCount == 0)
            {

                WriteLine("Book is empty, nothing to edit. \r\n Press any key to exit edit mode. ");
                ReadKey();
            }
            else 
            {
                do
                {
                    Console.Clear();

                    WriteLine($"{"*============== Instructions =================\r\n",-80}");
                    WriteLine($"{" Navigate:",-20} {"[UpKey]/[DownKey]",20}");
                    WriteLine($"{" Edit:",-20} {"[EnterKey]",20}");
                    WriteLine($"{" Delete:",-20} {"[DeleteKey]",20}");
                    WriteLine($"{" BACK:",-20} {"[EscKey]",20}");
                    WriteLine($"{"\r\n*=============================================\r\n",-80}");


                    // подсветка выбранной строки
                    WriteLine($"{"Дата создания",20}  {"Заголовок"}");
                    foreach (var rec in book.Records)
                    {
                        if (book.Records.IndexOf(rec) == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                        }
                        else {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        WriteLine($"{rec.Cdate,20}  {rec.Title}");
                            

                    }
                    Console.BackgroundColor = ConsoleColor.Black;

                    var key = ReadKey();

                    //обработка выбранного действия
                    switch (key.Key ) 
                    {
                        case  (ConsoleKey.UpArrow) :
                            {
                                if (  selectedIndex > 0)
                                    { selectedIndex--; }
                                break;
                            }
                        case (ConsoleKey.DownArrow) :
                            {
                                if (selectedIndex < itemsCount - 1)
                                    { selectedIndex++; }
                                break;
                            }

                        case (ConsoleKey.Enter) :
                            {
                                //WriteLine(selectedIndex);
                                editRecord(book, selectedIndex);
                                //ReadKey();
                                break;
                            }
                        case (ConsoleKey.Delete):
                            {
                                if (itemsCount > 0)
                                {
                                    deleteRecord(book, selectedIndex);
                                    selectedIndex--;
                                    itemsCount--;
                                }
                                break;
                            }
                        case (ConsoleKey.Escape):
                            {
                                // выход по эскейпу
                                return;
                            }

                        default:
                            {
                                break;
                            }
                    }
                        

                }

                while (true);
                    
            
            
            }                

        
        }


        /// <summary>
        ///  обработка меню для сортировки записей книги
        /// </summary>
        /// <param name="book">книга</param>
        public static void sortMenu(NoteBook book)
        {
            bool exitFlag = false;
            do
            {
                Console.Clear();
                WriteLine($"{"*============== Instructions =================\r\n",-80}");
                WriteLine($"{" Sort by Title:",-20} {"[1]+",20}");
                WriteLine($"{" Sort by Text:",-20} {" [2]+",20}");
                WriteLine($"{" Sort by Cdate:",-20} {" [3]+",20}");
                WriteLine($"{" Sort by Mdate:",-20} {" [4]+",20}");
                WriteLine($"{" BACK:",-20} {"[EscKey]",20}");
                WriteLine($"{"\r\n*=============================================\r\n",-80}");

                

                var key = ReadKey();
                switch (key.Key)
                {
                    case (ConsoleKey.Escape):
                        {
                            // флаг выхода из цикла
                            exitFlag = true;
                            
                            break;
                        }
                    case (ConsoleKey.D1):
                        {

                            book.sortByTitle();
                            WriteLine("\r\nSorted by Title. Press aby key.\r\n");
                            ReadKey();
                            
                            break;
                        }
                    case (ConsoleKey.D2):
                        {

                            book.sortByText();
                            WriteLine("\r\nSorted by Text. Press aby key.\r\n");
                            ReadKey();

                            break;
                        }
                    case (ConsoleKey.D3):
                        {

                            book.sortByCdate();
                            WriteLine("\r\nSorted by Cdate. Press aby key.\r\n");
                            ReadKey();

                            break;
                        }
                    case (ConsoleKey.D4):
                        {

                            book.sortByMdate();
                            WriteLine("\r\nSorted by Mdate. Press aby key.\r\n");
                            ReadKey();

                            break;
                        }

                    default:
                        { break; }

                }
            }
            while (!exitFlag);
        }


    }
}
