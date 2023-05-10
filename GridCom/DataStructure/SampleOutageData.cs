using System;
using System.Collections.Generic;
using GridCom.DataStructure;
namespace GridCom.DataStructure
{
    public class OutageSampleData
    {

        internal static readonly List<OutageData> outageDataList = new List<OutageData>()
        {
           /* new OutageData()
            {
                Substation_Name="Камское устье",
                Date_Outage="январь",
                Temperature=-5,
                Wind_speed=14,
                Snow=1,
                Rain=0,
                Thunder=0, //==1
            },
             new OutageData()
            {
                Substation_Name="Свияжск",
                Date_Outage="январь",
                Temperature=-16,
                Wind_speed=4,
                Snow=0,
                Rain=0,
                Thunder=0, //==0
             },
              new OutageData()
              {
                Substation_Name="Апастово",
                Date_Outage="май",
                Temperature=10,
                Wind_speed=13,
                Snow=0,
                Rain=1,
                Thunder=0, //==1
              },
              new OutageData()
              {
                Substation_Name="Караталга",
                Date_Outage="июнь",
                Temperature=20,
                Wind_speed=1,
                Snow=0,
                Rain=0,
                Thunder=0, //==0
              },
               new OutageData()
              {
                Substation_Name="Верхний Услон",
                Date_Outage="июнь",
                Temperature=14,
                Wind_speed=17,
                Snow=0,
                Rain=1,
                Thunder=0, //==1
              },*/
             new OutageData()
              {
                Substation_Name="Кайбицы",
                Date_Outage="январь",
                Temperature=-7,
                Wind_speed=15,
                Snow=1,
                Rain=0,
                Thunder=0, //==1
              },
               new OutageData()
              {
                Substation_Name="Ключи",
                Date_Outage="апрель",
                Temperature=15,
                Wind_speed=15,
                Snow=0,
                Rain=1,
                Thunder=1, //==1
              },
                 new OutageData()
              {
                Substation_Name="Нурлаты",
                Date_Outage="ноябрь",
                Temperature=-1,
                Wind_speed=15,
                Snow=1,
                Rain=0,
                Thunder=0, //==1
              },
                 new OutageData()
              {
                Substation_Name="Буинск",
                Date_Outage="май",
                Temperature=15,
                Wind_speed=2,
                Snow=0,
                Rain=0,
                Thunder=0, //==0
              },
                 new OutageData()
              {
                Substation_Name="Каратун",
                Date_Outage="июль",
                Temperature=27,
                Wind_speed=1,
                Snow=0,
                Rain=0,
                Thunder=0, //==0
              },

                 new OutageData()
              {
                Substation_Name="Федоровская",
                Date_Outage="декабрь",
                Temperature=-15,
                Wind_speed=5,
                Snow=0,
                Rain=0,
                Thunder=0, //==0
              }
        };
    }
}