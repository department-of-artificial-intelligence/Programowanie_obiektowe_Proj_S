using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{

    public class Movie
    {
        public int _id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string TagLine { get; set; }

        public Genre Genre { get; set; }

        public Author author { get; set; }

        public DateTime uploadDate {  get; set; }

        public Movie() { }

        public Movie(int id, string title, string desc, string tagLine, Genre gen, Author author, DateTime uploadDate)
        {
            _id = id;
            Title = title;
            Description = desc;
            TagLine = tagLine;
            Genre = gen;
            this.author = author;
            this.uploadDate = uploadDate;

        }

        public override string ToString() {
            return String.Format("Movie id: {0} \n Title: {1} \n Description: {2} \n TagLine: {3} \n Genre: {4} \n Author: {5} \n Uploaded: {6}",

                _id, Title, Description, TagLine, Genre, author, uploadDate
                );
        }

    }
}
