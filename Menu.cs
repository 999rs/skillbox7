﻿using System;
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
    public static class Menu
    {
        private static string input = "";
       // private static List<string> AcceptableInputs = new List<string>{"1", "2", "3", "s", "c", "o" , "e" , "p"};

        public static void drawMainMenu(NoteBook book)
        {
            do 
            {
                Console.Clear();
                WriteLine("MENU:");
                WriteLine("[1]:     Add new Record +");
                WriteLine("[2]:     Edit Mode +");
                WriteLine("[sort]:  Sort Mode");  // todo
                //WriteLine("[3]:  Delete Record +");
                WriteLine("[p]:     Print Book +");
                WriteLine("[e]:     Export book to file +");
                WriteLine("[c]:     Clear book (delete all records) +");
                WriteLine("[i]:     Import book file (add ALL new records) +");
                WriteLine("[idate]: Import from book file (add new records in specific Range) "); // todo
                WriteLine("[q]:     Quit program +");
                input = ReadLine();

                Console.Clear();


                switch (input)
                {
                    case "1":
                        {
                            addNewRecord(book.Records);
                            break;
                        }
                    case "p":
                        {
                            printBook(book.Records);
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
                    case "2":
                        {
                            navigate(book);
                            break;
                        }
                    case "sort":
                        {
                            sortMenu(book);
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

            //while (!AcceptableInputs.Contains(input));
            while (input != "q");
        }
        
        public static void addNewRecord(List<NoteBookRecord> records)
        {
            Write("\r\nTitle: ");
            var titleInput = ReadLine();
            
            Write("\r\nText: ");
            var textInput = ReadLine();

            NoteBookRecord newRecord = new NoteBookRecord(titleInput, textInput);
            records.Add(newRecord);

            WriteLine("\r\n\r\nЗапись добавлена!\r\nНажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

        public static void printBook(List<NoteBookRecord> records)
        {
            Console.WriteLine("Содержимое книги:");

            //WriteLine($"{"Guid",50}{"Modification date",25}{"Creation Date",25}");
            foreach (NoteBookRecord rec in records)
            {
                //WriteLine("***************************************************************************");
                //WriteLine($"{"Guid:",-20}{rec.Id,50}\r\n{"Modification date:",-20}{rec.Mdate,50}\r\n{"Creation Date",-20}{rec.Cdate,50}");
                //WriteLine($"Title: {rec.Title}");
                //WriteLine($"Text: {rec.Text}");            
                rec.printRecord();
            }

            WriteLine("\r\nНажмите любую клавишу для возврата в меню.");
            ReadKey();

        }

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

        public static void clearNoteBook(NoteBook book)
        {
            book.Records.Clear();
            WriteLine("\r\n Книга очищена. \r\n Нажмите любую клавишу для возврата в меню.");
            ReadKey();
        }

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
        /// вспомогательный - для загрузки конкретного файла при старте
        /// </summary>
        /// <param name="book"></param>
        /// <param name="filename"></param>
        public static void importNoteBook(NoteBook book, string filename)
        {
  
  
            NoteBook importedBook;


       
                WriteLine("Введите имя файла: ");
          
                int importedCount = 0;
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

                }
                WriteLine($"Количество Импортированных записей {importedCount}");



            WriteLine("\r\n Импорт завершен. \r\n Нажмите любую клавишу для продолжения");
            ReadKey();
        }

        public static void deleteRecord(NoteBook book, int idx)
        {
            book.Records.RemoveAt(idx);

            WriteLine($"\r\n\r\nRecord №[{idx + 1}] deleted. Press any key to continue. ");
            ReadKey();

        }

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


                    //WriteLine($"{"Navigate:",60} {"[UpKey]/[DownKey]",20}");
                    //WriteLine($"{"Delete:",60} {" [DeleteKey]",20}");
                    //WriteLine($"{"Edit:",60} {"[EnterKey]",20}");
                    //WriteLine($"{"Back to main menu:",60} {"[EscKey]",20}");

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
                                return;
                            }

                        default:
                            {
                                break;
                            }
                    }
                        
                    //if (key.Key == ConsoleKey.UpArrow && selectedIndex > 0) 
                    //{
                    //    selectedIndex--;
                    //}
                    //if (key.Key == ConsoleKey.DownArrow && selectedIndex < itemsCount -1)
                    //{
                    //    selectedIndex++;
                    //}
                    //WriteLine($"key.KeyChar.ToString() =  {key.KeyChar.ToString()}");
                   

                   // ReadKey();
                }

                while (true);
                    
            
            
            }                

        
        }

        public static void sortMenu(NoteBook book)
        {
            bool exitFlag = false;
            do
            {
                Console.Clear();
                WriteLine($"{"*============== Instructions =================\r\n",-80}");
                WriteLine($"{" Sort by Title:",-20} {"[1]+",20}");
                WriteLine($"{" Sort by Text:",-20} {" [2]+",20}");
                WriteLine($"{" Sort by Cdate:",-20} {" [3]",20}");
                WriteLine($"{" Sort by Mdate:",-20} {" [4]",20}");
                WriteLine($"{" BACK:",-20} {"[EscKey]",20}");
                WriteLine($"{"\r\n*=============================================\r\n",-80}");

                

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
                            WriteLine("\r\nSorted by Title:\r\n");
                            book.Records.Sort( (NoteBookRecord a, NoteBookRecord b) => {
                                return string.Compare(a.Title, b.Title);
                            });
                            ReadKey();
                            
                            break;
                        }
                    case (ConsoleKey.D2):
                        {
                            WriteLine("\r\nSorted by Text:\r\n");
                            book.Records.Sort((NoteBookRecord a, NoteBookRecord b) => {
                                return string.Compare(a.Text, b.Text);
                            });
                            ReadKey();

                            break;
                        }

                    default:
                        { break; }

                }
            }
            while (!exitFlag);
        }

        //public static void updateField(NoteBookRecord rex, string fName)
        //{ 
        //    if
        //    rex[]
        //}
    }
}
