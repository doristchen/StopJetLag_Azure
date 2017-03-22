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
                new Airport {AirportCode="CLT", AirportName="Charlotte, NC, USA" },
                new Airport {AirportCode="ORD", AirportName="Chicago, IL, USA" },
                new Airport {AirportCode="SEA", AirportName="Seattle, WA, USA" },
                new Airport {AirportCode="LHR", AirportName="London, England, UK" },
                new Airport {AirportCode="NRT", AirportName="Tokyo, Japan"},
                new Airport {AirportCode="ANC", AirportName="Anchorage, AK, USA" },
                new Airport {AirportCode="BER", AirportName="Berlin, Germany"}

            };

            foreach (Airport ap in airports)
            {
                context.Airports.Add(ap);
            }
            context.SaveChanges();

            var travelers = new Traveler[]
            {
               new Traveler{FirstName="Example1",LastName="Traveler1"},
               new Traveler{FirstName="Example2",LastName="Traveler2"},
               

            };
            foreach (Traveler tv in travelers)
            {
                context.Travelers.Add(tv);
            }
            context.SaveChanges();

            var legNotes = new LegNote[]
            {
                //new LegNote {Note = "There are no additional trip notes for this leg of your trip" },
                new LegNote { },
                new LegNote {Note ="On your flight from Chicago to Tokyo, go to sleep as soon as you can after you get on the plane in Chicago.At 8:00pm Chicago Time, it is already 2:00am in Tokyo." +
                       "You can sleep in until shortly before your arrival on the Chicago to Tokyo flight especially if you did not get to sleep immediately when you boarded the flight in Chicago." +
                       "Do not nap after you arrive in Tokyo, but stay awake until after supper in Tokyo.Early to Bed is ok.",
                       NoteRetrieved=DateTime.Parse("2017-03-05 08:15:00"),ReadyToDeliver=true},
                new LegNote { }, new LegNote { }, new LegNote { },
                new Models.LegNote {Note ="On your flight from Chicago to Tokyo, go to sleep as soon as you can after you get on the plane in Chicago.At 8:00pm Chicago Time, it is already 2:00am in Tokyo." +
                       "You can sleep in until shortly before your arrival on the Chicago to Tokyo flight especially if you did not get to sleep immediately when you boarded the flight in Chicago." +
                       "Do not nap after you arrive in Tokyo, but stay awake until after supper in Tokyo.Early to Bed is ok.",
                 NoteRetrieved=DateTime.Parse("2017-03-08 09:15:00"), ReadyToDeliver=true},
                new LegNote { },

            };

            foreach (LegNote ln in legNotes)
            {
                context.LegNotes.Add(ln);
            }
            context.SaveChanges();

            var trips = new Trip[]
            {
                new Trip{TravelerId=1 },
                new Trip{TravelerId=2 },
            };


            foreach (Trip t in trips)
            {
                context.Trips.Add(t);
            }
            context.SaveChanges();

            var tripLegs = new TripLeg[]
            {
                new TripLeg{TripId=1, Segment=1,DepartureAirportCode="CLT", DepartureDate=DateTime.Parse("2017-03-05 09:15:00"),
                 ArrivalAirportCode ="ORD", NoteId=1},

                new TripLeg{TripId=1, Segment=2,DepartureAirportCode="ORD",
                 ArrivalAirportCode ="NRT" , NoteId=2},
                new TripLeg{TripId=1, Segment=3,DepartureAirportCode="NRT",
                 ArrivalAirportCode ="ORD", NoteId=3 },
                new TripLeg{TripId=1, Segment=4,DepartureAirportCode="ORD",
                 ArrivalAirportCode ="CLT", NoteId=4 },
                new TripLeg{TripId=2, Segment=3,DepartureAirportCode="LHR",
                 ArrivalAirportCode ="BER", NoteId=7},
                new TripLeg{TripId=2, Segment=2,DepartureAirportCode="SEA",
                 ArrivalAirportCode ="LHR", NoteId=6 },
                new TripLeg{TripId=2, Segment=1,DepartureAirportCode="ANC", DepartureDate=DateTime.Parse("2017-03-08 12:15:00"),
                 ArrivalAirportCode ="SEA", NoteId=5},

            };

            foreach (TripLeg tl in tripLegs)
            {
                context.TripLegs.Add(tl);
            }
            context.SaveChanges();

        }
    }
}

