using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    
    public class Battery
    {
        public int ID { get; set; }
        public string status { get; set; }
        public List<Column> columnsList;
        public List<FloorRequestButton> floorRequestsButttonsList;
        char columnID;
        int floorRequestButtonID;
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            ID = _ID;
            status = "online";
            columnsList = new List<Column>();
            floorRequestsButttonsList = new List<FloorRequestButton>();
            columnID = 'A';
            floorRequestButtonID = 1;
            if (_amountOfBasements > 0)
            {
                createBasementFloorRequestButtons(_amountOfBasements);
                createBasementColumn(_amountOfBasements, _amountOfElevatorPerColumn);
                _amountOfColumns --;
            }
            createFloorRequestButtons(_amountOfFloors);
            createColumns(_amountOfColumns, _amountOfFloors, _amountOfElevatorPerColumn);
        }

        public void createBasementColumn(int amountOfBasements, int amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int> ();
            int floor = -1;
            while (floor >= amountOfBasements) 
            {
                servedFloors.Add(floor);
                floor--;
            }
            columnsList.Add(new Column(columnID.ToString(), amountOfElevatorPerColumn, servedFloors, true, "online", amountOfBasements));
            //System.Console.WriteLine("created column ID " + columnID);
            columnID++;
        }

        public void createColumns(int amountOfColumn, int amountOfFloors, int amountOfElevator)
        {
            decimal amountOfFloorsPerColumn = Math.Ceiling(Convert.ToDecimal(amountOfFloors) / Convert.ToDecimal(amountOfColumn));
            int floor = 1;
            for (int column = 1; column <= amountOfColumn; column++)
            {
                List<int> servedFloors = new List<int>();
                for (int i = 0; i < amountOfFloorsPerColumn; i++)
                {
                    if (floor <= amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                columnsList.Add(new Column(columnID.ToString(), amountOfElevator, servedFloors, false, "online", amountOfFloors));
                //System.Console.WriteLine("created column ID " + columnID);
                columnID++;
            }
        }

        public void createFloorRequestButtons(int amountOfFloors)
        {
            for (int buttonFloor = 1; buttonFloor <= amountOfFloors; buttonFloor++)
            {
                floorRequestsButttonsList.Add(new FloorRequestButton(floorRequestButtonID, buttonFloor, "up", "OFF"));
                //System.Console.WriteLine("created request button ID " + floorRequestButtonID);
                floorRequestButtonID++;
            }
        }
        public void createBasementFloorRequestButtons(int amountOfBasements)
        {
            
            for (int buttonFloor = -1; buttonFloor >= -amountOfBasements; buttonFloor--)
            {
                floorRequestsButttonsList.Add(new FloorRequestButton(floorRequestButtonID, buttonFloor, "down", "OFF"));
                //System.Console.WriteLine("created request button ID " + floorRequestButtonID);
                floorRequestButtonID++;
            }
        }

        public Column findBestColumn(int requestedFloor)
        {
            Column bestColumn = null;
            foreach(Column column in columnsList)
            {
                if (column.servedFloorsList.Contains(requestedFloor))
                {
                    bestColumn = column;
                }
            }
            return bestColumn;
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int requestedFloor, string direction)
        {
            Column column = findBestColumn(requestedFloor);
            Elevator elevator = column.findElevator(1, direction);
            elevator.addNewRequest(1);
            elevator.move();
            elevator.addNewRequest(requestedFloor);
            elevator.move();
            return (column, elevator);
        }
    }
}

