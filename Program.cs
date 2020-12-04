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


            // создаем книгу
            NoteBook currentNoteBook = new NoteBook("MyNoteBook");
            

            
            // можно использовать для первоначальной загрузки книги без ввода имени
            // фала в консоли
            // Menu.importNoteBook(currentNoteBook, "alert.dat");  

            // Запуск меню
            Menu.drawMainMenu(currentNoteBook);

        }
       

    }

}
