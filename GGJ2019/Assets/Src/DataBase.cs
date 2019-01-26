using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour {
    public struct DB {
        public DB(
            string title,
            string message,
            string Option1,
            int Option1_Scrap,
            int Option1_Energy,
            int Option1_Peaple,
            string Option2,
            int Option2_Scrap,
            int Option2_Energy,
            int Option2_Peaple,
            string Option3,
            int Option3_Scrap,
            int Option3_Energy,
            int Option3_Peaple
            ) {
            this.title = title;
            this.message = message;
            this.Option1 = Option1;
            this.Option1_Energy = Option1_Energy;
            this.Option1_Peaple = Option1_Peaple;
            this.Option1_Scrap = Option1_Scrap;
            this.Option2 = Option2;
            this.Option2_Energy = Option2_Energy;
            this.Option2_Peaple = Option2_Peaple;
            this.Option2_Scrap = Option2_Scrap;
            this.Option3 = Option3;
            this.Option3_Energy = Option3_Energy;
            this.Option3_Peaple = Option3_Peaple;
            this.Option3_Scrap = Option3_Scrap;
        }

        public string title;
        public string message;
        public string Option1;
        public int Option1_Scrap;
        public int Option1_Energy;
        public int Option1_Peaple;
        public string Option2;
        public int Option2_Scrap;
        public int Option2_Energy;
        public int Option2_Peaple;
        public string Option3;
        public int Option3_Scrap;
        public int Option3_Energy;
        public int Option3_Peaple;
    }

    public List<DB> DATA = new List<DB>();

    public void Init() {

        DATA.Add(new DB(
            "Discharge Distress",
            "Waves of seething energy from the Cataclysm are dangerously near. " +
            "Adjusting your flight path to skim across the waves could increase your " +
            "energy reserves significantly. As you ponder, the comms suddenly light up with an audio message - " +
            "*This is an official request for assistance in the evacuation of our planet, there's no one else close enough. Please help us* ",

            "Help the evacuation", 0, 0, 1, // Scrap, Energy, People
            "Siphon the energy", 0, 100, 0, // Scrap, Energy, People
            "Ignore", 0, 0, 0  // Scrap, Energy, People
            ));

        DATA.Add(new DB(
            "Generator Scrap",
            "Wrecked ships and ancient hulls litter the space around you. ",

            "Scavange for scrap", 100, 0, 0, // Scrap, Energy, People
            "Scavange for energy", 0, 100, 0, // Scrap, Energy, People
            "Scavange for both", 50, 50, 0  // Scrap, Energy, People
            ));

        DATA.Add(new DB(
            "Burning Crater",
            "A crashed trader has managed to turn the burning crater of their crash landing into " +
            "a venerable trading post, but the Cataclysm has driven people away. The trader requests help. ",

            "Evacuate the trader ", 0, 0, 1, // Scrap, Energy, People
            "Take the wrecked trading ship for extra power", 0, 100, 0, // Scrap, Energy, People
            "Take the unused areas appart for scrap", 100, 0, 0  // Scrap, Energy, People
            ));

        DATA.Add(new DB(
            "Floating Tools",
            "Upon scanning the surface, tools and equipment appear to be hovering above the ground. " +
            "Though there seems to be no sign of life, there is hazardous material floating as well.",

            "Send a team to retrieve scrap  ",200, 0, -1, // Scrap, Energy, People
            "Syphon energy from the anti gravity field", 0, 100, 0, // Scrap, Energy, People
            "Leave without risk", 0, 0, 0  // Scrap, Energy, People
            ));

        DATA.Add(new DB(
            "Kharernian Nomads",
            "Kharernian Nomads are leaving their temporary post to escape the Cataclysm, and offer supplies.",

            "Take their spare energy", 0, 50, 1, // Scrap, Energy, People
            "Take their spare scrap", 50, 0, 0, // Scrap, Energy, People
            "Offer to take some of them faster", 0, 0, 3  // Scrap, Energy, People
            ));
    }
}
