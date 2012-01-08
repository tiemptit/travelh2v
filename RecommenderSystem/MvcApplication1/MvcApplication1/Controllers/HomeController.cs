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

        public string UpdateForm(string idPlace)
        {
            string details;
            switch (Convert.ToInt32(idPlace))
            {
                case 1:
                    details = "Address: 57A Tháp Mười P.2 Q.6 <br />Tel: (08)38556130 <br />Website: <a href=\"http://www.chobinhtay.gov.vn\" target=\"_blank\">http://www.chobinhtay.gov.vn</a>";
                    break;
                case 2:
                    details = "Address: 1 Lê Lợi P.Bến Nghé Q.1 <br />Tel: (08)38292096 <br />Website: <a href=\"http://www.skydoor.net/place/Ch%E1%BB%A3_B%E1%BA%BFn_Th%C3%A0nh\" target=\"_blank\">http://www.skydoor.net/place/cho-ben-thanh</a>";
                    break;
                case 3:
                    details = "Address: 34 An Dương Vương P.9 Q.5 <br />Tel: (08)38356609 <br />";
                    break;
                case 4:
                    details = "Address: 135 Nam Kỳ Khởi Nghĩa P.Bến Thành Q1 <br />Tel: (08)38223652 <br />Website: <a href=\"http://www.dinhdoclap.gov.vn\" target=\"_blank\">http://www.dinhdoclap.gov.vn</a>";
                    break;
                case 5:
                    details = "Address: 339 Nam Kỳ Khởi Nghĩa P.7 Q.3 <br />Tel: (04)38455435 <br />Website: <a href=\"http://vinhnghiemvn.com\" target=\"_blank\">http://vinhnghiemvn.com</a>";
                    break;
                case 6:
                    details = "Address: 1 Nguyễn Tất Thành P.12 Q.4 <br />Tel: (08)38483153 <br />Website: <a href=\"http://www.baotanghochiminh.vn\" target=\"_blank\">http://www.baotanghochiminh.vn/</a>";
                    break;
                case 7:
                    details = "Address: 120 Trần Bình Trọng P.2 Q.5 <br />Tel: (08)39235067 <br />Website: <a href=\"http://giothanhle.com/MassMap.php?Church=13\" target=\"_blank\">http://giothanhle.com/nha-tho-cho-quan</a>";
                    break;
                case 8:
                    details = "Address: 89B Bà Huyện Thanh Quan P.7 Q.3 <br />Website: <a href=\"http://www.phatgiao.vn/tuvien/trongnuoc/734619.aspx\" target=\"_blank\">http://www.phatgiao.vn/chua-xa-loi</a>";
                    break;
                case 9:
                    details = "Address: 28 Võ Văn Tần P.6 Q.3 <br />Tel: (08)39305587 <br />Website: <a href=\"http://www.baotangchungtichchientranh.vn\" target=\"_blank\">http://www.baotangchungtichchientranh.vn</a>";
                    break;
                case 10:
                    details = "Address: 18 An Dương Vương P.9 Q.5 <br />Tel: (08)38336688 <br />Website: <a href=\"http://www.windsorplazahotel.com\" target=\"_blank\">http://www.windsorplazahotel.com</a>";
                    break;
                case 11:
                    details = "Address: 34 Lê Duẩn P.Bến Nghé Q.1 <br />Tel: (08)38257750 <br />Website: <a href=\"http://www.diamondplaza.com.vn\" target=\"_blank\">http://www.diamondplaza.com.vn</a>";
                    break;
                case 12:
                    details = "Address: 126 Hùng Vương P.2 Q.10 <br />Tel: (08)22220388 <br />Website: <a href=\"http://www.megastar.vn\" target=\"_blank\">http://www.megastar.vn/</a>";
                    break;
                case 13:
                    details = "Address: 230 Nguyễn Trãi P.Nguyễn Cư Trinh Q.1 <br />Tel: (08)39206688 <br />Website: <a href=\"http://www.galaxycine.vn\" target=\"_blank\">http://www.galaxycine.vn/</a>";
                    break;
                case 14:
                    details = "Address: 12, 3 Tháng 2 P.12 Q.10 <br />Tel: (08)62909059 <br />Website: <a href=\"http://thodia.vn/nha-hang-mon-hue-ho-chi-minh-5.html\" target=\"_blank\">http://thodia.vn/nha-hang-mon-hue</a>";
                    break;
                case 15:
                    details = "Address: 71 - 73, Đồng Khởi P.Bến Nghé Q.1 <br />Tel: (08)39142424 <br />Website: <a href=\"http://pho24.com.vn\" target=\"_blank\">http://pho24.com.vn</a>";
                    break;
                case 16:
                    details = "Address: 1 Công Xã Paris P.Bến Nghé Q.1 <br />Tel: (08)39142424 <br />Website: <a href=\"http://giothanhle.com/MassMap.php?Church=23\" target=\"_blank\">http://giothanhle.com/nha-tho-duc-ba</a>";
                    break;
                case 17:
                    details = "Address: 3 Hòa Bình P.3 Q.11 <br />Tel: (08)38588418 <br />Website: <a href=\"http://www.damsenwaterpark.com.vn\" target=\"_blank\">http://www.damsenwaterpark.com.vn</a>";
                    break;
                case 18:
                    details = "Address: 1147 Bình Quới P.28 Q.Bình Thạnh <br />Tel: (08)35565891 <br />Website: <a href=\"http://www.binhquoiresort.com.vn\" target=\"_blank\">http://www.binhquoiresort.com.vn</a>";
                    break;
                case 19:
                    details = "Address: 141 Nguyễn Huệ P.Bến Nghé, Q.1 <br />Tel: (08)38292185 <br />Website: <a href=\"http://www.rexhotelvietnam.com\" target=\"_blank\">http://www.rexhotelvietnam.com</a>";
                    break;
                case 20:
                    details = "Address: 48/10 Điện Biên Phủ P.22 Q.Bình Thạnh <br />Tel: (08)35123026 <br />Website: <a href=\"http://www.zing.vn/news/choi-vui/oc-dao-xanh-van-thanh-giua-thanh-pho/a102444.html\" target=\"_blank\">http://www.zing.vn/news/choi-vui/van-thanh-resort</a>";
                    break;
                default:
                    details = "default";
                    break;
            }
            return details;
        }

    }
}
