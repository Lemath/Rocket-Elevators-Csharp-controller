namespace Commercial_Controller
{
    public class Door
    {

        public Door(int _id, string _status)
        {
            ID = _id;
            status = _status;
            sensorState = "OFF";

        }
        public int ID { get; set; }
        public string status { get; set; }
        public string sensorState { get; set; }

        public bool isObstructed() => sensorState == "ON";
    }

   
}