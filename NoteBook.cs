using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBook
{
    [Serializable]
    public struct NoteBook
    {
        public string Name { get;set;}
        public List<NoteBookRecord> Records;

        public NoteBook(string name) {
            this.Name = name;
            this.Records = new List<NoteBookRecord>();
        }
        //public NoteBook() {
        //    this.Records = new List<NoteBookRecord>();
        //}
    }
}
