using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using static System.Console;

namespace NoteBook
{
    /// <summary>
    /// структура записи, инкапсулирует собственные свойства и методы. Аксессоры к полям
    /// </summary>
    [Serializable]
    public struct NoteBookRecord
    {
        /// <summary>
        /// уникальный Ид записи
        /// </summary>
        public Guid Id { get; }
        /// <summary>
        /// дата создания.
        /// </summary>
        public DateTime Cdate { get; } 
        /// <summary>
        /// дата последнего изменения
        /// </summary>
        public DateTime Mdate { get; private set; }  

        private string _Title ;
        /// <summary>
        /// заголовок записи
        /// </summary> 
        public string Title
        {
            get { return this._Title; }
            set
            {
                
                this._Title = value;
                updateMdate();
                

            }
        }

        private string _Text;
        /// <summary>
        /// текст записи
        /// </summary>
        public string Text
        {
            get { return this._Text; }
            set
            {
                
                this._Text = value;
                updateMdate();
                

            }
        }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="title">заголовок</param>
        /// <param name="text">текст</param>
        public NoteBookRecord(string title = "", string text = "") 
        {
            this.Id = Guid.NewGuid();
            this.Cdate = DateTime.Now;
            this.Mdate = DateTime.Now;
            this._Title = "";
            this._Text = "";
            this.Title = title;
            this.Text = text;          
            
            

        }
         

        /// <summary>
        /// обновляет дату модификации, используется в сеттерах
        /// </summary>
        private void updateMdate()
        {
            this.Mdate = DateTime.Now;
        }

        /// <summary>
        /// выводит на консоль данные записи
        /// </summary>
        public void printRecord()
        {
            //WriteLine($"{"Guid",50}{"Modification date",25}{"Creation Date",25}");

                WriteLine("\r\n***************************************************************************");
                WriteLine($"{"Guid:",-20}{this.Id,50}\r\n{"Modification date:",-20}{this.Mdate,50}\r\n{"Creation Date",-20}{this.Cdate,50}");
                WriteLine($"Title: {this.Title}");
                WriteLine($"Text: {this.Text}");
            

        }

    }
}
