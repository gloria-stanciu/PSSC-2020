using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace L06.Inputs
{
    public class CheckLanguageCmd
    {
        [Required]
        public string Text { get; }

        public CheckLanguageCmd(string text)
        {
            Text = text;
        }
    }
}
