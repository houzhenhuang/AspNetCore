using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesSample.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }
        public bool IsWeekend
        {
            get
            {
                var dayOfweek = DateTime.Now.DayOfWeek;

                return dayOfweek == DayOfWeek.Saturday || DayOfWeek.Tuesday == dayOfweek;
            }
        }
        public void OnGet()
        {
            Message = "Your contact page.";
        }
    }
}
