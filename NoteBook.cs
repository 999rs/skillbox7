using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace NoteBook
{
    /// <summary>
    /// Представляет книгу с записями. инкапсулирует методы печати книги в косоль, добвления новых записей , сортирующие методы и д.р.
    /// </summary>
    [Serializable]
    public struct NoteBook
    {

        public string Name { get;set;}
        
        /// <summary>
        /// список записей в книге
        /// </summary>
        public List<NoteBookRecord> Records;

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="name"></param>
        public NoteBook(string name) {
            this.Name = name;
            this.Records = new List<NoteBookRecord>();
        }

        /// <summary>
        /// печать всех записей книги
        /// </summary>
        public void printOut()
        {
            //WriteLine($"{"Guid",50}{"Modification date",25}{"Creation Date",25}");
            foreach (NoteBookRecord rec in this.Records)
            {
                //WriteLine("***************************************************************************");
                //WriteLine($"{"Guid:",-20}{rec.Id,50}\r\n{"Modification date:",-20}{rec.Mdate,50}\r\n{"Creation Date",-20}{rec.Cdate,50}");
                //WriteLine($"Title: {rec.Title}");
                //WriteLine($"Text: {rec.Text}");            
                rec.printRecord();
            }
        }

        /// <summary>
        /// добавление новой записи в список записей книги
        /// </summary>
        /// <param name="titleInput"></param>
        /// <param name="textInput"></param>
        public void addNewRecord(string titleInput = "", string textInput = "")
        {
            NoteBookRecord newRecord = new NoteBookRecord(titleInput, textInput);
            this.Records.Add(newRecord);

        }

        /// <summary>
        /// сортирует список записей по заголовку
        /// </summary>
        public void sortByTitle() 
        {
            this.Records.Sort((NoteBookRecord a, NoteBookRecord b) => {
                return string.Compare(a.Title, b.Title);
            });
        }

        /// <summary>
        /// сортирует список записей по тексту
        /// </summary>
        public void sortByText()
        {
            this.Records.Sort((NoteBookRecord a, NoteBookRecord b) =>
            {
                return string.Compare(a.Text, b.Text);
            });
        }

        /// <summary>
        /// сортирует список записей по дате создания
        /// </summary>
        public void sortByCdate()
        {
            this.Records.Sort((NoteBookRecord a, NoteBookRecord b) =>
            {
                return DateTime.Compare(a.Cdate, b.Cdate);
            });

        }

        /// <summary>
        /// сортирует список записей по дате изменения
        /// </summary>
        public void sortByMdate()
        {
            this.Records.Sort((NoteBookRecord a, NoteBookRecord b) =>
            {
                return DateTime.Compare(a.Mdate, b.Mdate);
            });

        }

        /// <summary>
        /// Очищает список записей в книге
        /// </summary>
        public void clear()
        {
            this.Records.Clear();
        }
    }
}
