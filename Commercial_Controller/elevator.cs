using System.Threading;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Elevator
    {
        public string ID { get; set; }
        public string status { get; set; }
        public int currentFloor { get; set; }
        public Door door { get; set; }
        public List<int> floorRequestsList;
        public List<int> completedRequestsList;
        public int screenDisplay { get; set; }
        public string direction { get; set; }
        public bool overweight { get; set; }
        public bool overweightAlarm { get; set; }

        public Elevator(string _elevatorID, string _status, int _currentFloor)
        {
            ID = _elevatorID;
            status = _status;
            currentFloor = _currentFloor;
            door = new Door(1, "Closed");
            floorRequestsList = new List<int>();
            completedRequestsList = new List<int>();
            direction = "any";
            overweight = false;
            overweightAlarm = false;
            screenDisplay = currentFloor;
        }
        public void move()
        {
            while (floorRequestsList.Count > 0) 
            {
                int destination = floorRequestsList[0];
                status = "moving";
                if (currentFloor < destination)
                {
                    direction = "up";
                    sortFloorList();
                    while (currentFloor < destination)
                    {
                        currentFloor++;
                        screenDisplay = currentFloor;
                    }
                }
                else if (currentFloor > destination)
                {
                    direction = "down";
                    sortFloorList();
                    while (currentFloor > destination)
                    {
                        currentFloor--;
                        screenDisplay = currentFloor;
                    }
                }
                status = "stopped";
                operateDoors();
                if (!completedRequestsList.Contains(destination))
                {
                    completedRequestsList.Add(destination);
                }
                floorRequestsList.RemoveAt(0);
            }
            status = "idle";
        }
        
        public void sortFloorList()
        {
            if (floorRequestsList.Count > 1)
            {
                if (direction == "up")
                {
                    floorRequestsList.Sort((x, y) => x.CompareTo(y));
                }
                else 
                {
                    floorRequestsList.Sort((x, y) => y.CompareTo(x));
                }
            }
        }

        void operateDoors()
        {
            door.status = "Opening";
            // Wait 5 seconds
            if (!isOverweight())
            {
                door.status = "Closing";
                if(!door.isObstructed())
                {
                    door.status= "Closed";
                }
                else
                {
                    operateDoors();
                }
            }
            else
            {
                while (isOverweight())
                {
                    overweightAlarm = true;
                }
                overweightAlarm = false;
                operateDoors();
            }
        }

        public void addNewRequest(int requestedFloor)
        {
            if (!floorRequestsList.Contains(requestedFloor))
            {
                floorRequestsList.Insert(0, requestedFloor);
            }
            if (currentFloor < requestedFloor)
            {
                direction = "up";
            }
            if (currentFloor > requestedFloor)
            {
                direction = "down";
            }
        } 

        public bool isOverweight()
        {
            return overweight;
        }
    }
}