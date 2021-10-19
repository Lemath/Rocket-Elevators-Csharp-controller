using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Column
    {
        public string ID { get; set; }
        public string status { get; set; }
        public List<int> servedFloorsList;
        public bool isBasement { get; set; } 
        public List<Elevator> elevatorsList;
        public List<CallButton> callButtonsList;
        public Column(string _ID, int _amountOfElevators, List<int> _servedFloors, bool _isBasement, string _status, int _amountOfFloors)
        {
            ID = _ID;
            status = _status;
            servedFloorsList = _servedFloors;
            isBasement = _isBasement;
            elevatorsList = new List<Elevator>();
            callButtonsList = new List<CallButton>();
            createElevators(_amountOfFloors, _amountOfElevators);
            createCallButtons(_amountOfFloors, isBasement);
        }

        public void createCallButtons(int amountOfFloors, bool isBasement)
        {
            int callButtonID = 1;
            if (isBasement)
            {
                for (int buttonFloor = -1; buttonFloor >= -amountOfFloors; buttonFloor--)
                {
                    callButtonsList.Add(new CallButton(callButtonID, buttonFloor, "up", "OFF"));
                    //System.Console.WriteLine("created button ID " + callButtonID);
                    callButtonID++;
                }
            }
            else
            {
                for (int buttonFloor = 1; buttonFloor <= amountOfFloors; buttonFloor++)
                {
                    callButtonsList.Add(new CallButton(callButtonID, buttonFloor, "down", "OFF"));
                    //System.Console.WriteLine("created button ID " + callButtonID);
                    callButtonID++;
                }
            }
        }

        public void createElevators(int amountOfFloors, int amountOfElevators)
        {
            
            for (int elevatorID = 1; elevatorID <= amountOfElevators; elevatorID++)
            {
                elevatorsList.Add(new Elevator(elevatorID.ToString(), "idle", 1));
                //System.Console.WriteLine("created elevator ID " + elevatorID);
            }
        }

        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();
            elevator.addNewRequest(1);
            elevator.move();
            return elevator;
        }

        public Elevator findElevator(int requestedFloor, string requestedDirection)
        {
            Elevator bestElevator = null;
            int bestScore = 6;
            int referenceGap = 100;
            Tuple<Elevator, int, int> bestElevatorInfo;

            if (requestedFloor == 1)
            {
                foreach (Elevator elevator in elevatorsList)
                {
                    int score;
                    if (elevator.currentFloor == 1 && elevator.status == "stopped")
                    {
                        score = 1;                        
                    }
                    else if (elevator.currentFloor == 1 && elevator.status == "idle")
                    {
                        score = 2;
                    }
                    else if (1 > elevator.currentFloor && elevator.direction == "up")
                    {
                        score = 3;
                    }
                    else if (1 < elevator.currentFloor && elevator.direction == "down")
                    {
                        score = 3;
                    }
                    else if (elevator.status == "idle")
                    {
                        score = 4;
                    }
                    else 
                    {
                        score = 5;
                    }
                    bestElevatorInfo = compareElevator(score, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    bestElevator = bestElevatorInfo.Item1;
                    bestScore = bestElevatorInfo.Item2;
                    referenceGap = bestElevatorInfo.Item3;

                }
            }
            else
            {
                foreach (Elevator elevator in elevatorsList)
                {
                    int score;
                    if (elevator.currentFloor == requestedFloor && elevator.status == "stopped" && elevator.direction == requestedDirection)
                    {
                        score = 1;                        
                    }
                    else if (requestedFloor > elevator.currentFloor && elevator.direction == "up" && requestedDirection == "up")
                    {
                        score = 2;
                    }
                    else if (requestedFloor < elevator.currentFloor && elevator.direction == "down" && requestedDirection == "down")
                    {
                        score = 2;
                    }
                    else if (elevator.status == "idle")
                    {
                        score = 4;
                    }
                    else 
                    {
                        score = 5;
                    }
                    bestElevatorInfo = compareElevator(score, elevator, bestScore, referenceGap, bestElevator, requestedFloor);
                    bestElevator = bestElevatorInfo.Item1;
                    bestScore = bestElevatorInfo.Item2;
                    referenceGap = bestElevatorInfo.Item3;

                }
            }
            return bestElevator;
        }

        public Tuple<Elevator, int, int> compareElevator(int scoreToCheck,Elevator currentElevator, int bestScore, int referenceGap, Elevator bestElevator, int requestedFloor)
        {
            if (scoreToCheck < bestScore)
            {
                bestScore = scoreToCheck;
                bestElevator = currentElevator;
                referenceGap = Math.Abs(currentElevator.currentFloor - requestedFloor);
            }
            else if (bestScore == scoreToCheck)
            {
                int gap = Math.Abs(currentElevator.currentFloor - requestedFloor);
                if (referenceGap > gap)
                {
                    bestElevator = currentElevator;
                    referenceGap = gap;
                }
            }
            return Tuple.Create(bestElevator, bestScore, referenceGap);
        }
    }
}