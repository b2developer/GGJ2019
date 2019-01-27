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
        //1
        DATA.Add(new DB(
            "Discharge Distress",
            "Waves of seething energy from the Cataclysm are dangerously near. " +
            "Adjusting your flight path to skim across the waves could increase your " +
            "energy reserves significantly. As you ponder, the comms suddenly light up with an audio message - " +
            "*This is an official request for assistance in the evacuation of our planet, there's no one else close enough. Please help us* ",

            "Help the evacuation", 0, 0, 1, 
            "Siphon the energy", 0, 100, 0, 
            "Ignore", 0, 0, 0  
            ));
        //2
        DATA.Add(new DB(
            "Generator Scrap",
            "Wrecked ships and ancient hulls litter the space around you. ",

            "Scavange for scrap", 100, 0, 0,
            "Scavange for energy", 0, 100, 0,
            "Scavange for both", 50, 50, 0
            ));
        //3
        DATA.Add(new DB(
            "Burning Crater",
            "A crashed trader has managed to turn the burning crater of their crash landing into " +
            "a venerable trading post, but the Cataclysm has driven people away. The trader requests help. ",

            "Evacuate the trader ", 0, 0, 1, 
            "Take the wrecked trading ship for extra power", 0, 100, 0, 
            "Take the unused areas appart for scrap", 100, 0, 0 
            ));
        //4
        DATA.Add(new DB(
            "Floating Tools",
            "Upon scanning the surface, tools and equipment appear to be hovering above the ground. " +
            "Though there seems to be no sign of life, there is hazardous material floating as well.",

            "Send a team to retrieve scrap  ",200, 0, -1, 
            "Syphon energy from the anti gravity field", 0, 100, 0, 
            "Leave without risk", 0, 0, 0  
            ));
        //5
        DATA.Add(new DB(
            "Kharernian Nomads",
            "Kharernian Nomads are leaving their temporary post to escape the Cataclysm, and offer supplies.",

            "Take their spare energy", 0, 50, 1, 
            "Take their spare scrap", 50, 0, 0, 
            "Offer to take some of them faster", 0, 0, 3  
            ));
        //6
        DATA.Add(new DB(
            "Destablized planet",
            "the core of this planet is destablized and starting to break the planet up. we could try and rescue " +
            "the residentds here, collect the metals being poured out on the surface or try and collect some energy",

            "Rescue the residents", 0,5,0,
            "Collect the metals", 100,0,0,
            "Collect energy", 0,100,0
            ));
        //7
        DATA.Add(new DB(
            "Terror face",
            "you come accrose a civillian caraven however when you contact them they are so terrafied of your face they imidiatly offer you anything of your choice",

            "Take scrap from them", 100,0,0,
            "Take energy from them", 0,100,0,
            "Take a person from them", 0,0,1
            ));
        //8
        DATA.Add(new DB(
            "wasing of the wanting",
            "you come acrose an old town located on the planet and are contacted by the locals. 'wasin been wanting for the moving or the fortin, wasin comin of the flein and scared'",

            "wasin the wanting of the moving", 0,100,0,
            "wasin the wanting of the fortin", 100,0,0,
            "wasin flein and the scare be takin", 0,0,2
            ));
        //9
        DATA.Add(new DB(
            "Dimensions",
            "as you arrive you recive a strange comunication... from your self! apparently this is an ulternative version of you about to fall to the cataclisim, you can spend some energy to send " +
            "over some scrap or people",

            "Send over scrap", 150,-25,0,
            "Send over people", 0,-25,5,
            "Ignore", 0,0,0
            ));
        //10
        DATA.Add(new DB(
            "Pirates!",
            "as you exit from ftl you are suddenly jumped on by a bunch of pirates! you can try and board them and fight them off or spend energy, alternativly you can just bribe them with a lot of scrap",

            "Board them!", 200, 0, -2,
            "Fire all guns!", 100, -50, 0,
            "Bribe them", -100, 0, 0
            ));
        //11
        DATA.Add(new DB(
            "Pushing",
            "we have been trying to push through a misterious cloud of space dust what do you want us to do? We could flare the engines to push through, " +
            "hold position but this cloud seems to be making the crew act weir or pull back.",

            "Push forwards!", 0, -50, 0,
            "Hold position", 0, 0, -2,
            "pull back", 0, -20 ,0
            ));
        //12
        DATA.Add(new DB(
            "Astoid field",
            "we have come accrose an astroid feel, we could spend some time harvesting some scrap or recharge our power supplies a bit. we have also detected a strange signle we could investigate",

            "Harvest from the astroids", 150,0,0,
            "Recharge batteries", 0, 75,0,
            "Investigate signle", 50,0,2
            ));
        //13
        DATA.Add(new DB(
            "Abandonded station",
            "we have found an abandond station we could search it for goods and scrap however this could be dangerous, we could recharge our batteries from the stations little remaing power" +
            "or we could blast it for scrap.",

            "search the station", 150,0,-1,
            "recharge batteries", 0,75,0,
            "Blast it!", 75,0,0
            ));
        //14
        DATA.Add(new DB(
            "Colony",
            "we have found one of our colonies, we could try and load as many colonist onto our ship or we could prioratize their goods.",

            "Load them all on", 0,0,4,
            "even priority", 50,50,2,
            "only the goods!", 100,100,0
            ));
        //15
        DATA.Add(new DB(
            "Sun Explosion",
            "the sun is exploding here we can collect the material flung out by it or collect energy from it however this will be dangerous. what do you want us to do?",

            "gather material", 200,0,-2,
            "Gather Energy", 0,200,-2,
            "flee from here", 0,0,0
            ));
    }
}
