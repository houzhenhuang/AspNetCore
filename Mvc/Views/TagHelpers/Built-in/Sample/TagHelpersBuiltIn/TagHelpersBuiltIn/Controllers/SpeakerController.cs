using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TagHelpersBuiltIn.Controllers
{
    public class SpeakerController : Controller
    {
        private List<Speaker> Speakers = new List<Speaker>
        {
            new Speaker {SpeakerId = 10},
            new Speaker {SpeakerId = 11},
            new Speaker {SpeakerId = 12}
        };
        public IActionResult Index() => View(Speakers);
        [Route("/Speaker/EvaluationsCurrent",
            Name = "speakerevalscurrent")]
        public IActionResult Evaluations(int speakerId, bool currentYear) => View();
        [Route("/Speaker/Evaluations",
           Name = "speakerevals")]
        public IActionResult Evaluations() => View();
        [Route("Speaker/{id:int}")]
        public IActionResult Detail(int id) => View(Speakers.FirstOrDefault(a=>a.SpeakerId==id));

    }
    public class Speaker
    {
        public int SpeakerId { get; set; }
    }
}