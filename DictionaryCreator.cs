using System.Collections.Generic;
using System.Linq;

namespace Samples.Logic.Emails.Tools
{
    internal class DictionaryCreator
    {
        public static Dictionary<string, object> GetAgent(Agent agent)
        {
            const string prefix = "Agent";

            return new Dictionary<string, object>
            {
                {$"{prefix}.LastName", agent.LastName},
                {$"{prefix}.FirstName", agent.FirstName},
                {$"{prefix}.Name", agent.Name},
                {$"{prefix}.Email", agent.Email},
                {$"{prefix}.Mobile", agent.Mobile},
                {$"{prefix}.NationalRegisterIdNumber", agent.NationalRegisterIdNumber},
                {$"{prefix}.BadgeNumber", agent.BadgeNumber},
            };
        }

        public static Dictionary<string, object> GetFirm(Firm firm)
        {
            const string prefix = "Firm";

            return new Dictionary<string, object>
            {
                {$"{prefix}.Name", firm.Name},
                {$"{prefix}.Address", firm.Address},
                {$"{prefix}.PostalCode", firm.PostalCode},
                {$"{prefix}.City", firm.City},
                {$"{prefix}.Responsible", firm.Responsible},
                {$"{prefix}.Email", firm.Email},
                {$"{prefix}.Phone", firm.Phone},
                {$"{prefix}.UserName", firm.UserName},
            };
        }

        public static Dictionary<string, object> GetRoom(Room room)
        {
            const string prefix = "Room";

            return new Dictionary<string, object>
            {
                {$"{prefix}.Code", room.Code},
                {$"{prefix}.Type", room.Type},
                {$"{prefix}.Zone", room.Zone},
                {$"{prefix}.SubZone", room.SubZone},
                {$"{prefix}.Address", room.Address},
                {$"{prefix}.PostalCode", room.PostalCode},
                {$"{prefix}.City", room.City},
                {$"{prefix}.Comments", room.Comments},
                {$"{prefix}.HasBadgeReader", room.HasBadgeReader},
                {$"{prefix}.IsKeyRequired", room.IsKeyRequired ?? false}
            };
        }

        public static Dictionary<string, object> GetRoomAccess(RoomAccess roomAccess)
        {
            const string prefix = "RoomAccess";

            return new Dictionary<string, object>
            {
                {$"{prefix}.Justification", roomAccess.Justification},
                {$"{prefix}.RefusalReason", roomAccess.RefusalReason},
                {$"{prefix}.AccessStatus", roomAccess.AccessStatus}
            };
        }

        public static Dictionary<string, object> GetIntervention(Intervention intervention)
        {
            const string prefix = "Intervention";

            return new Dictionary<string, object>
            {
                {$"{prefix}.Id", intervention.Id},
                {$"{prefix}.FunctionalId", intervention.FunctionalId},
                {$"{prefix}.Type", intervention.Type},
                {$"{prefix}.Comment", intervention.Comment},
                {$"{prefix}.RefusalReason", intervention.RefusalReason},
                {$"{prefix}.StartDate", intervention.StartDate.AsDdMmmYyyySpaced()},
                {$"{prefix}.StartTime", intervention.StartTime.AsHhMmWithColon()},
                {$"{prefix}.EndDate", intervention.EndDate.AsDdMmmYyyySpaced()},
                {$"{prefix}.EndTime", intervention.EndTime.AsHhMmWithColon()}
            };
        }

        public static Dictionary<string, object> GetAgentAndFirm(Agent agent)
        {
            return new List<Dictionary<string, object>>
                {
                    GetAgent(agent),
                    GetFirm(agent.Firm)
                }
                .SelectMany(dict => dict)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<string, object> GetInterventionRoomAndFirm(Intervention intervention)
        {
            return new List<Dictionary<string, object>>
                {
                    GetIntervention(intervention),
                    GetRoom(intervention.RoomAccess.Room),
                    GetFirm(intervention.Agent.Firm),
                }
                .SelectMany(dict => dict)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static Dictionary<string, object> GetInterventionRoomAccessRoomAgentAndFirm(Intervention intervention)
        {
            return new List<Dictionary<string, object>>
                {
                    GetIntervention(intervention),
                    GetRoomAccess(intervention.RoomAccess),
                    GetRoom(intervention.RoomAccess.Room),
                    GetAgent(intervention.Agent),
                    GetFirm(intervention.Agent.Firm)
                }
                .SelectMany(dict => dict)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
