using System.Collections;
using System.Collections.Generic;
using Web.Users.Models;

namespace Web.Users.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private IList<Purpose> _purposes;

        public IList<Purpose> GetPurposes()
        {
            return CreatePurposes();
        }

        private IList<Purpose> CreatePurposes()
        {
            var meal = new Purpose()
            {
                GroupName = "Meal",
                Options = new List<string>()
                {
                    "Breakfast",
                    "Lunch",
                    "Dinner",
                    "Supper",
                    "Snack",
                    "Drink",
                    "Brunch"
                }
            };

            var activity = new Purpose()
            {
                GroupName = "Activity",
                Options = new List<string>()
                {
                    "Movie",
                    "Sing K",
                    "Games",
                    "Workout"
                }
            };

            var events = new Purpose()
            {
                GroupName = "Event",
                Options = new List<string>()
                {
                    "Wedding",
                    "Songka"
                }
            };

            _purposes = new List<Purpose>();
            _purposes.Add(meal);
            _purposes.Add(activity);
            _purposes.Add(events);

            return _purposes;
        }
    }
}