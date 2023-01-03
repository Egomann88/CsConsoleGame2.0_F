﻿// See https://aka.ms/new-console-template for more information
using CsConsoleGame;

/// <summary>
/// Check and returns if Character is alive.<br />
/// If not, character save file will be deleted
/// </summary>
/// <param name="p">character</param>
/// <returns>true, if yes - false, if not</returns>
static bool PlayerAlive(Player p) {
    if (p.Health[0] <= 0) {
        Player.HasPlayers();
        return false;
    }

    return true;
}

/// <summary>
/// Generates rnd Enemy and let it be fought by the player
/// </summary>
/// <param name="p"></param>
/// <returns>Player with new stats</returns>
static Player Dungeon(Player p) {
    Random r = new();
    byte rnd = 0;

    if (p.Lvl < 3) rnd = Convert.ToByte(r.Next(1, 3)); // only picks the easy enemies
    else if (p.Lvl < 5) rnd = Convert.ToByte(r.Next(1, 6)); // only picks the easy enemies
    else if (p.Lvl < 8) rnd = Convert.ToByte(r.Next(3, 8)); // only picks the easy enemies
    else if (p.Lvl < 10) rnd = Convert.ToByte(r.Next(3, 12)); // only picks the easy enemies
    else {
        rnd = Convert.ToByte(r.Next(1, 101));

        if (rnd <= 4) rnd = 5; // 4 %
        else if (rnd <= 10) rnd = 6; // 6 %
        else if (rnd <= 20) rnd = 7; // 10 %
        else if (rnd <= 33) rnd = 8; // 13 %
        else if (rnd <= 45) rnd = 9; // 12 %
        else if (rnd <= 55) rnd = 10; // 10 %
        else if (rnd <= 67) rnd = 11; // 12 %
        else if (rnd <= 83) rnd = 12; // 16 %
        else rnd = 13; // 17 %
    }

    Enemy e = new Enemy(p.Lvl, rnd, false); // generate enemy
    Fight f = new Fight(p, e);  // generate fight

    return f.FightIn();
}

static Player MainMenu() {
    string mainMenuText = "1) Charakter erstellen\n";

    if (Player.HasPlayers()) mainMenuText += "2) Charakter laden\n3) Charakter löschen\n";

    while (true) {
        Console.Clear();
        Console.WriteLine("Hauptmenü\n{0}9) Spiel beenden", mainMenuText);
        char input = Console.ReadKey(true).KeyChar;
        Console.Clear();

        switch (input) {
            case '1': return Player.CreatePlayer();
            case '2':
                if (Player.HasPlayers()) return Player.GetPlayers(false);
                else break;
            case '3':
                if (Player.HasPlayers()) Player.GetPlayers(true);
                break;
            case '9': Environment.Exit(-1); break;
        }
    }
}

// main()

char input = '0';
bool chAlive = false;
Player player;
Marketplace marketplace;

do {
    player = MainMenu();

    chAlive = true;
    marketplace = new Marketplace(player);

    do {
        Console.Clear();
        Console.WriteLine("{0}, bei Ihnen liegt die Wahl.", player.Name);
        Console.WriteLine("1) Dungeon\n2) Charakter betrachten\n3) Marktplatz\n6) Charakter umbenennen\n" +
          "7) Charakter speichern\n8) Zurück zum Hauptmenü\n9) Spiel beenden");
        input = Console.ReadKey(true).KeyChar;

        switch (input) {
            case '1':
                player = Dungeon(player);
                chAlive = PlayerAlive(player);
                break;
            case '2': player.ShowPlayer(); continue;    // continiue skips autosave
            case '3': player = marketplace.OnMarket(); break;
            //case '4': player.Gold += 9999; player.Lvl += 9; player.Strength += 80; break;
            case '6': player.Name = Player.ChangeName(); break;
            case '7': Player.SavePlayer(player); continue;  // call sensitive methods with classname
            case '8': chAlive = false; continue;
            case '9':
                do {
                    Console.WriteLine("Charakter speichern? [j/n]");
                    input = Console.ReadKey(true).KeyChar;
                } while (input != 'j' && input != 'n');

                if (input == 'j') Player.SavePlayer(player);  // call sensitive methods with classname
                Environment.Exit(0); // stops appligation

                continue;
            default: break; // nothing happens
        }
        Player.SavePlayer(player); // autosave
    } while (chAlive);  // if char still alive, start new opt
} while (true);
