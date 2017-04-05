using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripJetLagAdmin.Models;

namespace TripJetLagAdmin.Data
{
    public class DbInitializer
    {
        public static void Initialize(TripJetLagDBContext context)
        {
            context.Database.EnsureCreated();
   
            // Look for any trips
            if (context.Trips.Any())
            {
                return;   // DB has been seeded
            }

            var airports = new Airport[]
            {
                new Airport {
                    AirportCode ="CLT",
                    AirportName ="Charlotte, NC, USA"
                },
                new Airport {
                    AirportCode ="ORD",
                    AirportName ="Chicago, IL, USA"
                },
                new Airport{
                    AirportCode ="SEA",
                    AirportName ="Seattle, WA, USA"
                },
                new Airport{
                    AirportCode ="LHR",
                    AirportName ="London, England, UK"
                },
                new Airport{
                    AirportCode ="NRT",
                    AirportName ="Tokyo, Japan"
                },
                new Airport{
                    AirportCode ="ANC",
                    AirportName ="Anchorage, AK, USA"
                },
                new Airport{
                    AirportCode ="BER",
                    AirportName ="Berlin, Germany"
                },
                new Airport{
                    AirportCode ="EDI",
                    AirportName ="Edinburgh, Scotland, UK"
                },
                new Airport{
                    AirportCode ="SFO",
                    AirportName ="San Francisco, CA, USA"
                }
            };

            foreach (Airport ap in airports)
            {
                context.Airports.Add(ap);
            }
            context.SaveChanges();

            var travelers = new Traveler[]
            {
               new Traveler{
                   FirstName ="Example1",
                   LastName ="Traveler1"
               },
               new Traveler{
                   FirstName ="Example2",
                   LastName ="Traveler2"
               },
            };
            foreach (Traveler tv in travelers)
            {
                context.Travelers.Add(tv);
            }
            context.SaveChanges();

            var trips = new Trip[]
            {
                new Trip{
                    TravelerId =1
                },
                new Trip{
                    TravelerId =2
                },
                new Trip{
                    TravelerId =2
                },
            };

            foreach (Trip t in trips)
            {
                context.Trips.Add(t);
            }
            context.SaveChanges();

            var tripLegs = new TripLeg[]
            {
                new TripLeg{
                    TripId =1,
                    Segment =1,
                    DepartureAirportCode ="CLT",
                    DepartureDate =DateTime.Parse("2017-03-05 09:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-05 12:15:00"),
                    ArrivalAirportCode ="ORD"},
                new TripLeg{
                    TripId =1,
                    Segment =2,
                    DepartureAirportCode ="ORD",
                    DepartureDate =DateTime.Parse("2017-03-05 15:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-05 12:15:00"),
                    ArrivalAirportCode ="NRT"},
                new TripLeg{
                    TripId =1,
                    Segment =3,
                    DepartureAirportCode ="NRT",
                    DepartureDate =DateTime.Parse("2017-03-05 17:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-05 12:15:00"),
                    ArrivalAirportCode ="ORD"},
                new TripLeg{
                    TripId =1,
                    Segment =4,
                    DepartureAirportCode ="ORD",
                    DepartureDate =DateTime.Parse("2017-03-05 18:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-06 12:15:00"),
                    ArrivalAirportCode ="CLT"
                },
                new TripLeg{
                    TripId =2,
                    Segment =3,
                    DepartureAirportCode ="LHR",
                    DepartureDate =DateTime.Parse("2017-02-05 19:15:00"),
                    ArrivalDate=DateTime.Parse("2017-02-06 12:15:00"),
                    ArrivalAirportCode ="BER"
                },
                new TripLeg{
                    TripId =2,
                    Segment =2,
                    DepartureAirportCode ="SEA",
                    DepartureDate =DateTime.Parse("2017-02-05 09:15:00"),
                    ArrivalDate=DateTime.Parse("2017-02-06 12:15:00"),
                    ArrivalAirportCode ="LHR"
                },
                new TripLeg{
                    TripId =2,
                    Segment =1,
                    DepartureAirportCode ="ANC",
                    DepartureDate =DateTime.Parse("2017-02-03 12:15:00"),
                    ArrivalDate=DateTime.Parse("2017-02-04 12:15:00"),
                    ArrivalAirportCode ="SEA"
                },
                new TripLeg{
                    TripId =3,
                    Segment =1,
                    DepartureAirportCode ="SFO",
                    DepartureDate =DateTime.Parse("2017-03-08 12:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-09 12:15:00"),
                    ArrivalAirportCode ="EDI"
                },
                new TripLeg{
                    TripId =3,
                    Segment =2,
                    DepartureAirportCode ="EDI",
                    DepartureDate =DateTime.Parse("2017-03-08 12:15:00"),
                    ArrivalDate=DateTime.Parse("2017-03-09 12:15:00"),
                    ArrivalAirportCode ="SFO"
                },

            };

            foreach (TripLeg tl in tripLegs)
            {
                context.TripLegs.Add(tl);
            }
            context.SaveChanges();

            var legNotes = new LegNote[]
            {
                new LegNote{
                    TripLegId =1
                },
                new LegNote{
                    TripLegId =2,
                    Note ="On your flight from Chicago to Tokyo, go to sleep as soon as you can after you get on the plane in Chicago.At 8:00pm Chicago Time, it is already 2:00am in Tokyo." +
                       "You can sleep in until shortly before your arrival on the Chicago to Tokyo flight especially if you did not get to sleep immediately when you boarded the flight in Chicago." +
                       "Do not nap after you arrive in Tokyo, but stay awake until after supper in Tokyo.Early to Bed is ok.",
                    NoteRetrieved=DateTime.Parse("2017-03-05 08:15:00"),
                    ReadyToDeliver =true
                },
                new LegNote{
                    TripLegId =3
                },
                new LegNote{
                    TripLegId =4
                },
                new LegNote{
                    TripLegId =5
                },

                new LegNote{
                    TripLegId =6,
                    Note ="On your flight from Anchorage to Berlin, go to sleep as soon as you can after you get on the plane in Chicago.At 8:00pm Chicago Time, it is already 2:00am in Berlin." +
                       "You can sleep in until shortly before your arrival on the Anchorage to Berlin flight especially if you did not get to sleep immediately when you boarded the flight in Anchorage." +
                       "Do not nap after you arrive in Berlin, but stay awake until after supper in Berlin.Early to Bed is ok.",
                    NoteRetrieved=DateTime.Parse("2017-03-08 09:15:00"),
                    ReadyToDeliver =true
                },
                new LegNote{
                    TripLegId =7
                },
                new LegNote{
                    TripLegId =8,
                    Note ="Once you wake up to catch your flight in San Francisco, try to stay awake " +
                       "until you get on the plane New York to fly to Edinburgh.  On your flight from" +
                       "from New York to Edinburgh, go to sleep as soon as you can after...",
                    NoteRetrieved=DateTime.Parse("2017-03-08 09:15:00"),
                    ReadyToDeliver =true
                },
                new LegNote{
                    TripLegId =9,
                    Note ="You will have to get up early (most likely 5 to 6 am) to catch your 9:25am" +
                       "flight to New York.  It is ok to have one cup of a caffeninated beverage " +
                       "if you do get up that early.  Any caffeine after 6am should be avoided as you want...",
                    NoteRetrieved=DateTime.Parse("2017-03-08 09:15:00"),
                    ReadyToDeliver =true
                },

            };

            foreach (LegNote ln in legNotes)
            {
                context.LegNotes.Add(ln);
            }
            context.SaveChanges();
        }
    }
}

