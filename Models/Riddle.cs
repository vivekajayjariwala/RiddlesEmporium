﻿using Microsoft.Identity.Client;

namespace RiddlesWebApp.Models
{
    public class Riddle
    {
        public int Id { get; set; }
        public string RiddleQuestion {  get; set; }
        public string RiddleAnswer { get; set;}

        public Riddle ()
        {

        }
    }
}
