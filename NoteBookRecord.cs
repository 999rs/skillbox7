using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace NoteBook
{
    [Serializable]
    public struct NoteBookRecord
    {
        public Guid Id { get; }
        public DateTime Cdate { get; }
        public DateTime Mdate { get; private set; }

        private string _Title ;
        public string Title
        {
            get { return this._Title; }
            set
            {
                if (value.Contains(";"))
                {
                    value = value.Replace(";", "");
                    WriteLine("Из заголовка вырезан символ разделитель \";\" ");
                }
                
                this._Title = value;
                updateMdate();
                

            }
        }

        private string _Text;
        public string Text
        {
            get { return this._Text; }
            set
            {
                if (value.Contains(";"))
                {
                    value = value.Replace(";", "");
                    WriteLine("Из текста вырезан символ разделитель \";\"");
                }
                
                this._Text = value;
                updateMdate();
                

            }
        }
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

        private void updateMdate()
        {
            this.Mdate = DateTime.Now;
        }
    }
}
