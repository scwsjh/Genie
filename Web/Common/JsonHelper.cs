namespace Web
{
    public class JsonHelper
    {
        public static JsonResult DictionaryToJsonResult(Dictionary<object, object> dictionary)
        {
            JsonResult result = new JsonResult(dictionary);
            return result;
        }

        public static JsonResult jsonResult(ResultJson rj)
        {
            JsonResult result = new JsonResult(rj);
            return result;

            //Dictionary<object, object> data = new Dictionary<object, object>();
            //data.Add("Success", rj.Success);
            //data.Add("Code", rj.Code);
            //data.Add("Msg", rj.Msg);
            //data.Add("Data", rj.Data);
            //data.Add("DataExt", rj.DataExt);
            //return DictionaryToJsonResult(data);
        }
    }
}