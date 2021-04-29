using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WordGuess.Classes;
using WordGuess.Models;

namespace WordGuess.Controllers
{
    [Route("/api/[controller]")]
    public class SecretWordController : Controller
    {
        private List<WordModel> SecretWord = new List<WordModel>()
        {
            (new WordModel()
            {
                SecretWord =  "лепешка",
                Description = "Это вкусное хлебобулочное изделие)"
            }),
            (new WordModel()
            {
                SecretWord =   "лимон",
                Description = "Очень кислый фрукт",
            }),
            (new WordModel()
            {
                SecretWord =  "шляпа",
                Description = "Этот предмет одежды надеваешь на голову",
            }),
            (new WordModel()
            {
                SecretWord =  "солнце",
                Description = "Газовый гигант",
            }),
            (new WordModel()
            {
                SecretWord =  "ботинок",
                Description = "Этот предмет одежды надевается на ногу",
            }),
            (new WordModel()
            {
                SecretWord =   "кекс",
                Description = "Сладкое хлебобулочное изделие(может быть шоколадным)",
            }),
            (new WordModel()
            {
                SecretWord =   "лягушка",
                Description = "Слизкое, оооочень слизкое животное, еще прыгает)",
            }),
            (new WordModel()
            {
                SecretWord =   "котенок",
                Description = "Милый пушистый комочек)",
            }),
            (new WordModel()
            {
                SecretWord =   "ризотто",
                Description = "Итальянское блюдо на основе риса",
            }),
            (new WordModel()
            {
                SecretWord =   "чай",
                Description = "Бывает разных он цветов: зеленый, черный, красный",
            }),
        };
        
        [HttpGet]
        public IActionResult Index()
        {
            Random rand = new Random();
            int index = rand.Next(SecretWord.Count);
            string secretWord = SecretWord[index].SecretWord;
            string description = SecretWord[index].Description;
            Response.Cookies.Append("secret", secretWord);
            char[] array = secretWord.ToCharArray();
            List<string> secretLetterArray = new List<string>();
            foreach (var letter in array)
            {
                secretLetterArray.Add(letter.ToString());
            }
            for (int i = 0; i < secretLetterArray.Count; i++)
            {
                string letterIndex = i.ToString();
                Response.Cookies.Append(letterIndex,secretLetterArray[i]);
            }
            Response.Cookies.Append("Guess", "0");
            return Json(GetResponses.GetResponse(description));
            
        }

    }
}