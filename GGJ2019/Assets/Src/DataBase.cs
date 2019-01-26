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
            "Title",
            "Message",

            "Option 1", 1000, 1000, 1000, // Scrap, Energy, People
            "Option 2", 1000, 1000, 1000, // Scrap, Energy, People
            "Option 3", 1000, 1000, 1000  // Scrap, Energy, People
            ));

        DATA.Add(new DB(
            "Holder",
            "Me is being me in the wassin in the wasin",

            "yes 1", 1000, 1000, 1000, // Scrap, Energy, People
            "no 2", 1000, 1000, 1000, // Scrap, Energy, People
            "possibly 3", 1000, 1000, 1000  // Scrap, Energy, People
            ));
    }
}
