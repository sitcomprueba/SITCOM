using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesEntities
{
  public  class WeekForMonth
    {

        public List<Day> Week1 { get; set; } //days for week1
        public List<Day> Week2 { get; set; } //days for week2
        public List<Day> Week3 { get; set; } //days for week3
        public List<Day> Week4 { get; set; } //days for week4
        public List<Day> Week5 { get; set; } //days for week5
        public List<Day> Week6 { get; set; } //days for week6
        public string nextMonth { get; set; }
        public string prevMonth { get; set; }

       


    }
}
