using System;

namespace BookStore.Models
{
    public class Square
    {

        public int Id { get; set; }
        public int h { get; set; }
        public int a { get; set; }

        public void count(int a, int h)
        {
            s1 = a * h / 2.0;
        }

        public double s1 { get; set; }
        
        public DateTime Date { get; set; }
    }
}