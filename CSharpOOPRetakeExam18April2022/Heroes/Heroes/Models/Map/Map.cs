using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();

            foreach (IHero hero in players)
            {
                if (hero.GetType().Name == nameof(Knight))
                {
                    knights.Add(hero);
                }
                else if (hero.GetType().Name == nameof(Barbarian))
                {
                    barbarians.Add(hero);
                }
            }

            while (knights.Any(k => k.IsAlive) && barbarians.Any(b => b.IsAlive))
            {
                for (int i = 0; i < knights.Count; i++)
                {
                    for (int j = 0; j < barbarians.Count; j++)
                    {
                        if (knights[i].IsAlive && barbarians[j].IsAlive)
                        {
                            barbarians[j].TakeDamage(knights[i].Weapon.DoDamage());
                        }
                    }
                }

                for (int i = 0; i < barbarians.Count; i++)
                {
                    for (int j = 0; j < knights.Count; j++)
                    {
                        if (barbarians[i].IsAlive && knights[j].IsAlive)
                        {
                            knights[j].TakeDamage(barbarians[i].Weapon.DoDamage());
                        }
                    }
                }
            }

            string outputMessage = string.Empty;

            if (knights.Any(k => k.IsAlive))
            {
                int casualties = knights.Count(k => !k.IsAlive);
                outputMessage = string.Format(OutputMessages.MapFightKnightsWin, casualties);
            }
            else if (barbarians.Any(b => b.IsAlive))
            {
                int casualties = barbarians.Count(b => !b.IsAlive);
                outputMessage = string.Format(OutputMessages.MapFigthBarbariansWin, casualties);
            }

            return outputMessage;
        }
    }
}
