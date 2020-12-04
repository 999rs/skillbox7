using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    class Program
    {
        static void Main(string[] args)
        {
            NoteBook currentNoteBook = new NoteBook("MyNoteBook");
            /// Разработать ежедневник.
            /// В ежедневнике реализовать возможность 
            /// - создания
            /// - удаления
            /// - реактирования 
            /// записей
            /// 
            /// В отдельной записи должно быть не менее пяти полей
            /// 
            /// Реализовать возможность 
            /// - Загрузки даннах из файла
            /// - Выгрузки даннах в файл
            /// - Добавления данных в текущий ежедневник из выбранного файла
            /// - Импорт записей по выбранному диапазону дат
            /// - Упорядочивания записей ежедневника по выбранному полю

            Menu.importNoteBook(currentNoteBook, "redalert.dat");

            Menu.drawMainMenu(currentNoteBook);









        }
        
      //  private static List<NoteBookRecord> currentNoteBook = new List<NoteBookRecord>();
    }

}
