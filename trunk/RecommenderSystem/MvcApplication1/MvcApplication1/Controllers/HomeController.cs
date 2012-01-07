using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using MvcApplication1.Models;


namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Home()
        {
            // load List Places, Budget, Weather, Companion, Rating
            List<SelectListItem> ListPlace = new List<SelectListItem>();
            ListPlace.Add(new SelectListItem { Text = "Cho Lon market (Chợ Lớn, chợ Bình Tây)", Value = "1" });
            ListPlace.Add(new SelectListItem { Text = "Ben Thanh market (Chợ Bến Thành)", Value = "2" });
            ListPlace.Add(new SelectListItem { Text = "An Dong market (Chợ An Đông)", Value = "3" });
            ListPlace.Add(new SelectListItem { Text = "Reunification Palace (Dinh Độc Lập)", Value = "4" });
            ListPlace.Add(new SelectListItem { Text = "Vinh Nghiem Pagoda (Chùa Vĩnh Nghiêm)", Value = "5" });
            ListPlace.Add(new SelectListItem { Text = "Ho Chi Minh museum (Bảo tàng HCM)", Value = "6" });
            ListPlace.Add(new SelectListItem { Text = "Cho Quan church (Nhà thờ Chợ Quán)", Value = "7" });
            ListPlace.Add(new SelectListItem { Text = "Xa Loi pagoda (Chùa Xá Lợi)", Value = "8" });
            ListPlace.Add(new SelectListItem { Text = "War Remnants Museum (Bảo tàng Chứng Tích Chiến Tranh)", Value = "9" });
            ListPlace.Add(new SelectListItem { Text = "Windsor Plaza hotel", Value = "10" });
            ListPlace.Add(new SelectListItem { Text = "Diamond Plaza", Value = "11" });
            ListPlace.Add(new SelectListItem { Text = "Megastar Cinema", Value = "12" });
            ListPlace.Add(new SelectListItem { Text = "Galaxy Cinema", Value = "13" });
            ListPlace.Add(new SelectListItem { Text = "Mon Hue restaurant (Nhà hàng Món Huế)", Value = "14" });
            ListPlace.Add(new SelectListItem { Text = "Pho 24 (Phở 24)", Value = "15" });
            ListPlace.Add(new SelectListItem { Text = "Duc Ba church (Nhà thờ Đức Bà)", Value = "16" });
            ListPlace.Add(new SelectListItem { Text = "Dam Sen water park (CV nước Đầm Sen)", Value = "17" });
            ListPlace.Add(new SelectListItem { Text = "Binh Quoi resort (Khu du lịch Bình Quới)", Value = "18" });
            ListPlace.Add(new SelectListItem { Text = "Rex hotel", Value = "19" });
            ListPlace.Add(new SelectListItem { Text = "Van Thanh resort (Khu du lịch Văn Thánh)", Value = "20" });
            ViewData["ListPlace"] = ListPlace;

            List<SelectListItem> ListBudget = new List<SelectListItem>();
            ListBudget.Add(new SelectListItem { Text = "Budget traveler (Vừa phải)", Value = "1" });
            ListBudget.Add(new SelectListItem { Text = "Price for quality (Tương đối tốt)", Value = "2" });
            ListBudget.Add(new SelectListItem { Text = "High spender (Cao)", Value = "3" });
            ViewData["ListBudget"] = ListBudget;

            List<SelectListItem> ListWeather = new List<SelectListItem>();
            ListWeather.Add(new SelectListItem { Text = "Sunny (Nắng)", Value = "1" });
            ListWeather.Add(new SelectListItem { Text = "Cloudy (Âm u)", Value = "2" });
            ListWeather.Add(new SelectListItem { Text = "Clear sky (Trong xanh)", Value = "3" });
            ListWeather.Add(new SelectListItem { Text = "Rainy (Mưa)", Value = "4" });
            ViewData["ListWeather"] = ListWeather;

            List<SelectListItem> ListCompanion = new List<SelectListItem>();
            ListCompanion.Add(new SelectListItem { Text = "Alone (Một mình)", Value = "1" });
            ListCompanion.Add(new SelectListItem { Text = "Friends / Colleagues (Bạn bè / Đồng nghiệp)", Value = "2" });
            ListCompanion.Add(new SelectListItem { Text = "Family (Gia đình)", Value = "3" });
            ListCompanion.Add(new SelectListItem { Text = "Girlfriend / Boyfriend (Người yêu)", Value = "4" });
            ListCompanion.Add(new SelectListItem { Text = "Children (Trẻ em)", Value = "5" });
            ViewData["ListCompanion"] = ListCompanion;

            List<SelectListItem> ListRating = new List<SelectListItem>();
            ListRating.Add(new SelectListItem { Text = "1 point", Value = "1" });
            ListRating.Add(new SelectListItem { Text = "2 points", Value = "2" });
            ListRating.Add(new SelectListItem { Text = "3 points", Value = "3" });
            ListRating.Add(new SelectListItem { Text = "4 points", Value = "4" });
            ListRating.Add(new SelectListItem { Text = "5 points", Value = "5" });
            ViewData["ListRating"] = ListRating;

            // Message
            ViewBag.Message = "Please fill out this form and click Rate! (Xin vui lòng điền form và nhấn Đánh giá!)";
            return View();
        }

        // POST: // insert a rating into DB
        [HttpPost]
        public ActionResult Home(HomeModel model)
        {
            // load List Places, Budget, Weather, Companion, Rating
            List<SelectListItem> ListPlace = new List<SelectListItem>();
            ListPlace.Add(new SelectListItem { Text = "Cho Lon market (Chợ Lớn, chợ Bình Tây)", Value = "1" });
            ListPlace.Add(new SelectListItem { Text = "Ben Thanh market (Chợ Bến Thành)", Value = "2" });
            ListPlace.Add(new SelectListItem { Text = "An Dong market (Chợ An Đông)", Value = "3" });
            ListPlace.Add(new SelectListItem { Text = "Reunification Palace (Dinh Độc Lập)", Value = "4" });
            ListPlace.Add(new SelectListItem { Text = "Vinh Nghiem Pagoda (Chùa Vĩnh Nghiêm)", Value = "5" });
            ListPlace.Add(new SelectListItem { Text = "Ho Chi Minh museum (Bảo tàng HCM)", Value = "6" });
            ListPlace.Add(new SelectListItem { Text = "Cho Quan church (Nhà thờ Chợ Quán)", Value = "7" });
            ListPlace.Add(new SelectListItem { Text = "Xa Loi pagoda (Chùa Xá Lợi)", Value = "8" });
            ListPlace.Add(new SelectListItem { Text = "War Remnants Museum (Bảo tàng Chứng Tích Chiến Tranh)", Value = "9" });
            ListPlace.Add(new SelectListItem { Text = "Windsor Plaza hotel", Value = "10" });
            ListPlace.Add(new SelectListItem { Text = "Diamond Plaza", Value = "11" });
            ListPlace.Add(new SelectListItem { Text = "Megastar Cinema", Value = "12" });
            ListPlace.Add(new SelectListItem { Text = "Galaxy Cinema", Value = "13" });
            ListPlace.Add(new SelectListItem { Text = "Mon Hue restaurant (Nhà hàng Món Huế)", Value = "14" });
            ListPlace.Add(new SelectListItem { Text = "Pho 24 (Phở 24)", Value = "15" });
            ListPlace.Add(new SelectListItem { Text = "Duc Ba church (Nhà thờ Đức Bà)", Value = "16" });
            ListPlace.Add(new SelectListItem { Text = "Dam Sen water park (CV nước Đầm Sen)", Value = "17" });
            ListPlace.Add(new SelectListItem { Text = "Binh Quoi resort (Khu du lịch Bình Quới)", Value = "18" });
            ListPlace.Add(new SelectListItem { Text = "Rex hotel", Value = "19" });
            ListPlace.Add(new SelectListItem { Text = "Van Thanh resort (Khu du lịch Văn Thánh)", Value = "20" });
            ViewData["ListPlace"] = ListPlace;

            List<SelectListItem> ListBudget = new List<SelectListItem>();
            ListBudget.Add(new SelectListItem { Text = "Budget traveler (Vừa phải)", Value = "1" });
            ListBudget.Add(new SelectListItem { Text = "Price for quality (Tương đối tốt)", Value = "2" });
            ListBudget.Add(new SelectListItem { Text = "High spender (Cao)", Value = "3" });
            ViewData["ListBudget"] = ListBudget;

            List<SelectListItem> ListWeather = new List<SelectListItem>();
            ListWeather.Add(new SelectListItem { Text = "Sunny (Nắng)", Value = "1" });
            ListWeather.Add(new SelectListItem { Text = "Cloudy (Âm u)", Value = "2" });
            ListWeather.Add(new SelectListItem { Text = "Clear sky (Trong xanh)", Value = "3" });
            ListWeather.Add(new SelectListItem { Text = "Rainy (Mưa)", Value = "4" });
            ViewData["ListWeather"] = ListWeather;

            List<SelectListItem> ListCompanion = new List<SelectListItem>();
            ListCompanion.Add(new SelectListItem { Text = "Alone (Một mình)", Value = "1" });
            ListCompanion.Add(new SelectListItem { Text = "Friends / Colleagues (Bạn bè / Đồng nghiệp)", Value = "2" });
            ListCompanion.Add(new SelectListItem { Text = "Family (Gia đình)", Value = "3" });
            ListCompanion.Add(new SelectListItem { Text = "Girlfriend / Boyfriend (Người yêu)", Value = "4" });
            ListCompanion.Add(new SelectListItem { Text = "Children (Trẻ em)", Value = "5" });
            ViewData["ListCompanion"] = ListCompanion;

            List<SelectListItem> ListRating = new List<SelectListItem>();
            ListRating.Add(new SelectListItem { Text = "1 point", Value = "1" });
            ListRating.Add(new SelectListItem { Text = "2 points", Value = "2" });
            ListRating.Add(new SelectListItem { Text = "3 points", Value = "3" });
            ListRating.Add(new SelectListItem { Text = "4 points", Value = "4" });
            ListRating.Add(new SelectListItem { Text = "5 points", Value = "5" });
            ViewData["ListRating"] = ListRating;

            //string email = model.EmailUser.ToString();
            string email = Request.Form["EmailUser"];
            string place = Request.Form["ListPlace"];
            string budget = Request.Form["ListBudget"];
            string weather = Request.Form["ListWeather"];
            string companion = Request.Form["ListCompanion"];
            string traveltime_tmp = Request.Form["TravelTime"];
            string[] traveltime_split = traveltime_tmp.Split(' ');
            string traveltime = traveltime_split[0] + "and" + traveltime_split[1];
            string rating = Request.Form["ListRating"];

            //build rating string
            string serverIP = "192.168.0.100";
            string urlRate = "http://" + serverIP + "/wcf4webservices/Service/Rate?username="
                + email + "&place=" + place + "&weather=" + weather
                + "&companion=" + companion + "&budget=" + budget
                + "&time=" + traveltime + "&rating=" + rating;
           
            // call web services to rate
            Uri uri = new Uri(urlRate);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Get;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string  tmp = reader.ReadToEnd();
            response.Close();

            if (tmp.Contains("true"))
            {
                // thank you and ask for more rating
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">");
                Response.Write("alert('Thank you! You can rate other places! (Cảm ơn! Bạn có để đánh giá nhiều địa điểm khác nữa!)');");
                Response.Write("</script>");
            }else{
                // thank you and ask for more rating
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">");
                Response.Write("alert('Rate fail! Please rate again! (Đánh giá thất bại! Xin vui lòng đánh giá lại!)');");
                Response.Write("</script>");
            }
            
            // Redisplay form
            return View(model);
        }

        public ActionResult About()
        {
             return View();
        }

    }
}
