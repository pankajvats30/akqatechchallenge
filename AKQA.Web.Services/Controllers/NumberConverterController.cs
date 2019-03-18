using AKQA.Web.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AKQA.Web.Services.Controllers
{
    public class NumberConverterController : ApiController
    {
        /// <summary>
        /// Returns the input number into Strings 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="postCode"></param>
        /// <returns>Returns the String conversion of provided name concatinated with Name</returns>
        [Route("api/numberconverter/{number}/")]
        [HttpGet]
        public string Convert(string number)
        {
            string response ="";
            try
            {
                decimal inputNumber;
                var IsNumber= decimal.TryParse(number,out inputNumber);
                if (IsNumber)
                {
                    /* convert Number to words */
                    response = NumberToText.Convert(inputNumber);
                  
                }
                else
                {
                    response= "Invalid Input";
                }


            }
            catch(Exception ex)
            {
                response = "Sorry there is something wrong! Exception message:" + ex.Message;

            }
            return response;
        }

    }
}