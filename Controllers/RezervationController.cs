using Microsoft.AspNetCore.Mvc;
using TrainRezervationProject.Model;

namespace TrainRezervationProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervationController : ControllerBase
    {
        public Rezervation RezList
        {
            get
            {
                Rezervation rez = new Rezervation();
                rez.Train = new Train()
                {
                    Name = "Baskent",
                    Vagons = new Vagon[3]
                };
                rez.Train.Vagons[0] = new Vagon() { Name = "Vagon 1", Capacity = 100, FullSeatQuantity = 50 };
                rez.Train.Vagons[1] = new Vagon() { Name = "Vagon 2", Capacity = 90, FullSeatQuantity = 80 };
                rez.Train.Vagons[2] = new Vagon() { Name = "Vagon 3", Capacity = 80, FullSeatQuantity = 80 };

                return rez;
            }
        }


        [HttpGet]
        public Rezervation Get()
        {
            return RezList;
        }

        [Route("rez")]
        [HttpPost]
        public RezervationResult Post(Rezervation rezervation)
        {
            //count of Vagon
            int vagonCount = rezervation.Train.Vagons.Length;
            int[] emptySeat = new int[vagonCount];
            for (int i = 0; i <= vagonCount - 1; i++)
            {
                emptySeat[i] = (int)Math.Abs(0.7 * (rezervation.Train.Vagons[i].Capacity - rezervation.Train.Vagons[i].FullSeatQuantity));
            }

            //Total Empty Seat
            int totalEmptySeat = emptySeat.Sum();

            RezervationResult RezervationResult = new RezervationResult();

            if (rezervation.RezervationLimit <= totalEmptySeat)
            {
                RezervationResult.CanBooked = true;
                if (rezervation.DifferentVagonAssignment == true)
                {
                    if (rezervation.RezervationLimit <= emptySeat[0])
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[1];
                        RezervationResult.DetailOfSeats[0] = new DetailOfSeats
                        {
                            VagonName = rezervation.Train.Vagons[0].Name,
                            PassengerCount = rezervation.RezervationLimit
                        };
                    }
                    if (rezervation.RezervationLimit > emptySeat[0] && rezervation.RezervationLimit <= (emptySeat[0] + emptySeat[1]))
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[2];
                        for (int i = 0; i <= 1; i++)
                        {
                            RezervationResult.DetailOfSeats[i] = new DetailOfSeats();
                        }
                        RezervationResult.DetailOfSeats[0].VagonName = rezervation.Train.Vagons[0].Name;
                        RezervationResult.DetailOfSeats[0].PassengerCount = emptySeat[0];
                        RezervationResult.DetailOfSeats[1].VagonName = rezervation.Train.Vagons[1].Name;
                        RezervationResult.DetailOfSeats[1].PassengerCount = rezervation.RezervationLimit - emptySeat[0];
                    }
                    if (rezervation.RezervationLimit > (emptySeat[0] + emptySeat[1]) && rezervation.RezervationLimit <= (totalEmptySeat))
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[3];
                        for (int i = 0; i <= 2; i++)
                        {
                            RezervationResult.DetailOfSeats[i] = new DetailOfSeats();
                        }
                        RezervationResult.DetailOfSeats[0].VagonName = rezervation.Train.Vagons[0].Name;
                        RezervationResult.DetailOfSeats[0].PassengerCount = emptySeat[0];
                        RezervationResult.DetailOfSeats[1].VagonName = rezervation.Train.Vagons[1].Name;
                        RezervationResult.DetailOfSeats[1].PassengerCount = emptySeat[1];
                        RezervationResult.DetailOfSeats[2].VagonName = rezervation.Train.Vagons[2].Name;
                        RezervationResult.DetailOfSeats[2].PassengerCount = rezervation.RezervationLimit - emptySeat[0] - emptySeat[1];
                    }
                }
                if (rezervation.DifferentVagonAssignment != true)
                {
                    if (rezervation.RezervationLimit <= emptySeat[0])
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[1];
                        RezervationResult.DetailOfSeats[0] = new DetailOfSeats
                        {
                            VagonName = rezervation.Train.Vagons[0].Name,
                            PassengerCount = rezervation.RezervationLimit
                        };
                    }
                    else if (rezervation.RezervationLimit <= emptySeat[1])
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[1];
                        RezervationResult.DetailOfSeats[0] = new DetailOfSeats
                        {
                            VagonName = rezervation.Train.Vagons[1].Name,
                            PassengerCount = rezervation.RezervationLimit
                        };
                    }
                    else if (rezervation.RezervationLimit <= emptySeat[2])
                    {
                        RezervationResult.DetailOfSeats = new DetailOfSeats[1];
                        RezervationResult.DetailOfSeats[0] = new DetailOfSeats
                        {
                            VagonName = rezervation.Train.Vagons[2].Name,
                            PassengerCount = rezervation.RezervationLimit
                        };
                    }
                    else
                    {
                        RezervationResult.CanBooked = false;
                        RezervationResult.DetailOfSeats = new DetailOfSeats[0];
                    }
                }
            }
            else
            {
                RezervationResult.CanBooked = false;
                RezervationResult.DetailOfSeats = new DetailOfSeats[0];
            }
            return RezervationResult;
        }
    }
}