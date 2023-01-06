﻿namespace CsConsoleGame
{
    internal class Trainer : Worker
    {
        //const
        public Trainer(string name, char focus) :base(name) {
            Focus = focus;
        }

        //meth
        public char Focus { get; } // S = Strength; I = Inteligents; D = Dex; E = Exp

        public byte UseService() {
            Random r = new();

            if (Focus == 'E') return IncreasePlayerExp((byte)r.Next(1, 51));
            else return IncreasePlayerStats((byte)r.Next(1, 51));
        }

        private byte IncreasePlayerStats(byte roll) {
            if (roll == 1) return 0;    // bad training
            else if (roll == 50) return 2;  // perfect training
            else return 1;
        }

        private byte IncreasePlayerExp(byte roll) {
            if (roll == 1) return 10;    // bad training
            else if (roll == 50) return 255;  // perfect training
            else return 100;
        }
    }
}
