using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheat
{
    class Game
    {
        public static void HandleTurn(Deck[] playerDecks, Deck discardDeck)
        {
            Display.ScrollMenu(playerDecks[0]);

            playerDecks[0].ResetSelectedCards();
        }

    }
}