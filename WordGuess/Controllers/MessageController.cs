using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WordGuess.Models;
using WordGuess.Classes;

namespace WordGuess.Controllers
{
    
    [Route("/api/[controller]")]
    public class MessageController : Controller
    {
        
        [HttpPost]
        public IActionResult PostMessage([FromBody] Content requestBody)
        {
            string content = requestBody.content.ToLower().Replace(" ","");
            if (ModelState.IsValid)
            {
                Response.Cookies.Append("myCookie", content);
                return Ok(content);
            }
            else
            {
                return BadRequest("Что то пошло не так!!!");
            }
        }
         
       [HttpGet]
        public IActionResult GetMessage()
        {
            
            string answer = Request.Cookies["myCookie"];
            string secret = Request.Cookies["secret"];
            
            if (Request.Cookies.ContainsKey("myCookie"))
            {
                if (answer.Length == secret.Length)
                {
                    Levenshtein.LevenshteinDistance(answer, secret);
                    if (answer == secret)
                    {
                        
                        string access = "Ты ответил правильно!!!" +
                                        "\nЭто слово " + secret.ToUpper() + ", можешь перезагрузить страницу, чтобы я загадал новое слово";
                        return Json(GetResponses.GetResponse(access));
                    }
                    else if(Levenshtein.LevenshteinDistance(answer,secret) <=3)
                    {
                        string minDifferent = "Cлово максимально похоже на загаданное, нужно изменить всего лишь " + Levenshtein.LevenshteinDistance(answer,secret) + " буквы";
                        return Json(GetResponses.GetResponse(minDifferent));
                    }
                    else
                    {
                        string fail = "Ты ответил неправильно((";
                        string guess = Request.Cookies["Guess"];
                        int Guesses = int.Parse(guess);
                        Guesses++;
                        guess = Guesses.ToString();
                        Response.Cookies.Delete("Guess");
                        Response.Cookies.Append("Guess", guess);
                        if (Guesses >= 3)
                        {
                            string letter = arrayCookies();
                            string failGuesses = letter;
                            var lelee = Request.Cookies.Keys;
                            Response.Cookies.Delete("Guess");
                            Response.Cookies.Append("Guess", "0");
                            return Json(GetResponses.GetResponse(failGuesses));
                        }
                        return Json(GetResponses.GetResponse(fail));
                    }
                }
                else
                {
                    if (answer.Length < secret.Length)
                    {
                        List<string> LenghtBigger = new List<string>()
                        {
                            "Загаданное слово больше, чем ты написал",
                            "Неа,твое слово меньше, чем загаданное",
                            "Эх, какое короткое слово(((",
                            "Попробуй написать слово чуть подлиннее"
                        };
                        Random rand = new Random();
                        int index = rand.Next(LenghtBigger.Count);
                        string answerToClient = LenghtBigger[index];
                        return Json(GetResponses.GetResponse(answerToClient));
                    }
                    else
                    {
                        List<string> LenghtShorter = new List<string>()
                        {
                            "Загаданное слово меньше, чем ты написал",
                            "Неа,твое слово больше, чем загаданное",
                            "Эх, какое длинное слово(((",
                            "Попробуй написать слово чуть короче"
                        };
                        Random rand = new Random();
                        int index = rand.Next(LenghtShorter.Count);
                        string answerToClient = LenghtShorter[index];
                        return Json(GetResponses.GetResponse(answerToClient));
                    }
                }
            }
            else if(answer == null )
            {
                string nullValue = "Хэээй, ты ничего не написал";
                return Json(GetResponses.GetResponse(nullValue));
            }
            else
            {
                string failGetData = "Что то не так с сервером!!!";
                return Json(GetResponses.GetResponse(failGetData));
            }

        
        
    }

        public string arrayCookies()
        {
            for (int i = 0; i < 10; i++)
            {
                string str = i.ToString();

                if (Request.Cookies[str] != null)
                {
                    string name ="Опять ошибся! Ладно, держи букву "+ Request.Cookies[str];
                    Response.Cookies.Delete(str);
                    return name;
                    
                }
                else
                {
                   
                }
            }
            
            
            string namesecret = "Ну как так то? Загаданное слово было " + Request.Cookies["secret"];
            return namesecret;
        }

    }
    
    
}