namespace Commercial_Controller
{
    //Button on a floor or basement to go back to lobby
    public class CallButton
    {
        public CallButton(int _id, int _floor, string _direction, string _status)
        {
            this.id = _id;
            this.floor = _floor;
            this.direction = _direction;
            this.status = _status;
        }

        public int id { get; set; }
        public int floor { get; set; }
        public string direction { get; set; }
        public string status { get; set; }
    }
}