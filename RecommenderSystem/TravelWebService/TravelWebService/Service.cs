using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Data;
using System.Web.Script.Serialization;
using RecommenderSystem.Core.Helper;

namespace TravelWebService
{
    // Start the service and browse to http://<machine_name>:<port>/Service/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class Service
    {
        // TODO: Implement the collection resource that will contain the SampleItem instances

        [WebGet(UriTemplate = "")]
        public List<SampleItem> GetCollection()
        {
            // TODO: Replace the current implementation to return a collection of SampleItem instances
            return new List<SampleItem>() { new SampleItem() { Id = 1, StringValue = "Hello" } };
        }

        [WebInvoke(UriTemplate = "", Method = "POST")]
        public SampleItem Create(SampleItem instance)
        {
            // TODO: Add the new instance of SampleItem to the collection
            throw new NotImplementedException();
        }

        [WebGet(UriTemplate = "{id}")]
        public SampleItem Get(string id)
        {
            // TODO: Return the instance of SampleItem with the given id
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        public SampleItem Update(string id, SampleItem instance)
        {
            // TODO: Update the given instance of SampleItem in the collection
            throw new NotImplementedException();
        }

        [WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        public void Delete(string id)
        {
            // TODO: Remove the instance of SampleItem with the given id from the collection
            throw new NotImplementedException();
        }


        ////////////////////////////////////////////////////////////////////////////////////

        // List All Places
        [WebGet(UriTemplate = "ListAllPlaces", ResponseFormat = WebMessageFormat.Json)]
        [Description("list operation.")]
        public Stream  GetAllPlaces()
        {
            /*
            PlaceCategoryModel place_category_obj = new PlaceCategoryModel();
            place_category_obj.id = 1;
            place_category_obj.place_category = "Market";
            
            PlaceModel place_obj = new PlaceModel();
            place_obj.id = 1;
            place_obj.place_category_obj = place_category_obj;
            place_obj.name = "Da Lat Market";
            place_obj.imgurl = "http://www.google.com/maps/place?source=uds&q=hotel&cid=6565492984366607731";
            place_obj.lat = 11.943115;
            place_obj.lng = 108.436554;
            place_obj.house_number = 201;
            place_obj.street = "Nguyen Thi Minh Khai";
            place_obj.ward = "6";
            place_obj.district = "10";
            place_obj.city = "Da Lat";
            place_obj.province = "Lam Dong";
            place_obj.country = "Viet Nam";
            place_obj.phone_number = "0909090909";
            place_obj.email = "abc@yahoo.com";
            place_obj.website = "www.google.com";
            place_obj.history = "history ...";
            place_obj.details = "details ...";
            place_obj.sources = "www.google.com.vn";
            place_obj.general_rating = 5;
            place_obj.general_count_rating = 100;
            place_obj.general_sum_rating = 500;
            List<PlaceModel> places = new List<PlaceModel>();
            places.Add(place_obj);
            places.Add(place_obj);
            places.Add(place_obj);
            */

            List<PlaceModel> places = PlaceModel.GetAllPlaces();

            var javaScriptSerializer = new JavaScriptSerializer();
            string jsonStringMultiple = "{\"responseData\":" + javaScriptSerializer.Serialize(places.Select(x => new { x.id, x.place_category_obj, x.name, x.imgurl, x.lat, x.lng, x.house_number, x.street, x.ward, x.district, x.city, x.province, x.country, x.phone_number, x.email, x.website, x.history, x.details, x.sources, x.general_rating, x.general_count_rating, x.general_sum_rating })) + "}";
            var json = Encoding.UTF8.GetBytes(jsonStringMultiple);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/javascript; charset=utf-8";
            
            
            var memoryStream = new MemoryStream(json);
            return memoryStream;
            //javaScriptSerializer.Serialize(places.Select(x => new { x.id, x.place_category_obj, x.name,x.imgurl, x.lat, x.lng, x.house_number, x.street, x.ward,x.district, x.city,x.province,x.country, x.phone_number, x.email, x.website, x.history, x.details, x.sources, x.general_rating, x.general_count_rating, x.general_sum_rating}));
            //return "{\"responseData\":" + jsonStringMultiple + "}";
        }

    }
}
