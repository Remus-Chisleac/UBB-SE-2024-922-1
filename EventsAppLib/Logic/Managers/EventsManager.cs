namespace EventsApp.Logic.Managers
{
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;
    public static class EventsManager
    {
        private static DataAdapter<Entities.EventInfo> eventsAdapter;
        private static DataAdapter<UserEventRelationInfo> userEventRelationsAdapter;

        public static void Initialize(DataAdapter<Entities.EventInfo> adapter, DataAdapter<UserEventRelationInfo> userEventRelationsAdapter)
        {
            eventsAdapter = adapter;
            EventsManager.userEventRelationsAdapter = userEventRelationsAdapter;
        }

        public static Entities.EventInfo GetEvent(Guid eventId)
        {
            Entities.EventInfo eventInfo = new Entities.EventInfo(eventId);
            return eventsAdapter.Get(eventInfo.GetIdentifier());
        }

        public static List<Entities.EventInfo> GetAllEvents()
        {
            return eventsAdapter.GetAll();
        }

        /// <summary>
        /// Returns true if the current date is between the startDate and endDate of the event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsEventActive(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            DateTime startDate = eventInfo.StartDate;
            DateTime endDate = eventInfo.EndDate;
            DateTime today = DateTime.Now;
            if (today >= startDate && today <= endDate)
            {
                return true;
            }

            return false;
        }

        public static bool IsOrganizer(Guid userId, Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            if (eventInfo.OrganizerGUID == userId)
            {
                return true;
            }

            return false;
        }

        public static void AddNewEvent(Entities.EventInfo eventInfo)
        {
            eventsAdapter.Add(eventInfo);
        }

        public static void UpdateEvent(Guid eventId, Entities.EventInfo eventInfo)
        {
            Entities.EventInfo currentEvent = GetEvent(eventId);
            eventsAdapter.Update(currentEvent.GetIdentifier(), eventInfo);
        }

        public static void DeleteEvent(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            eventsAdapter.Delete(eventInfo.GetIdentifier());
        }

        /// <summary>
        /// Returns true if the current date is after the endDate of the event
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsEventOver(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            DateTime endDate = eventInfo.EndDate;
            DateTime today = DateTime.Now;
            if (today > endDate)
            {
                return true;
            }

            return false;
        }

        public static List<Entities.EventInfo> FilterEvents(EventFilter filter)
        {
            List<Entities.EventInfo> filteredEvents = GetAllEvents();
            // If something is null ignore that filter
            // Ex: If name is "" ignore the name filter
            if (filter.Name != null)
            {
                filteredEvents = filteredEvents.FindAll(c => c.EventName.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.MaxFee != 0)
            {
                filteredEvents = filteredEvents.FindAll(e => e.EntryFee <= filter.MaxFee);
            }

            if (filter.StartDate != null)
            {
                filteredEvents = filteredEvents.FindAll(e => e.StartDate >= filter.StartDate);
            }

            if (filter.Categories != null)
            {
                filteredEvents = filteredEvents.FindAll(e =>
                {
                    List<string> presentCategories = e.Categories.Split(',').ToList();
                    foreach (string category in filter.Categories)
                    {
                        if (presentCategories.Contains(category))
                        {
                            return true;
                        }
                    }

                    return false;
                });
            }

            return filteredEvents;
        }

        public static List<UserInfo> GetInterestedUsers(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            List<UserEventRelationInfo> userEventRelationInfos = userEventRelationsAdapter.GetAll();
            List<UserInfo> users = new List<UserInfo>();
            foreach (UserEventRelationInfo relationInfo in userEventRelationInfos)
            {
                if (relationInfo.EventGUID == eventId && relationInfo.Status == "interested")
                {
                    UserInfo user = UsersManager.GetUser(relationInfo.UserGUID);
                    users.Add(user);
                }
            }

            return users;
        }

        public static List<UserInfo> GetGoingUsers(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            List<UserEventRelationInfo> userEventRelationInfos = userEventRelationsAdapter.GetAll();
            List<UserInfo> users = new List<UserInfo>();
            foreach (UserEventRelationInfo relationInfo in userEventRelationInfos)
            {
                if (relationInfo.EventGUID == eventId && relationInfo.Status == "going")
                {
                    UserInfo user = UsersManager.GetUser(relationInfo.UserGUID);
                    users.Add(user);
                }
            }

            return users;
        }

        public static UserInfo GetEventOrganizer(Guid eventId)
        {
            Entities.EventInfo eventInfo = GetEvent(eventId);
            return UsersManager.GetUser(eventInfo.OrganizerGUID);
        }

        public static int GetNumberOfParticipants(Guid eventId)
        {
            return GetGoingUsers(eventId).Count;
        }

        public static float GetTotalDonationAmount(Guid eventId)
        {
            float totalDonationAmount = 0;
            List<DonationInfo> donations = DonationsManager.GetAllDonationsForEvent(eventId);
            foreach (DonationInfo donation in donations)
            {
                totalDonationAmount += donation.Amount;
            }

            return totalDonationAmount;
        }

        public static List<Entities.EventInfo> GetEventsOfUser(Guid userId)
        {
            List<Entities.EventInfo> events = GetAllEvents();
            List<Entities.EventInfo> eventsForUser = new List<Entities.EventInfo>();
            foreach (Entities.EventInfo eventInfo in events)
            {
                if (eventInfo.OrganizerGUID == userId)
                {
                    eventsForUser.Add(eventInfo);
                }
            }

            return eventsForUser;
        }

        public static List<Entities.EventInfo> SortEventsByPopularityAscending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => GetNumberOfParticipants(firstEvent.GUID).CompareTo(GetNumberOfParticipants(secondEvent.GUID)));
            return events;
        }

        public static List<Entities.EventInfo> SortEventsByPopularityDescending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => GetNumberOfParticipants(secondEvent.GUID).CompareTo(GetNumberOfParticipants(firstEvent.GUID)));
            return events;
        }

        public static List<Entities.EventInfo> SortEventsByDateAscending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => firstEvent.StartDate.CompareTo(secondEvent.StartDate));
            return events;
        }

        public static List<Entities.EventInfo> SortEventsByDateDescending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => secondEvent.StartDate.CompareTo(firstEvent.StartDate));
            return events;
        }

        public static List<Entities.EventInfo> SortEventsByPriceAscending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => firstEvent.EntryFee.CompareTo(secondEvent.EntryFee));
            return events;
        }

        public static List<Entities.EventInfo> SortEventsByPriceDescending()
        {
            List<Entities.EventInfo> events = GetAllEvents();
            events.Sort((firstEvent, secondEvent) => secondEvent.EntryFee.CompareTo(firstEvent.EntryFee));
            return events;
        }

        public struct EventFilter(string name, float maxFee, DateTime startDate, DateTime endDate, List<string> categories)
        {
            public string Name = name;
            public float MaxFee = maxFee;
            public DateTime StartDate = startDate;
            public DateTime EndDate = endDate;
            public List<string> Categories = categories;
        }

        public static List<Entities.EventInfo> SearchEventByName(string nameString)
        {
            List<Entities.EventInfo> allEvents = GetAllEvents();
            List<Entities.EventInfo> filteredEvents = new List<Entities.EventInfo>();
            filteredEvents = allEvents.FindAll(eventItem => eventItem.EventName.ToLower().Contains(nameString.ToLower()));
            return filteredEvents;
        }

        public static List<Entities.EventInfo> SearchEventByLocation(string locationString)
        {
            List<Entities.EventInfo> allEvents = GetAllEvents();
            List<Entities.EventInfo> filteredEvents = new List<Entities.EventInfo>();
            filteredEvents = allEvents.FindAll(eventItem => eventItem.Location.ToLower().Contains(locationString.ToLower()));
            return filteredEvents;
        }

        private static bool ProcessPayment(string cardHolderName, string cardNumber, string cvv, DateTime expirationDate)
        {
            string correctCardHolderName = cardHolderName; // get this from database
            string correctCardNumber = cardNumber; // get this from database
            string correctCvv = cvv; // get this from database
            DateTime correctExpirationDate = expirationDate; // get this from database
            return true;
            // if (correctExpirationDate.Date < DateTime.Now.Date || correctCardHolderName != cardHolderName || correctCardNumber != cardNumber || correctCvv != cvv || correctExpirationDate.Date != expirationDate.Date)
            // {
            //    return false;
            // }
            // return true;
        }

        public static void BuyTicket(Guid userId, Guid eventId, string cardHolderName, string cardNumber, string cvv, DateTime expirationDate)
        {
            if (ProcessPayment(cardHolderName, cardNumber, cvv, expirationDate))
            {
                UsersManager.SetGoingStatus(userId, eventId);
            }
        }
    }
}
