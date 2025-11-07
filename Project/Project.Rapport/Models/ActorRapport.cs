using Project.Models;

namespace Project.Rapport.Models
{
    public static class ActorRapport
    {
     
        public static List<Actor>? SortActorsByPopularity(List<Actor> actors, Func<Actor,bool>? predicate)
        {
            List<Actor> sortedActors = [..actors.OrderBy(a => a.Popularity)];

            if (predicate != null)
            {
                foreach (Actor actor in sortedActors)
                {
                    if(!predicate(actor))
                    {
                        sortedActors.Remove(actor);
                    }
                }
            }

            return sortedActors;
        }

        public static string GetActorsNationalityRapport(List<Actor> actors)
        {
            var nationalitiesRaport = actors.GroupBy(a => a.Nationality).Select(a => new
            {
                Name = a.Key,
                Count = a.Count(),
            });

            string rapportResult = string.Empty;

            foreach (var nationality in nationalitiesRaport)
            {
                rapportResult += $"{nationality.Name}: {nationality.Count} \n";
            }

            return rapportResult;
        }

        public static string GetActorsAgeRapport(List<Actor> actors)
        {
            var ageRapport = actors.GroupBy(a => a.Age).Select(a => new
            {
                Name = a.Key,
                Count = a.Count(),
            });

            string rapportResult = string.Empty;

            foreach (var ageInfo in ageRapport)
            {
                rapportResult += $"{ageInfo.Name}: {ageInfo.Count} \n";
            }

            return rapportResult;
        }
    }
}
