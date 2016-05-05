using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HSProjectMVC.Models
{
    public class VideoGameModel
    {
        public int Id { get; set; }
        public string GameName { get; set; }
        public string Systems { get; set; }
        public decimal Price { get; set; }
        public string Publisher { get; set; }
        public string CoverArt { get; set; }

        public VideoGameModel()
        {

        }
    }
}