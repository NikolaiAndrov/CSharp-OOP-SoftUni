using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            var planetItems = planet.Items.ToList();

            foreach (var astronaut in astronauts)
            {
                for (int i = 0; i < planetItems.Count; i++)
                {
                    if (astronaut.CanBreath)
                    {
                        astronaut.Bag.Items.Add(planetItems[i]);
                        astronaut.Breath();

                        planet.Items.Remove(planetItems[i]);
                        planetItems.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        break;
                    }
                }

                if (planetItems.Count == 0)
                {
                    break;
                }
            }
        }
    }
}
